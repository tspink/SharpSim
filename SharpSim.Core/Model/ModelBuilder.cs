//
// ADLBuilder.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSim.Model
{
	using Diagnostics;

	public class ModelBuilder
	{
		private IDiagnostics diag;

		public ModelBuilder (IDiagnostics diag)
		{
			this.diag = diag;
		}

		public bool TryBuild (IEnumerable<Model.AST.ArchFile> archFiles, out Architecture arch)
		{
			arch = null;

			string archName = null;
			foreach (var archFile in archFiles) {
				//var pp = new AST.Visitor.PrettyPrinterVisitor ();
				//pp.VisitArchFile (archFile);

				if (archName == null) {
					archName = archFile.Identifier.Identifier;
				} else if (archName != archFile.Identifier.Identifier) {
					this.diag.AddError (
						archFile.Identifier.Location.ToDiagnosticLocation (),
						"Multiply defined architecture identifier.  Identifier already defined as {0}", archName);
					return false;
				}
			}

			if (string.IsNullOrEmpty (archName)) {
				this.diag.AddError (DiagnosticLocation.Empty, "Architecture name not declared");
				return false;
			}

			arch = new Architecture (archName);

			var context = new SSA.SSAContext ();

			// Load register file
			int currentBase = 0;
			foreach (var archFile in archFiles) {
				foreach (var regspace in archFile.RegisterSpaces) {
					// Align the base of the space up
					if ((currentBase % 16) != 0) {
						currentBase += 16 - (currentBase % 16);
					}

					int maximumSize = 0;
					foreach (var def in regspace.RegisterDefinitions) {
						if (def is AST.RegisterBank) {
							var bank = def as AST.RegisterBank;
							arch.RegisterFile.AddRegisterBank (bank.Name, bank.Type, bank.Count, bank.Width, bank.Stride, currentBase + bank.Offset);
							if ((bank.Offset + (bank.Stride * bank.Count)) > maximumSize) {
								maximumSize = (bank.Offset + (bank.Stride * bank.Count));
							}
						} else if (def is AST.RegisterSlot) {
							var slot = def as AST.RegisterSlot;
							arch.RegisterFile.AddRegister (slot.Name, slot.Type, slot.Tag, slot.Width, currentBase + slot.Offset);
							if ((slot.Offset + slot.Width) > maximumSize) {
								maximumSize = slot.Offset + slot.Width;
							}
						} else if (def is AST.VectorRegisterBank) {
							var vrb = def as AST.VectorRegisterBank;
							arch.RegisterFile.AddVectorRegisterBank (
								vrb.Name,
								vrb.Type,
								vrb.Arity,
								vrb.Count,
								vrb.Width,
								vrb.Stride,
								currentBase + vrb.Offset);

							if ((vrb.Offset + (vrb.Arity * vrb.Stride * vrb.Count)) > maximumSize) {
								maximumSize = vrb.Offset + (vrb.Arity * vrb.Stride * vrb.Count);
							}
						} else {
							throw new NotImplementedException ();
						}
					}

					currentBase += maximumSize;
				}
			}

			// Load ISAs
			foreach (var archFile in archFiles) {
				foreach (var isaBlock in archFile.ISABlocks) {
					var isa = arch.GetOrCreateISA (isaBlock.Name);

					// Load Formats
					foreach (var format in isaBlock.FormatDefinitions) {
						var instructionFormat = isa.CreateInstructionFormat (format.Name);

						int currentOffset = 0;
						foreach (var field in format.FieldDefinitions) {
							if (field is AST.NamedFormatFieldDefinition) {
								instructionFormat.AddField (
									((AST.NamedFormatFieldDefinition)field).Name,
									currentOffset,
									field.Width);
							} else if (field is AST.ConstrainedFormatFieldDefinition) {
								instructionFormat.AddConstraint (
									((AST.ConstrainedFormatFieldDefinition)field).Value,
									currentOffset,
									field.Width);
							}
							currentOffset += field.Width;
						}
					}
				}
			}

			// Register Exceptions
			foreach (var archFile in archFiles) {
				foreach (var decl in archFile.Exceptions) {
					arch.CreateException (decl.ExceptionType);
				}
			}

			// Generate helper prototypes
			foreach (var archFile in archFiles) {
				foreach (var helper in archFile.Helpers) {
					var prototype = GeneratePrototype (helper);

					if (context.HasAction (prototype)) {
						throw new Exception ();
					}

					context.CreateAction (prototype);
				}
			}

			// Load instruction definitions
			foreach (var archFile in archFiles) {
				foreach (var isablock in archFile.ISABlocks) {
					var isa = arch.GetOrCreateISA (isablock.Name);

					foreach (var insn in isablock.Instructions) {
						InstructionFormat fmt;
						try {
							fmt = isa.GetInstructionFormat (insn.FormatName);
						} catch {
							this.diag.AddError (insn.Location.ToDiagnosticLocation (), "Instruction format '{0}' does not exist in ISA", insn.FormatName);
							continue;
						}

						var associatedBehaviours = new List<InstructionBehaviourInstantiation> ();
						foreach (var part in insn.Parts) {
							if (part is AST.BehaviourPart) {
								var behaviourPart = (AST.BehaviourPart)part;
								try {
									var behaviour = InstantiateBehaviour (arch, fmt, context, archFiles, behaviourPart.Name, behaviourPart.InstantiationTypes);
									associatedBehaviours.Add (new InstructionBehaviourInstantiation (behaviour));
								} catch (Exception ex) {
									this.diag.AddError (part.Location.ToDiagnosticLocation (), "Exception whilst processing {0}: {1}", ((AST.BehaviourPart)part).Name, ex);
									//this.diag.AddError(part.Location.ToDiagnosticLocation(), "Behaviour '{0}' does not exist", ((AST.BehaviourPart)part).Name);
									continue;
								}
							}
						}

						if (associatedBehaviours.Count == 0) {
							this.diag.AddError (insn.Location.ToDiagnosticLocation (), "Instruction '{0}' does not define its behaviour", insn.Name);
							continue;
						}

						var instruction = isa.CreateInstruction (insn.Name, fmt, associatedBehaviours);
						foreach (var part in insn.Parts) {
							if (part is AST.MatchPart) {
								instruction.AddDecodeMatch (BuildDecodeMatch (fmt, (AST.MatchPart)part));
							} else if (part is AST.DisasmPart) {
								//throw new NotImplementedException();
							} else if (part is AST.BehaviourPart) {
								continue;
							} else {
								throw new NotSupportedException ();
							}
						}
					}
				}
			}

			foreach (var archFile in archFiles) {
				foreach (var helperDefinition in archFile.Helpers) {
					SSA.SSAAction action;

					var prototype = GeneratePrototype (helperDefinition);
					if (!context.TryGetAction (prototype, out action, false)) {
						diag.AddError (helperDefinition.Location.ToDiagnosticLocation (), "Helper not registered as action");
						continue;
					}

					BuildHelper (action, helperDefinition, arch);
				}
			}

			Console.WriteLine ("BEFORE OPTIMISATION");
			foreach (var action in context.Actions) {
				if (!action.External) {
					Console.WriteLine (action);
				}
			}

			var optimiser = new SSA.Optimiser.SSAOptimiser (diag);
			foreach (var action in context.Actions) {
				if (!action.External) {
					optimiser.OptimiseAction (action);
				}
			}

			Console.WriteLine ("AFTER OPTIMISATION");
			foreach (var action in context.Actions) {
				if (!action.External) {
					Console.WriteLine (action);
				}
			}


			return !this.diag.HasErrors;
		}

		private Instruction.DecodeMatch BuildDecodeMatch (InstructionFormat fmt, AST.MatchPart part)
		{
			// TODO: Implement



			return new Instruction.DecodeMatch ();
		}

		private Behaviour InstantiateBehaviour (Architecture arch, InstructionFormat format, SSA.SSAContext context, IEnumerable<Model.AST.ArchFile> archFiles, string behaviourName, IEnumerable<string> typeArguments)
		{
			var targetPrototype = new SSA.SSAActionPrototype (SSA.PrimitiveType.Void, behaviourName);

			foreach (var typeArgument in typeArguments) {
				targetPrototype.AddTypeParameter (SSA.SSAType.FromString (null, typeArgument));
			}

			if (context.HasAction (targetPrototype)) {
				return new Behaviour (behaviourName, context.GetAction (targetPrototype));
			}

			var action = context.CreateAction (targetPrototype);

			AST.Behaviour candidate = null;
			foreach (var archFile in archFiles) {
				foreach (var behaviour in archFile.Behaviours) {
					if (behaviour.Name == behaviourName)
						candidate = behaviour;
				}
			}

			if (candidate == null)
				throw new Exception ("Behaviour not found");

			return BuildBehaviour (action, candidate, format, arch);
		}

		private Behaviour BuildBehaviour (SSA.SSAAction action, Model.AST.Behaviour behaviour, InstructionFormat format, Architecture arch)
		{
			var visitor = new SSA.SSAASTVisitor (this.diag, action, format, arch.RegisterFile);
			visitor.VisitBehaviour (behaviour);

			return new Behaviour (behaviour.Name, action);
		}

		private Helper BuildHelper (SSA.SSAAction action, Model.AST.Helper helper, Architecture arch)
		{
			var visitor = new SSA.SSAASTVisitor (this.diag, action, null, arch.RegisterFile);
			visitor.VisitHelper (helper);

			return new Helper (helper.Name, action);
		}

		private SSA.SSAActionPrototype GeneratePrototype (AST.Helper helper)
		{
			var actionPrototype = new SSA.SSAActionPrototype (SSA.SSAType.FromString (null, helper.ReturnType), helper.Name);

			foreach (var typeParameter in helper.TypeParameters) {
				SSA.SSAType typeParameterType;
				if (!SSA.SSAType.TryFromString (null, typeParameter, out typeParameterType)) {
					typeParameterType = new SSA.SSATypeParameter (typeParameter);
				}

				actionPrototype.AddTypeParameter (typeParameterType);
			}

			foreach (var parameter in helper.Parameters) {
				actionPrototype.AddParameter (SSA.SSAType.FromString (null, parameter.Type));
			}

			return actionPrototype;
		}

		private SSA.SSAActionPrototype GeneratePrototype (AST.Behaviour behaviour)
		{
			var actionPrototype = new SSA.SSAActionPrototype (
									  SSA.PrimitiveType.Void,
									  behaviour.Name);

			foreach (var typeParameter in behaviour.TypeParameters) {
				SSA.SSAType typeParameterType;
				if (!SSA.SSAType.TryFromString (null, typeParameter, out typeParameterType)) {
					typeParameterType = new SSA.SSATypeParameter (typeParameter);
				}

				actionPrototype.AddTypeParameter (typeParameterType);
			}

			return actionPrototype;
		}

		private InstructionFormat GetInstructionFormat (Architecture arch, string fqn)
		{
			var parts = fqn.Split ('.');
			if (parts.Length != 2)
				throw new Exception ("Invalid name for instruction format");

			var isa = arch.GetISA (parts [0]);
			return isa.GetInstructionFormat (parts [1]);
		}
	}
}

