//
// SSAASTVisitor.cs
//
// Copyright (C) Tom Spink 2016 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.Collections.Generic;

namespace SharpSim.Model.SSA
{
	using AST;
	using AST.Visitor;
	using Diagnostics;

	public class SSAASTVisitor : ASTVisitor
	{
		private IDiagnostics diagnostics;
		private SSAAction action;
		private SSABlock currentBlock;
		private Stack<SSAScope> scope = new Stack<SSAScope>();
		private List<Tuple<ControlFlowStatement, int>> unresolvedControlFlow = new List<Tuple<ControlFlowStatement, int>>();
		private InstructionFormat format;
		private RegisterFile registerFile;

		public SSAASTVisitor(IDiagnostics diagnostics, SSAAction action, InstructionFormat format, RegisterFile file)
		{
			if (diagnostics == null)
				throw new ArgumentNullException(nameof(diagnostics));
            
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			if (file == null)
				throw new ArgumentNullException(nameof(registerFile));

			this.diagnostics = diagnostics;
			this.action = action;
			this.format = format;
			this.registerFile = file;
		}

		public override void VisitBehaviour(Behaviour behaviour)
		{
			if (currentBlock != null)
				throw new InvalidOperationException();

			if (this.format == null)
				throw new InvalidOperationException();

			currentBlock = this.action.EntryBlock;

			var rootScope = new SSAScope();
			scope.Push(rootScope);

			foreach (var exp in this.format.ISA.Architecture.Exceptions) {
				rootScope.CreateSymbol(exp.Name, ExceptionType.Type);
			}

			foreach (var field in this.format.Fields) {
				rootScope.CreateSymbol("inst." + field.Name, PrimitiveType.UInt32);
			}

			base.VisitBehaviour(behaviour);

			if (scope.Pop() != rootScope)
				throw new Exception("Unbalanced scope");

			if (!(this.currentBlock.Last is ControlFlowStatement))
				CurrentBlock.AddStatement(new LeaveStatement());
		}

		public override void VisitHelper(Helper helper)
		{
			if (currentBlock != null)
				throw new InvalidOperationException();

			currentBlock = this.action.EntryBlock;

			var rootScope = new SSAScope();
			scope.Push(rootScope);

			foreach (var parameter in helper.Parameters) {
				if (rootScope.InLocalScope(parameter.Name)) {
					diagnostics.AddError(parameter.Location.ToDiagnosticLocation(), "Duplicate parameter name {0}", parameter.Name);
					return;
				}

				rootScope.CreateSymbol(parameter.Name, SSAType.FromString(parameter.Type));
			}

			base.VisitHelper(helper);

			if (scope.Pop() != rootScope)
				throw new Exception("Unbalanced scope");

			if (!(this.currentBlock.Last is ControlFlowStatement))
				CurrentBlock.AddStatement(new ReturnStatement(null));
		}

		public override void VisitFunctionBody(FunctionBody body)
		{
			var newScope = new SSAScope(scope.Peek());
			scope.Push(newScope);

			foreach (var stmt in body.Statements) {
				if (CurrentBlock.Last != null && CurrentBlock.Last is ControlFlowStatement) {
					diagnostics.AddWarning(stmt.Location.ToDiagnosticLocation(), "Unreachable code detected");
					break;
				}

				stmt.Accept(this);
			}

			if (scope.Pop() != newScope)
				throw new Exception("Unbalanced scope");
		}

		public override void VisitReturn(Return ret)
		{
			SSAOperand operand = ret.Value == null ? null : ExpressionToOperand(ret.Value);

			CurrentBlock.AddStatement(new ReturnStatement(operand));
		}

		public override void VisitRaise(Raise raise)
		{
			if (!(raise.Value is SymbolExpression))
				diagnostics.AddError(raise.Value.Location.ToDiagnosticLocation(), "Raise expression must be a symbol");

			SSASymbol symbol;
			try {
				symbol = CurrentScope.ResolveSymbol(((SymbolExpression)raise.Value).Symbol);
			} catch {
				diagnostics.AddError(raise.Value.Location.ToDiagnosticLocation(), "Symbol '{0}' does not exist", ((SymbolExpression)raise.Value).Symbol);
				return;
			}

			if (!(symbol.Type is ExceptionType))
				diagnostics.AddError(raise.Value.Location.ToDiagnosticLocation(), "Raise symbol must be an exception type");

			CurrentBlock.AddStatement(new RaiseStatement(symbol.AsOperand()));
		}

		public override void VisitReadRegisterBank(ReadRegisterBank readRegisterBank)
		{
			if (!(readRegisterBank.Bank is SymbolExpression)) {
				diagnostics.AddError(readRegisterBank.Bank.Location.ToDiagnosticLocation(), "Register Bank identifier must be a symbol");
				return;
			}

			SSAType registerType;
			var offset = BankedRegisterOperand(readRegisterBank.Bank as SymbolExpression, readRegisterBank.Id, out registerType);
			CurrentBlock.AddStatement(new LoadRegisterStatement(offset, registerType));
		}

