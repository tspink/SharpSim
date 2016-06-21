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

		public ModelBuilder(IDiagnostics diag)
		{
			this.diag = diag;
		}

		public bool TryBuild(IEnumerable<Model.AST.ArchFile> archFiles, out Architecture arch)
		{
			arch = null;

			string archName = null;
			foreach (var archFile in archFiles) {
				//var pp = new AST.Visitor.PrettyPrinterVisitor();
				//pp.VisitArchFile(archFile);

				if (archName == null) {
					archName = archFile.Identifier.Identifier;
				} else if (archName != archFile.Identifier.Identifier) {
					this.diag.AddError(
						archFile.Identifier.Location.ToDiagnosticLocation(),
						"Multiply defined architecture identifier.  Identifier already defined as {0}", archName);
					return false;
				}
			}
				
			if (string.IsNullOrEmpty(archName)) {
				this.diag.AddError(DiagnosticLocation.Empty, "Architecture name not declared");
				return false;
			}

			arch = new Architecture(archName);

			var context = new SSA.SSAContext();

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
							arch.RegisterFile.AddRegisterBank(bank.Name, bank.Type, bank.Count, bank.Width, bank.Stride, currentBase + bank.Offset);
							if ((bank.Offset + (bank.Stride * bank.Count)) > maximumSize) {
								maximumSize = (bank.Offset + (bank.Stride * bank.Count));
							}
						} else if (def is AST.RegisterSlot) {
							var slot = def as AST.RegisterSlot;
							arch.RegisterFile.AddRegister(slot.Name, slot.Type, slot.Tag, slot.Width, currentBase + slot.Offset);
							if ((slot.Offset + slot.Width) > maximumSize) {
								maximumSize = slot.Offset + slot.Width;
							}
						} else if (def is AST.VectorRegisterBank) {
							var vrb = def as AST.VectorRegisterBank;
							arch.RegisterFile.AddVectorRegisterBank(
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
							throw new NotImplementedException();
						}
					}

					currentBase += maximumSize;
				}
			}

			// Load ISAs
			foreach (var archFile in archFiles) {
				foreach (var isaBlock in archFile.ISABlocks) {
					var isa = arch.GetOrCreateISA(isaBlock.Name);

					// Load Formats
					foreach (var format in isaBlock.FormatDefinitions) {
						var instructionFormat = isa.CreateInstructionFormat(format.Name);

						int currentOffset = 0;
						foreach (var field in format.FieldDefinitions) {
							if (field is AST.NamedFormatFieldDefinition) {
								instructionFormat.AddField(
									((AST.NamedFormatFieldDefinition)field).Name,
									currentOffset,
									field.Width);
							} else if (field is AST.ConstrainedFormatFieldDefinition) {
								instructionFormat.AddConstraint(
									((AST.ConstrainedFormatFieldDefinition)field).Value,
									currentOffset,
									field.Width);
							}
							currentOffset += field.Width;
						}
					}
				}
			}

			// Load helper prototypes
			foreach (var archFile in archFiles) {
				foreach (var helper in archFile.Helpers) {
					var proto = GeneratePrototype(helper);
					if (proto != null)
						context.CreateAction(helper.Name, proto);
				}
			}

			// Create helpers
			foreach (var archFile in archFiles) {
				foreach (var helper in archFile.Helpers) {
					var o = BuildHelper(context, helper);
					if (o != null)
						arch.AddHelper(o);
				}
			}

			// Create behaviours
			foreach (var archFile in archFiles) {
				foreach (var behaviour in archFile.Behaviours) {
					var b = BuildBehaviour(context, arch, behaviour);
					if (b != null)
						arch.AddBehaviour(b);
				}
			}

			// Load instruction definitions
			foreach (var archFile in archFiles) {
				foreach (var isablock in archFile.ISABlocks) {
					var isa = arch.GetOrCreateISA(isablock.Name);

					foreach (var insn in isablock.Instructions) {
						InstructionFormat fmt;
						try {
							fmt = isa.GetInstructionFormat(insn.FormatName);
						} catch {
							this.diag.AddError(insn.Location.ToDiagnosticLocation(), "Instruction format '{0}' does not exist in ISA", insn.FormatName);
							continue;
						}

						Behaviour behaviour = null;
						foreach (var part in insn.Parts) {
							if (part is AST.BehaviourPart) {
								if (behaviour != null) {
									this.diag.AddError(part.Location.ToDiagnosticLocation(), "Multiply defined behaviour for instruction");
									continue;
								}

								try {
									behaviour = arch.GetBehaviour(((AST.BehaviourPart)part).Name);
								} catch {
									this.diag.AddError(part.Location.ToDiagnosticLocation(), "Behaviour '{0}' does not exist", ((AST.BehaviourPart)part).Name);
									continue;
								}
							}
						}

						if (behaviour == null) {
							this.diag.AddError(insn.Location.ToDiagnosticLocation(), "Instruction '{0}' does not define its behaviour", insn.Name);
							continue;
						}

						var instruction = isa.CreateInstruction(insn.Name, fmt, behaviour);
						foreach (var part in insn.Parts) {
							if (part is AST.MatchPart) {
								//throw new NotImplementedException();
							} else if (part is AST.DisasmPart) {
								//throw new NotImplementedException();
							} else if (part is AST.BehaviourPart) {
								continue;
							}
						}
					}
				}
			}

			return !this.diag.HasErrors;
		}

		private Helper BuildHelper(SSA.SSAContext context, AST.Helper helper)
		{
			try {
				var action = context.GetAction(helper.Name);
				var visitor = new SSA.SSAASTVisitor(this.diag, action, null);
				visitor.VisitHelper(helper);

				return new Helper(helper.Name, action);
			} catch (SSA.Exceptions.NoSuchActionException) {
				diag.AddError(helper.Location.ToDiagnosticLocation(), "Helper prototype was not registered");
				return null;
			}
		}

		private Behaviour BuildBehaviour(SSA.SSAContext context, Architecture arch, AST.Behaviour behaviour)
		{
			try {
				var action = context.CreateAction(behaviour.Name, GeneratePrototype(behaviour));
				var visitor = new SSA.SSAASTVisitor(this.diag, action, arch.GetISA(behaviour.ISAName).GetInstructionFormat(behaviour.FormatName));
				visitor.VisitBehaviour(behaviour);

				return new Behaviour(behaviour.Name, action);
			} catch (SSA.Exceptions.DuplicateActionException) {
				diag.AddError(behaviour.Location.ToDiagnosticLocation(), "Behaviour name '{0}' conflicts with existing action.", behaviour.Name);
				return null;
			}
		}

		private SSA.SSAActionPrototype GeneratePrototype(AST.Helper helper)
		{
			try {
				return SSA.SSAActionPrototype.FromParameters(
					SSA.SSAType.FromString(helper.ReturnType),
					helper.Parameters.Select(p => SSA.SSAType.FromString(p.Type, p.Reference)));
			} catch {
				diag.AddError(helper.Location.ToDiagnosticLocation(), "Exception whilst generating helper prototype");
				return null;
			}
		}

		private SSA.SSAActionPrototype GeneratePrototype(AST.Behaviour behaviour)
		{
			return new SSA.SSAActionPrototype(SSA.PrimitiveType.Void);
		}

		private InstructionFormat GetInstructionFormat(Architecture arch, string fqn)
		{
			var parts = fqn.Split('.');
			if (parts.Length != 2)
				throw new Exception("Invalid name for instruction format");

			var isa = arch.GetISA(parts[0]);
			return isa.GetInstructionFormat(parts[1]);
		}
	}
}

