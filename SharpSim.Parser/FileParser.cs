﻿//
// FileParser.cs
//
// Copyright (C) Tom Spink 2015 <tspink@gmail.com>
// All Rights Reserved
//
using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace SharpSim.Parser
{
    using Diagnostics;
    using Model.AST;
    using Grammar;

    public class FileParser
    {
        private IDiagnostics diag;
        private string filename;

        public FileParser(IDiagnostics diag, string filename)
        {
            this.diag = diag;
            this.filename = filename;
        }

        public bool TryParse(out ArchFile node)
        {
            using (var file = File.OpenRead(this.filename)) {
                return TryParse(file, out node);
            }
        }

        public bool TryParse(Stream file, out ArchFile node)
        {
            var chars = new AntlrInputStream(file);
            var tokenSource = new ArchFileLexer(chars);
            var tokens = new BufferedTokenStream(tokenSource);
            ArchFileParser p = new ArchFileParser(tokens);

            p.AddErrorListener(new DiagnosticErrorListener(this.diag, filename));

            var ctx = p.start();

            if (!this.diag.HasErrors && ctx != null) {
                node = BuildAST(ctx);
                return true;
            } else {
                node = null;
                return false;
            }
        }

        private ArchFile BuildAST(ArchFileParser.StartContext ctx)
        {
            var af = new ArchFile(new ASTNode.ASTNodeLocation
                {
                    Filename = filename,
                    Line = 0,
                    Column = 0,
                }, new ArchIdentifier(
                             ctx.arch_ident().ARCH().Symbol.ToASTLocation(filename),
                             ctx.arch_ident().IDENT().GetText()));

            foreach (var def in ctx.def()) {
                if (def.isa_block_def() != null) {
                    af.AddISABlock(BuildISABlock(def.isa_block_def()));
                } else if (def.behaviour_def() != null) {
                    af.AddBehaviour(BuildBehaviour(def.behaviour_def()));
                } else if (def.helper_def() != null) {
                    af.AddHelper(BuildHelper(def.helper_def()));
                }
            }

            return af;
        }

        private ISABlock BuildISABlock(ArchFileParser.Isa_block_defContext ctx)
        {
            return new ISABlock(ctx.ISA().Symbol.ToASTLocation(filename));
        }

        private Behaviour BuildBehaviour(ArchFileParser.Behaviour_defContext ctx)
        {
            return new Behaviour(
                ctx.BEHAVIOUR().Symbol.ToASTLocation(filename),
                ctx.type.Text,
                ctx.name.Text,
                BuildBody(ctx.fnbody()));

        }

        private Helper BuildHelper(ArchFileParser.Helper_defContext ctx)
        {
            var helper = new Helper(
                             ctx.HELPER().Symbol.ToASTLocation(filename),
                             ctx.prototype().rtype.Text,
                             ctx.prototype().name.Text,
                             BuildBody(ctx.fnbody()));

            if (ctx.prototype().parameter_list() != null) {
                foreach (var param in ctx.prototype().parameter_list().parameter()) {
                    helper.AddParameter(new Parameter(
                            param.type.ToASTLocation(filename),
                            param.type.Text,
                            param.name.Text,
                            param.@ref != null));
                }
            }

            foreach (var attr in ctx.prototype().attr()) {
                switch (attr.T.Text) {
                case "noinline":
                    helper.SetAttributes(HelperAttributes.NoInline);
                    break;
                default:
                    throw new NotImplementedException();
                }
            }

            return helper;
        }

        private FunctionBody BuildBody(ArchFileParser.FnbodyContext ctx)
        {
            var body = new FunctionBody(new ASTNode.ASTNodeLocation());

            foreach (var stmt in ctx.statement()) {
                var astStatement = BuildStatement(stmt);
                if (astStatement != null)
                    body.AddStatement(astStatement);
            }

            return body;
        }

        private Statement BuildStatement(ArchFileParser.StatementContext ctx)
        {
            if (ctx.expression_statement() != null) {
                return BuildStatement(ctx.expression_statement());
            } else if (ctx.selection_statement() != null) {
                return BuildStatement(ctx.selection_statement());
            } else if (ctx.fnbody() != null) {
                return BuildBody(ctx.fnbody());
            } else if (ctx.flow_statement() != null) {
                return BuildStatement(ctx.flow_statement());
            } else {
                throw new NotImplementedException();
            }
        }

        private Statement BuildStatement(ArchFileParser.Expression_statementContext ctx)
        {
            if (ctx.expression() != null) {
                return BuildExpression(ctx.expression());
            } else {
                throw new Exception("XX");
                return null;
            }
        }

        private Statement BuildStatement(ArchFileParser.Selection_statementContext ctx)
        {
            if (ctx.if_statement() != null)
                return BuildStatement(ctx.if_statement());
            else if (ctx.switch_statement() != null)
                return BuildStatement(ctx.switch_statement());
            else
                throw new NotImplementedException();
        }

        private Statement BuildStatement(ArchFileParser.If_statementContext ctx)
        {
            return new IfStatement(
                ctx.KW.ToASTLocation(filename),
                BuildExpression(ctx.cond),
                BuildStatement(ctx.tt),
                ctx.ft == null ? null : BuildStatement(ctx.ft));
        }

        private Statement BuildStatement(ArchFileParser.Switch_statementContext ctx)
        {
            var stmt = new SwitchStatement(
                           ctx.KW.ToASTLocation(filename),
                           BuildExpression(ctx.expression())
                       );

            if (ctx.statement().fnbody() != null) {
                foreach (var s in ctx.statement().fnbody().statement()) {
                    if (s.flow_statement() != null && s.flow_statement().S.Text == "case") {
                        stmt.AddCase(
                            BuildConstant(s.flow_statement().constant()),
                            BuildStatement(s.flow_statement().statement()));
                    }
                }
            }

            return stmt;
        }

        private Statement BuildStatement(ArchFileParser.Flow_statementContext ctx)
        {
            if (ctx.S.Text == "return") {
                if (ctx.expression() == null) {
                    return new Return(ctx.S.ToASTLocation(filename));
                } else {
                    return new Return(
                        ctx.S.ToASTLocation(filename),
                        BuildExpression(ctx.expression()));
                }
            } else if (ctx.S.Text == "break") {
                return new Break(ctx.S.ToASTLocation(filename));
            } else {
                throw new NotImplementedException();
            }
        }

        private Expression BuildExpression(ArchFileParser.ExpressionContext ctx)
        {
            if (ctx.declaration() != null) {
                var decl = ctx.declaration();

                if (ctx.assignment_operator() == null) {
                    return new VariableDeclaration(
                        decl.type.ToASTLocation(filename),
                        decl.type.Text,
                        decl.name.Text);
                } else {
                    return new VariableDeclaration(
                        decl.type.ToASTLocation(filename),
                        decl.type.Text,
                        decl.name.Text,
                        BuildExpression(ctx.rvalue));
                }
            } else if (ctx.expr != null) {
                if (ctx.assignment_operator() != null) {
                    switch (ctx.assignment_operator().S.Text) {
                    case "=":
                        return new AssignmentExpression(
                            ctx.assignment_operator().S.ToASTLocation(filename),
                            BuildExpression(ctx.expr),
                            BuildExpression(ctx.rvalue));
                    case "+=":
                        return new AssignmentExpression(
                            ctx.assignment_operator().S.ToASTLocation(filename),
                            BuildExpression(ctx.expr),
                            new AddOperator(
                                ctx.assignment_operator().S.ToASTLocation(filename),
                                AddOperatorType.Add,
                                BuildExpression(ctx.expr),
                                BuildExpression(ctx.rvalue)));
                    case "&=":
                        return new AssignmentExpression(
                            ctx.assignment_operator().S.ToASTLocation(filename),
                            BuildExpression(ctx.expr),
                            new BitwiseOperator(
                                ctx.assignment_operator().S.ToASTLocation(filename),
                                BitwiseOperatorType.And,
                                BuildExpression(ctx.expr),
                                BuildExpression(ctx.rvalue)));
                    case "|=":
                        return new AssignmentExpression(
                            ctx.assignment_operator().S.ToASTLocation(filename),
                            BuildExpression(ctx.expr),
                            new BitwiseOperator(
                                ctx.assignment_operator().S.ToASTLocation(filename),
                                BitwiseOperatorType.Or,
                                BuildExpression(ctx.expr),
                                BuildExpression(ctx.rvalue)));
                    case "^=":
                        return new AssignmentExpression(
                            ctx.assignment_operator().S.ToASTLocation(filename),
                            BuildExpression(ctx.expr),
                            new BitwiseOperator(
                                ctx.assignment_operator().S.ToASTLocation(filename),
                                BitwiseOperatorType.Xor,
                                BuildExpression(ctx.expr),
                                BuildExpression(ctx.rvalue)));
                    case ">>=":
                        return new AssignmentExpression(
                            ctx.assignment_operator().S.ToASTLocation(filename),
                            BuildExpression(ctx.expr),
                            new ShiftOperator(
                                ctx.assignment_operator().S.ToASTLocation(filename),
                                ShiftOperatorType.ShiftRight,
                                BuildExpression(ctx.expr),
                                BuildExpression(ctx.rvalue)));
                    case "<<=":
                        return new AssignmentExpression(
                            ctx.assignment_operator().S.ToASTLocation(filename),
                            BuildExpression(ctx.expr),
                            new ShiftOperator(
                                ctx.assignment_operator().S.ToASTLocation(filename),
                                ShiftOperatorType.ShiftLeft,
                                BuildExpression(ctx.expr),
                                BuildExpression(ctx.rvalue)));
                    default:
                        throw new NotImplementedException();
                    }
                } else {
                    return BuildExpression(ctx.expr);
                }
            } else {
                throw new NotImplementedException();
            }
        }

        private Expression BuildExpression(ArchFileParser.Ternary_expressionContext ctx)
        {
            if (ctx.QMARK() != null) {
                return new TernaryOperator(
                    ctx.QMARK().Symbol.ToASTLocation(filename),
                    BuildExpression(ctx.cond),
                    BuildExpression(ctx.left),
                    BuildExpression(ctx.right)
                );
            } else {
                return BuildExpression(ctx.cond);
            }
        }

        private Expression BuildExpression(ArchFileParser.Log_or_expressionContext ctx)
        {
            if (ctx.P != null) {
                return new LogicalOperator(
                    ctx.P.ToASTLocation(filename),
                    LogicalOperatorType.Or,
                    BuildExpression(ctx.L),
                    BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Log_and_expressionContext ctx)
        {
            if (ctx.P != null) {
                return new LogicalOperator(
                    ctx.P.ToASTLocation(filename),
                    LogicalOperatorType.And,
                    BuildExpression(ctx.L),
                    BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Bit_or_expressionContext ctx)
        {
            if (ctx.P != null) {
                return new BitwiseOperator(ctx.P.ToASTLocation(filename), BitwiseOperatorType.Or, BuildExpression(ctx.L), BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Bit_xor_expressionContext ctx)
        {
            if (ctx.P != null) {
                return new BitwiseOperator(ctx.P.ToASTLocation(filename), BitwiseOperatorType.Xor, BuildExpression(ctx.L), BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Bit_and_expressionContext ctx)
        {
            if (ctx.P != null) {
                return new BitwiseOperator(ctx.P.ToASTLocation(filename), BitwiseOperatorType.And, BuildExpression(ctx.L), BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Equality_expressionContext ctx)
        {
            if (ctx.P != null) {
                return new EqualityOperator(
                    ctx.P.ToASTLocation(filename),
                    EqualityOperator.FromToken(ctx.P.Text),
                    BuildExpression(ctx.L),
                    BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Comparison_expressionContext ctx)
        {
            if (ctx.P != null) {
                throw new NotImplementedException();
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Shift_expressionContext ctx)
        {
            if (ctx.P != null) {
                ShiftOperatorType type;
                switch (ctx.P.Text) {
                case "<<<":
                    type = ShiftOperatorType.RotateLeft;
                    break;
                case ">>>":
                    type = ShiftOperatorType.RotateRight;
                    break;
                case "<<":
                    type = ShiftOperatorType.ShiftLeft;
                    break;
                case ">>":
                    type = ShiftOperatorType.ShiftRight;
                    break;
                default:
                    throw new NotSupportedException();
                }

                return new ShiftOperator(
                    ctx.P.ToASTLocation(filename),
                    type,
                    BuildExpression(ctx.L),
                    BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Add_expressionContext ctx)
        {
            if (ctx.P != null) {
                return new AddOperator(
                    ctx.P.ToASTLocation(filename),
                    AddOperatorType.Add,
                    BuildExpression(ctx.L),
                    BuildExpression(ctx.R));
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Mult_expressionContext ctx)
        {
            if (ctx.P != null) {
                throw new NotImplementedException();
            } else {
                return BuildExpression(ctx.L);
            }
        }

        private Expression BuildExpression(ArchFileParser.Cast_expressionContext ctx)
        {
            if (ctx.type != null) {
                return new CastOperator(ctx.type.ToASTLocation(filename), ctx.type.Text, BuildExpression(ctx.cast_expression()));
            } else {
                return BuildExpression(ctx.expr);
            }
        }

        private Expression BuildExpression(ArchFileParser.Unary_expressionContext ctx)
        {
            if (ctx.postfix_expression() == null) {
                throw new NotImplementedException();
            } else {
                return BuildExpression(ctx.postfix_expression());
            }
        }

        private Expression BuildExpression(ArchFileParser.Postfix_expressionContext ctx)
        {
            if (ctx.postfix_operator() != null) {
                var oper = ctx.postfix_operator();
                if (oper.DOT() != null) {
                    return new StructAccess(
                        oper.DOT().Symbol.ToASTLocation(filename), 
                        BuildExpression(ctx.primary_expression()), 
                        oper.member.Text);
                } else {
                    throw new NotImplementedException();
                }
            } else {
                return BuildExpression(ctx.primary_expression());
            }
        }


        private Expression BuildExpression(ArchFileParser.Primary_expressionContext ctx)
        {
            if (ctx.call_expression() != null) {
                return BuildExpression(ctx.call_expression());
            } else if (ctx.sym != null) {
                return new SymbolExpression(ctx.sym.ToASTLocation(filename), ctx.sym.Text);
            } else if (ctx.imm != null) {
                return BuildConstant(ctx.imm);
            } else if (ctx.LPAREN() != null) {
                return BuildExpression(ctx.expr);
            } else {
                throw new NotImplementedException();
            }
        }

        private BaseConstantExpression BuildConstant(ArchFileParser.ConstantContext ctx)
        {
            if (ctx.FLOAT_CONST() != null) {
                return new FloatConstantExpression(ctx.FLOAT_CONST().Symbol.ToASTLocation(filename), 0);
            } else if (ctx.HEX_VAL() != null) {
                return new IntegerConstantExpression(ctx.HEX_VAL().Symbol.ToASTLocation(filename), 0);
            } else if (ctx.INT_CONST() != null) {
                return new IntegerConstantExpression(ctx.INT_CONST().Symbol.ToASTLocation(filename), long.Parse(ctx.INT_CONST().GetText()));
            } else if (ctx.STRING() != null) {
                return new StringConstantExpression(ctx.STRING().Symbol.ToASTLocation(filename), "");
            } else {
                throw new NotImplementedException();
            }
        }

        private Expression BuildExpression(ArchFileParser.Constant_exprContext ctx)
        {
            throw new NotImplementedException();
        }

        private Expression BuildExpression(ArchFileParser.Call_expressionContext ctx)
        {
            var args = ctx.argument_list().expression();

            switch (ctx.fn.Text) {
            case "read_register_bank":
                if (args.Length != 2) {
                    throw new Exception("Nope");
                }

                return new ReadRegisterBank(
                    ctx.fn.ToASTLocation(filename),
                    BuildExpression(args[0]),
                    BuildExpression(args[1]));

            case "read_register":
                if (args.Length != 1)
                    throw new Exception("Nope");

                return new ReadRegister(
                    ctx.fn.ToASTLocation(filename),
                    BuildExpression(args[0]));

            case "write_register_bank":
                if (args.Length != 3) {
                    throw new Exception("Nope");
                }

                return new WriteRegisterBank(
                    ctx.fn.ToASTLocation(filename),
                    BuildExpression(args[0]),
                    BuildExpression(args[1]),
                    BuildExpression(args[2]));

            case "write_register":
                if (args.Length != 2) {
                    throw new Exception("Nope");
                }

                return new WriteRegister(
                    ctx.fn.ToASTLocation(filename),
                    BuildExpression(args[0]),
                    BuildExpression(args[1]));

            case "read_pc":
                if (args.Length != 0) {
                    throw new Exception("Nope");
                }

                return new ReadPC(ctx.fn.ToASTLocation(filename));
            }

            var call = new FunctionCall(ctx.fn.ToASTLocation(filename), ctx.fn.Text);

            foreach (var arg in args) {
                call.AddArgument(BuildExpression(arg));
            }

            return call;
        }
    }
}