		public override void VisitReadRegister(ReadRegister readRegister)
		{
			if (!(readRegister.Id is SymbolExpression)) {
				diagnostics.AddError(readRegister.Id.Location.ToDiagnosticLocation(), "Register identifier must be a symbol");
				return;
			}

			SSAType registerType;
			var offset = RegisterOperand((SymbolExpression)readRegister.Id, out registerType);
			CurrentBlock.AddStatement(new LoadRegisterStatement(offset, registerType));
		}

		public override void VisitWriteRegisterBank(WriteRegisterBank writeRegisterBank)
		{
			if (!(writeRegisterBank.Bank is SymbolExpression)) {
				diagnostics.AddError(writeRegisterBank.Bank.Location.ToDiagnosticLocation(), "Register Bank identifier must be a symbol");
				return;
			}

			SSAType registerType;
			var offset = BankedRegisterOperand(writeRegisterBank.Bank as SymbolExpression, writeRegisterBank.Id, out registerType);
			var value = ExpressionToOperand(writeRegisterBank.Value);

			var ssa = new StoreRegisterStatement(value, offset, registerType);
			currentBlock.AddStatement(ssa);
		}

		public override void VisitWriteRegister(WriteRegister writeRegister)
		{
			if (!(writeRegister.Id is SymbolExpression)) {
				diagnostics.AddError(writeRegister.Id.Location.ToDiagnosticLocation(), "Register identifier must be a symbol");
				return;
			}

			SSAType registerType;
			var offset = RegisterOperand((SymbolExpression)writeRegister.Id, out registerType);
			var value = ExpressionToOperand(writeRegister.Value);
			currentBlock.AddStatement(new StoreRegisterStatement(value, offset, registerType));
		}

		private TypedSSAOperand BankedRegisterOperand(SymbolExpression bankSymbol, Expression id, out SSAType registerType)
		{
			var bank = registerFile.GetRegisterBank(bankSymbol.Symbol);
			registerType = bank.Type;

			var mla = new MLAStatement(new IntegerOperand(PrimitiveType.UInt32, bank.Offset), ExpressionToOperand(id), new IntegerOperand(PrimitiveType.UInt32, 2));
			currentBlock.AddStatement(mla);

			return mla.AsOperand();
		}

		private TypedSSAOperand RegisterOperand(SymbolExpression id, out SSAType registerType)
		{
			var reg = registerFile.GetRegister(id.Symbol);

			registerType = reg.Type;
			return new IntegerOperand(PrimitiveType.UInt32, reg.Offset);
		}

		public override void VisitFunctionCall(FunctionCall call)
		{
			var action = CurrentBlock.Owner.Owner.GetAction(call.Name);
			var ssa = new CallStatement(action.AsOperand());

			foreach (var arg in call.Arguments) {
				ssa.AddArgument(ExpressionToOperand(arg));
			}

			currentBlock.AddStatement(ssa);
		}

		public override void VisitStructAccess(StructAccess structAccess)
		{
			var ssa = new LoadValueStatement(CurrentScope.ResolveSymbol("inst." + structAccess.Member).AsOperand());
			currentBlock.AddStatement(ssa);
		}

		public override void VisitAddOperator(AddOperator addExpression)
		{
			var lhs = ExpressionToOperand(addExpression.LHS);
			var rhs = ExpressionToOperand(addExpression.RHS);

			var ssa = new ArithmeticStatement(
				          lhs,
				          rhs,
				          ArithmeticStatement.ArithmeticOperation.Add);
            
			this.currentBlock.AddStatement(ssa);
		}

		public override void VisitSymbolExpression(SymbolExpression symbolExpression)
		{
			SSASymbol symbol;

			try {
				symbol = CurrentScope.ResolveSymbol(symbolExpression.Symbol);
			} catch {
				this.diagnostics.AddError(
					symbolExpression.Location.ToDiagnosticLocation(),
					"Symbol '{0}' not in scope",
					symbolExpression.Symbol);
				return;
			}

			this.currentBlock.AddStatement(new LoadValueStatement(symbol.AsOperand()));
		}

		public override void VisitAssignmentExpression(AssignmentExpression asnExpression)
		{
			if (!(asnExpression.LHS is SymbolExpression)) {
				this.diagnostics.AddError(asnExpression.LHS.Location.ToDiagnosticLocation(), "Left hand side of assignment must be a symbol");
				return;
			}

			var rhs = ExpressionToOperand(asnExpression.RHS);

			SSASymbol sym;

			try {
				sym = CurrentScope.ResolveSymbol(((SymbolExpression)asnExpression.LHS).Symbol);
			} catch {
				this.diagnostics.AddError(
					asnExpression.LHS.Location.ToDiagnosticLocation(),
					"Symbol '{0}' does not exist in scope", ((SymbolExpression)asnExpression.LHS).Symbol);
				return;
			}

			var ssa = new StoreValueStatement(rhs, sym.AsOperand());

			if (ssa.Fixed == SSAStatement.Fixedness.Dynamic)
				sym.Fixedness = SSAStatement.Fixedness.Dynamic;

			this.currentBlock.AddStatement(ssa);
		}

