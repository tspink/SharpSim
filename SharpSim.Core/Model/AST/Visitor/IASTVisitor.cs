//
// IVisitor.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;

namespace SharpSim.Model.AST.Visitor
{
	public interface IASTVisitor
	{
		void VisitAction(Action action);

		void VisitAddOperator(AddOperator addExpression);

		void VisitArchFile(ArchFile file);

		void VisitArchIdentifier(ArchIdentifier ident);

		void VisitAssignmentExpression(AssignmentExpression asnExpression);

		void VisitBehaviour(Behaviour behaviour);

		void VisitBinaryOperator(BinaryOperator binop);

		void VisitConstantExpression<T>(ConstantExpression<T> constExpr);

		void VisitIntegerConstantExpression(IntegerConstantExpression constExpr);

		void VisitFloatConstantExpression(FloatConstantExpression constExpr);

		void VisitStringConstantExpression(StringConstantExpression constExpr);

		void VisitEqualityOperator(EqualityOperator expr);

		void VisitExpression(Expression expr);

		void VisitFunctionBody(FunctionBody body);

		void VisitFunctionCall(FunctionCall call);

		void VisitHelper(Helper helper);

		void VisitIfStatement(IfStatement ifStatement);

		void VisitISABlock(ISABlock block);

		void VisitParameter(Parameter param);

		void VisitReadRegister(ReadRegister readRegister);

		void VisitReadRegisterBank(ReadRegisterBank readRegisterBank);

		void VisitReturn(Return ret);

		void VisitBreak(Break brk);

		void VisitRaise(Raise raise);

		void VisitStructAccess(StructAccess structAccess);

		void VisitSwitch(SwitchStatement switchStatement);

		void VisitSymbolExpression(SymbolExpression symbol);

		void VisitTernaryOperator(TernaryOperator ternary);

		void VisitVariableDeclaration(VariableDeclaration varDecl);

		void VisitWriteRegister(WriteRegister writeRegister);

		void VisitWriteRegisterBank(WriteRegisterBank writeRegisterBank);

		void VisitReadPC(ReadPC readPC);

		void VisitUnaryOperator(UnaryOperator unop);

		void VisitShiftOperator(ShiftOperator shop);

		void VisitLogicalOperator(LogicalOperator logop);

		void VisitBitwiseOperator(BitwiseOperator bitop);

		void VisitCastOperator(CastOperator castop);

		void VisitFormatDefinition(FormatDefinition formatDef);

		void VisitFormatFieldDefinition(FormatFieldDefinition formatFieldDef);

		void VisitNamedFormatFieldDefinition(NamedFormatFieldDefinition formatFieldDef);

		void VisitConstrainedFormatFieldDefinition(ConstrainedFormatFieldDefinition formatFieldDef);

		void VisitRegisterSpace(RegisterSpace regspace);

		void VisitRegisterBank(RegisterBank regbank);

		void VisitVectorRegisterBank(VectorRegisterBank vectorRegBank);

		void VisitRegisterSlot(RegisterSlot regslot);

		void VisitComparisonOperator(ComparisonOperator comop);

		void VisitInstruction(Instruction insn);

		void VisitInstructionPart(InstructionPart part);

		void VisitMatchPart(MatchPart match);

		void VisitMatchExpression(MatchExpression expr);

		void VisitBinaryMatchExpression(BinaryMatchExpression expr);

		void VisitComparisonMatchExpression(ComparisonMatchExpression expr);

		void VisitDisasmPart(DisasmPart disasm);

		void VisitDisasmStatement(DisasmStatement stmt);

		void VisitDisasmAppend(DisasmAppend append);

		void VisitDisasmWhere(DisasmWhere clause);

		void VisitBehaviourPart(BehaviourPart behaviour);

		void VisitExceptionDeclaration(ExceptionDeclaration exceptionDeclaration);
	}
}