		public override void VisitVariableDeclaration(VariableDeclaration varDecl)
		{
			if (CurrentScope.InLocalScope(varDecl.Name)) {
				diagnostics.AddError(varDecl.Location.ToDiagnosticLocation(), "Variable already declared in scope");
				return;
			} else if (CurrentScope.InAnyParentScope(varDecl.Name)) {
				diagnostics.AddWarning(varDecl.Location.ToDiagnosticLocation(), "Variable shadows parent scope");
			}

			var symbol = CurrentScope.CreateSymbol(varDecl.Name, SSAType.FromString(varDecl.Type));

			if (varDecl.Assignment != null) {
				var asn = ExpressionToOperand(varDecl.Assignment);
				var ssa = new StoreValueStatement(asn, symbol.AsOperand());
				this.currentBlock.AddStatement(ssa);
			}
		}

		public override void VisitIfStatement(IfStatement ifStatement)
		{
			var ssaCond = ExpressionToOperand(ifStatement.Condition);
			var branchBlock = this.currentBlock;

			var trueBlock = this.action.CreateBlock();
			this.currentBlock = trueBlock;

			ifStatement.IfTrue.Accept(this);

			SSABlock postBlock = null;

			if (!(trueBlock.Last is ControlFlowStatement)) {
				postBlock = this.action.CreateBlock();
				trueBlock.AddStatement(new JumpStatement(postBlock.AsOperand()));
			}

			SSABlock falseBlock;
			if (ifStatement.IfFalse != null) {
				falseBlock = this.action.CreateBlock();
				this.currentBlock = falseBlock;
				ifStatement.IfFalse.Accept(this);

				if (!(falseBlock.Last is ControlFlowStatement)) {
					if (postBlock == null)
						postBlock = this.action.CreateBlock();
					falseBlock.AddStatement(new JumpStatement(postBlock.AsOperand()));
				}
			} else {
				falseBlock = this.action.CreateBlock();
			}

			if (postBlock != null)
				this.currentBlock = postBlock;
			else
				this.currentBlock = falseBlock;

			var ssa = new BranchStatement(
				          BranchStatement.BranchPredicate.IfNonZero,
				          ssaCond,
				          trueBlock.AsOperand(),
				          falseBlock.AsOperand());

			branchBlock.AddStatement(ssa);
		}

		public override void VisitEqualityOperator(EqualityOperator equalityExpression)
		{
			var lhs = ExpressionToOperand(equalityExpression.LHS);
			var rhs = ExpressionToOperand(equalityExpression.RHS);

			ComparisonStatement.ComparisonKind comparisonKind;
			switch (equalityExpression.Type) {
			case EqualityOperatorType.Equal:
				comparisonKind = ComparisonStatement.ComparisonKind.Equal;
				break;
			case EqualityOperatorType.NotEqual:
				comparisonKind = ComparisonStatement.ComparisonKind.NotEqual;
				break;
			default:
				throw new NotSupportedException();
			}

			var ssa = new ComparisonStatement(lhs, rhs, comparisonKind);

			this.currentBlock.AddStatement(ssa);
		}

		public override void VisitComparisonOperator(ComparisonOperator comop)
		{
			var lhs = ExpressionToOperand(comop.LHS);
			var rhs = ExpressionToOperand(comop.RHS);

			ComparisonStatement.ComparisonKind comparisonKind;
			switch (comop.Type) {
			case ComparisonOperatorType.GreaterThan:
				comparisonKind = ComparisonStatement.ComparisonKind.GreaterThan;
				break;
			case ComparisonOperatorType.GreaterThanOrEqual:
				comparisonKind = ComparisonStatement.ComparisonKind.GreaterThanOrEqual;
				break;
			case ComparisonOperatorType.LessThan:
				comparisonKind = ComparisonStatement.ComparisonKind.LessThan;
				break;
			case ComparisonOperatorType.LessThanOrEqual:
				comparisonKind = ComparisonStatement.ComparisonKind.LessThanOrEqual;
				break;
			default:
				throw new NotSupportedException();
			}

			var ssa = new ComparisonStatement(lhs, rhs, comparisonKind);

			this.currentBlock.AddStatement(ssa);
		}

		private TypedSSAOperand ExpressionToOperand(Expression expr)
		{
			if (expr is IntegerConstantExpression) {
				return ((IntegerConstantExpression)expr).Value.AsOperand();
			} else if (expr is FloatConstantExpression) {
				return ((FloatConstantExpression)expr).Value.AsOperand();
			} else {
				expr.Accept(this);
			}

			return this.currentBlock.Last.AsOperand();
		}

		private SSABlock CurrentBlock{ get { return this.currentBlock; } }

		private SSAScope CurrentScope{ get { return this.scope.Peek(); } }
	}
}

