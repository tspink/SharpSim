//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from ArchFile.g by ANTLR 4.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6")]
[System.CLSCompliant(false)]
public partial class ArchFileLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, HEX_VAL=41, INT_CONST=42, FLOAT_CONST=43, STRING=44, 
		COLON=45, SEMICOLON=46, LBRACE=47, RBRACE=48, LCHEV=49, RCHEV=50, LPAREN=51, 
		RPAREN=52, LBRACKET=53, RBRACKET=54, EQ=55, PLUS=56, COMMA=57, DOT=58, 
		STAR=59, QMARK=60, AMPERSAND=61, TILDE=62, ARCH=63, ISA=64, FORMAT=65, 
		REGSPACE=66, BANK=67, VECTOR=68, SLOT=69, BEHAVIOUR=70, DECODE=71, INSTRUCTION=72, 
		DEFAULT=73, MATCH=74, DISASM=75, APPEND=76, WHERE=77, HELPER=78, ZEXCEPTION=79, 
		EXPORT=80, IDENT=81, WS=82;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "T__11", "T__12", "T__13", "T__14", "T__15", "T__16", 
		"T__17", "T__18", "T__19", "T__20", "T__21", "T__22", "T__23", "T__24", 
		"T__25", "T__26", "T__27", "T__28", "T__29", "T__30", "T__31", "T__32", 
		"T__33", "T__34", "T__35", "T__36", "T__37", "T__38", "T__39", "DIGIT", 
		"LETTER", "LETTER_OR_UNDERSCORE", "HEXDIGIT", "HEX_VAL", "INT_CONST", 
		"FLOAT_CONST", "STRING", "COLON", "SEMICOLON", "LBRACE", "RBRACE", "LCHEV", 
		"RCHEV", "LPAREN", "RPAREN", "LBRACKET", "RBRACKET", "EQ", "PLUS", "COMMA", 
		"DOT", "STAR", "QMARK", "AMPERSAND", "TILDE", "ARCH", "ISA", "FORMAT", 
		"REGSPACE", "BANK", "VECTOR", "SLOT", "BEHAVIOUR", "DECODE", "INSTRUCTION", 
		"DEFAULT", "MATCH", "DISASM", "APPEND", "WHERE", "HELPER", "ZEXCEPTION", 
		"EXPORT", "IDENT", "WS"
	};


	public ArchFileLexer(ICharStream input)
		: base(input)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'=='", "'&&'", "'noinline'", "'case'", "'break'", "'continue'", 
		"'return'", "'raise'", "'while'", "'do'", "'for'", "'if'", "'else'", "'switch'", 
		"'-'", "'!'", "'++'", "'--'", "'+='", "'-='", "'&='", "'*='", "'/='", 
		"'%='", "'<<='", "'>>='", "'^='", "'|='", "'||'", "'|'", "'^'", "'!='", 
		"'<='", "'>='", "'<<<'", "'<<'", "'>>'", "'>>>'", "'/'", "'%'", null, 
		null, null, null, "':'", "';'", "'{'", "'}'", "'<'", "'>'", "'('", "')'", 
		"'['", "']'", "'='", "'+'", "','", "'.'", "'*'", "'?'", "'&'", "'~'", 
		"'arch'", "'isa'", "'format'", "'regspace'", "'bank'", "'vector'", "'slot'", 
		"'behaviour'", "'decode'", "'instruction'", "'default'", "'match'", "'disasm'", 
		"'append'", "'where'", "'helper'", "'exception'", "'export'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, null, null, null, null, null, null, null, 
		null, null, null, null, null, "HEX_VAL", "INT_CONST", "FLOAT_CONST", "STRING", 
		"COLON", "SEMICOLON", "LBRACE", "RBRACE", "LCHEV", "RCHEV", "LPAREN", 
		"RPAREN", "LBRACKET", "RBRACKET", "EQ", "PLUS", "COMMA", "DOT", "STAR", 
		"QMARK", "AMPERSAND", "TILDE", "ARCH", "ISA", "FORMAT", "REGSPACE", "BANK", 
		"VECTOR", "SLOT", "BEHAVIOUR", "DECODE", "INSTRUCTION", "DEFAULT", "MATCH", 
		"DISASM", "APPEND", "WHERE", "HELPER", "ZEXCEPTION", "EXPORT", "IDENT", 
		"WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "ArchFile.g"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	static ArchFileLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static string _serializedATN = _serializeATN();
	private static string _serializeATN()
	{
	    StringBuilder sb = new StringBuilder();
	    sb.Append("\x3\x430\xD6D1\x8206\xAD2D\x4417\xAEF1\x8D80\xAADD\x2T\x22C");
		sb.Append("\b\x1\x4\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6");
		sb.Append("\x4\a\t\a\x4\b\t\b\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r");
		sb.Append("\t\r\x4\xE\t\xE\x4\xF\t\xF\x4\x10\t\x10\x4\x11\t\x11\x4\x12");
		sb.Append("\t\x12\x4\x13\t\x13\x4\x14\t\x14\x4\x15\t\x15\x4\x16\t\x16\x4");
		sb.Append("\x17\t\x17\x4\x18\t\x18\x4\x19\t\x19\x4\x1A\t\x1A\x4\x1B\t\x1B");
		sb.Append("\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E\x4\x1F\t\x1F\x4 \t ");
		sb.Append("\x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'\t\'\x4(\t");
		sb.Append("(\x4)\t)\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t");
		sb.Append("\x30\x4\x31\t\x31\x4\x32\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4");
		sb.Append("\x35\t\x35\x4\x36\t\x36\x4\x37\t\x37\x4\x38\t\x38\x4\x39\t\x39");
		sb.Append("\x4:\t:\x4;\t;\x4<\t<\x4=\t=\x4>\t>\x4?\t?\x4@\t@\x4\x41\t\x41");
		sb.Append("\x4\x42\t\x42\x4\x43\t\x43\x4\x44\t\x44\x4\x45\t\x45\x4\x46");
		sb.Append("\t\x46\x4G\tG\x4H\tH\x4I\tI\x4J\tJ\x4K\tK\x4L\tL\x4M\tM\x4N");
		sb.Append("\tN\x4O\tO\x4P\tP\x4Q\tQ\x4R\tR\x4S\tS\x4T\tT\x4U\tU\x4V\tV");
		sb.Append("\x4W\tW\x3\x2\x3\x2\x3\x2\x3\x3\x3\x3\x3\x3\x3\x4\x3\x4\x3\x4");
		sb.Append("\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x5\x3\x5\x3\x5\x3\x5");
		sb.Append("\x3\x5\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3");
		sb.Append("\a\x3\a\x3\a\x3\a\x3\a\x3\a\x3\b\x3\b\x3\b\x3\b\x3\b\x3\b\x3");
		sb.Append("\b\x3\t\x3\t\x3\t\x3\t\x3\t\x3\t\x3\n\x3\n\x3\n\x3\n\x3\n\x3");
		sb.Append("\n\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r\x3\r\x3\xE\x3");
		sb.Append("\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3\xF\x3");
		sb.Append("\xF\x3\x10\x3\x10\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12\x3\x13");
		sb.Append("\x3\x13\x3\x13\x3\x14\x3\x14\x3\x14\x3\x15\x3\x15\x3\x15\x3");
		sb.Append("\x16\x3\x16\x3\x16\x3\x17\x3\x17\x3\x17\x3\x18\x3\x18\x3\x18");
		sb.Append("\x3\x19\x3\x19\x3\x19\x3\x1A\x3\x1A\x3\x1A\x3\x1A\x3\x1B\x3");
		sb.Append("\x1B\x3\x1B\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x3\x1D\x3\x1D");
		sb.Append("\x3\x1E\x3\x1E\x3\x1E\x3\x1F\x3\x1F\x3 \x3 \x3!\x3!\x3!\x3\"");
		sb.Append("\x3\"\x3\"\x3#\x3#\x3#\x3$\x3$\x3$\x3$\x3%\x3%\x3%\x3&\x3&\x3");
		sb.Append("&\x3\'\x3\'\x3\'\x3\'\x3(\x3(\x3)\x3)\x3*\x3*\x3+\x3+\x3,\x3");
		sb.Append(",\x5,\x14E\n,\x3-\x3-\x3.\x3.\x3.\x3.\x6.\x156\n.\r.\xE.\x157");
		sb.Append("\x3/\x6/\x15B\n/\r/\xE/\x15C\x3\x30\x5\x30\x160\n\x30\x3\x30");
		sb.Append("\x6\x30\x163\n\x30\r\x30\xE\x30\x164\x3\x30\x3\x30\x6\x30\x169");
		sb.Append("\n\x30\r\x30\xE\x30\x16A\x3\x30\x5\x30\x16E\n\x30\x3\x31\x3");
		sb.Append("\x31\a\x31\x172\n\x31\f\x31\xE\x31\x175\v\x31\x3\x31\x3\x31");
		sb.Append("\x3\x32\x3\x32\x3\x33\x3\x33\x3\x34\x3\x34\x3\x35\x3\x35\x3");
		sb.Append("\x36\x3\x36\x3\x37\x3\x37\x3\x38\x3\x38\x3\x39\x3\x39\x3:\x3");
		sb.Append(":\x3;\x3;\x3<\x3<\x3=\x3=\x3>\x3>\x3?\x3?\x3@\x3@\x3\x41\x3");
		sb.Append("\x41\x3\x42\x3\x42\x3\x43\x3\x43\x3\x44\x3\x44\x3\x44\x3\x44");
		sb.Append("\x3\x44\x3\x45\x3\x45\x3\x45\x3\x45\x3\x46\x3\x46\x3\x46\x3");
		sb.Append("\x46\x3\x46\x3\x46\x3\x46\x3G\x3G\x3G\x3G\x3G\x3G\x3G\x3G\x3");
		sb.Append("G\x3H\x3H\x3H\x3H\x3H\x3I\x3I\x3I\x3I\x3I\x3I\x3I\x3J\x3J\x3");
		sb.Append("J\x3J\x3J\x3K\x3K\x3K\x3K\x3K\x3K\x3K\x3K\x3K\x3K\x3L\x3L\x3");
		sb.Append("L\x3L\x3L\x3L\x3L\x3M\x3M\x3M\x3M\x3M\x3M\x3M\x3M\x3M\x3M\x3");
		sb.Append("M\x3M\x3N\x3N\x3N\x3N\x3N\x3N\x3N\x3N\x3O\x3O\x3O\x3O\x3O\x3");
		sb.Append("O\x3P\x3P\x3P\x3P\x3P\x3P\x3P\x3Q\x3Q\x3Q\x3Q\x3Q\x3Q\x3Q\x3");
		sb.Append("R\x3R\x3R\x3R\x3R\x3R\x3S\x3S\x3S\x3S\x3S\x3S\x3S\x3T\x3T\x3");
		sb.Append("T\x3T\x3T\x3T\x3T\x3T\x3T\x3T\x3U\x3U\x3U\x3U\x3U\x3U\x3U\x3");
		sb.Append("V\x3V\x3V\aV\x221\nV\fV\xEV\x224\vV\x3W\x6W\x227\nW\rW\xEW\x228");
		sb.Append("\x3W\x3W\x2\x2X\x3\x3\x5\x4\a\x5\t\x6\v\a\r\b\xF\t\x11\n\x13");
		sb.Append("\v\x15\f\x17\r\x19\xE\x1B\xF\x1D\x10\x1F\x11!\x12#\x13%\x14");
		sb.Append("\'\x15)\x16+\x17-\x18/\x19\x31\x1A\x33\x1B\x35\x1C\x37\x1D\x39");
		sb.Append("\x1E;\x1F= ?!\x41\"\x43#\x45$G%I&K\'M(O)Q*S\x2U\x2W\x2Y\x2[");
		sb.Append("+],_-\x61.\x63/\x65\x30g\x31i\x32k\x33m\x34o\x35q\x36s\x37u");
		sb.Append("\x38w\x39y:{;}<\x7F=\x81>\x83?\x85@\x87\x41\x89\x42\x8B\x43");
		sb.Append("\x8D\x44\x8F\x45\x91\x46\x93G\x95H\x97I\x99J\x9BK\x9DL\x9FM");
		sb.Append("\xA1N\xA3O\xA5P\xA7Q\xA9R\xABS\xADT\x3\x2\a\x3\x2\x32;\x4\x2");
		sb.Append("\x43\\\x63|\x5\x2\x32;\x43H\x63h\x3\x2$$\x5\x2\v\f\xF\xF\"\"");
		sb.Append("\x232\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2");
		sb.Append("\t\x3\x2\x2\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2");
		sb.Append("\x2\x2\x2\x11\x3\x2\x2\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2");
		sb.Append("\x2\x2\x17\x3\x2\x2\x2\x2\x19\x3\x2\x2\x2\x2\x1B\x3\x2\x2\x2");
		sb.Append("\x2\x1D\x3\x2\x2\x2\x2\x1F\x3\x2\x2\x2\x2!\x3\x2\x2\x2\x2#\x3");
		sb.Append("\x2\x2\x2\x2%\x3\x2\x2\x2\x2\'\x3\x2\x2\x2\x2)\x3\x2\x2\x2\x2");
		sb.Append("+\x3\x2\x2\x2\x2-\x3\x2\x2\x2\x2/\x3\x2\x2\x2\x2\x31\x3\x2\x2");
		sb.Append("\x2\x2\x33\x3\x2\x2\x2\x2\x35\x3\x2\x2\x2\x2\x37\x3\x2\x2\x2");
		sb.Append("\x2\x39\x3\x2\x2\x2\x2;\x3\x2\x2\x2\x2=\x3\x2\x2\x2\x2?\x3\x2");
		sb.Append("\x2\x2\x2\x41\x3\x2\x2\x2\x2\x43\x3\x2\x2\x2\x2\x45\x3\x2\x2");
		sb.Append("\x2\x2G\x3\x2\x2\x2\x2I\x3\x2\x2\x2\x2K\x3\x2\x2\x2\x2M\x3\x2");
		sb.Append("\x2\x2\x2O\x3\x2\x2\x2\x2Q\x3\x2\x2\x2\x2[\x3\x2\x2\x2\x2]\x3");
		sb.Append("\x2\x2\x2\x2_\x3\x2\x2\x2\x2\x61\x3\x2\x2\x2\x2\x63\x3\x2\x2");
		sb.Append("\x2\x2\x65\x3\x2\x2\x2\x2g\x3\x2\x2\x2\x2i\x3\x2\x2\x2\x2k\x3");
		sb.Append("\x2\x2\x2\x2m\x3\x2\x2\x2\x2o\x3\x2\x2\x2\x2q\x3\x2\x2\x2\x2");
		sb.Append("s\x3\x2\x2\x2\x2u\x3\x2\x2\x2\x2w\x3\x2\x2\x2\x2y\x3\x2\x2\x2");
		sb.Append("\x2{\x3\x2\x2\x2\x2}\x3\x2\x2\x2\x2\x7F\x3\x2\x2\x2\x2\x81\x3");
		sb.Append("\x2\x2\x2\x2\x83\x3\x2\x2\x2\x2\x85\x3\x2\x2\x2\x2\x87\x3\x2");
		sb.Append("\x2\x2\x2\x89\x3\x2\x2\x2\x2\x8B\x3\x2\x2\x2\x2\x8D\x3\x2\x2");
		sb.Append("\x2\x2\x8F\x3\x2\x2\x2\x2\x91\x3\x2\x2\x2\x2\x93\x3\x2\x2\x2");
		sb.Append("\x2\x95\x3\x2\x2\x2\x2\x97\x3\x2\x2\x2\x2\x99\x3\x2\x2\x2\x2");
		sb.Append("\x9B\x3\x2\x2\x2\x2\x9D\x3\x2\x2\x2\x2\x9F\x3\x2\x2\x2\x2\xA1");
		sb.Append("\x3\x2\x2\x2\x2\xA3\x3\x2\x2\x2\x2\xA5\x3\x2\x2\x2\x2\xA7\x3");
		sb.Append("\x2\x2\x2\x2\xA9\x3\x2\x2\x2\x2\xAB\x3\x2\x2\x2\x2\xAD\x3\x2");
		sb.Append("\x2\x2\x3\xAF\x3\x2\x2\x2\x5\xB2\x3\x2\x2\x2\a\xB5\x3\x2\x2");
		sb.Append("\x2\t\xBE\x3\x2\x2\x2\v\xC3\x3\x2\x2\x2\r\xC9\x3\x2\x2\x2\xF");
		sb.Append("\xD2\x3\x2\x2\x2\x11\xD9\x3\x2\x2\x2\x13\xDF\x3\x2\x2\x2\x15");
		sb.Append("\xE5\x3\x2\x2\x2\x17\xE8\x3\x2\x2\x2\x19\xEC\x3\x2\x2\x2\x1B");
		sb.Append("\xEF\x3\x2\x2\x2\x1D\xF4\x3\x2\x2\x2\x1F\xFB\x3\x2\x2\x2!\xFD");
		sb.Append("\x3\x2\x2\x2#\xFF\x3\x2\x2\x2%\x102\x3\x2\x2\x2\'\x105\x3\x2");
		sb.Append("\x2\x2)\x108\x3\x2\x2\x2+\x10B\x3\x2\x2\x2-\x10E\x3\x2\x2\x2");
		sb.Append("/\x111\x3\x2\x2\x2\x31\x114\x3\x2\x2\x2\x33\x117\x3\x2\x2\x2");
		sb.Append("\x35\x11B\x3\x2\x2\x2\x37\x11F\x3\x2\x2\x2\x39\x122\x3\x2\x2");
		sb.Append("\x2;\x125\x3\x2\x2\x2=\x128\x3\x2\x2\x2?\x12A\x3\x2\x2\x2\x41");
		sb.Append("\x12C\x3\x2\x2\x2\x43\x12F\x3\x2\x2\x2\x45\x132\x3\x2\x2\x2");
		sb.Append("G\x135\x3\x2\x2\x2I\x139\x3\x2\x2\x2K\x13C\x3\x2\x2\x2M\x13F");
		sb.Append("\x3\x2\x2\x2O\x143\x3\x2\x2\x2Q\x145\x3\x2\x2\x2S\x147\x3\x2");
		sb.Append("\x2\x2U\x149\x3\x2\x2\x2W\x14D\x3\x2\x2\x2Y\x14F\x3\x2\x2\x2");
		sb.Append("[\x151\x3\x2\x2\x2]\x15A\x3\x2\x2\x2_\x15F\x3\x2\x2\x2\x61\x16F");
		sb.Append("\x3\x2\x2\x2\x63\x178\x3\x2\x2\x2\x65\x17A\x3\x2\x2\x2g\x17C");
		sb.Append("\x3\x2\x2\x2i\x17E\x3\x2\x2\x2k\x180\x3\x2\x2\x2m\x182\x3\x2");
		sb.Append("\x2\x2o\x184\x3\x2\x2\x2q\x186\x3\x2\x2\x2s\x188\x3\x2\x2\x2");
		sb.Append("u\x18A\x3\x2\x2\x2w\x18C\x3\x2\x2\x2y\x18E\x3\x2\x2\x2{\x190");
		sb.Append("\x3\x2\x2\x2}\x192\x3\x2\x2\x2\x7F\x194\x3\x2\x2\x2\x81\x196");
		sb.Append("\x3\x2\x2\x2\x83\x198\x3\x2\x2\x2\x85\x19A\x3\x2\x2\x2\x87\x19C");
		sb.Append("\x3\x2\x2\x2\x89\x1A1\x3\x2\x2\x2\x8B\x1A5\x3\x2\x2\x2\x8D\x1AC");
		sb.Append("\x3\x2\x2\x2\x8F\x1B5\x3\x2\x2\x2\x91\x1BA\x3\x2\x2\x2\x93\x1C1");
		sb.Append("\x3\x2\x2\x2\x95\x1C6\x3\x2\x2\x2\x97\x1D0\x3\x2\x2\x2\x99\x1D7");
		sb.Append("\x3\x2\x2\x2\x9B\x1E3\x3\x2\x2\x2\x9D\x1EB\x3\x2\x2\x2\x9F\x1F1");
		sb.Append("\x3\x2\x2\x2\xA1\x1F8\x3\x2\x2\x2\xA3\x1FF\x3\x2\x2\x2\xA5\x205");
		sb.Append("\x3\x2\x2\x2\xA7\x20C\x3\x2\x2\x2\xA9\x216\x3\x2\x2\x2\xAB\x21D");
		sb.Append("\x3\x2\x2\x2\xAD\x226\x3\x2\x2\x2\xAF\xB0\a?\x2\x2\xB0\xB1\a");
		sb.Append("?\x2\x2\xB1\x4\x3\x2\x2\x2\xB2\xB3\a(\x2\x2\xB3\xB4\a(\x2\x2");
		sb.Append("\xB4\x6\x3\x2\x2\x2\xB5\xB6\ap\x2\x2\xB6\xB7\aq\x2\x2\xB7\xB8");
		sb.Append("\ak\x2\x2\xB8\xB9\ap\x2\x2\xB9\xBA\an\x2\x2\xBA\xBB\ak\x2\x2");
		sb.Append("\xBB\xBC\ap\x2\x2\xBC\xBD\ag\x2\x2\xBD\b\x3\x2\x2\x2\xBE\xBF");
		sb.Append("\a\x65\x2\x2\xBF\xC0\a\x63\x2\x2\xC0\xC1\au\x2\x2\xC1\xC2\a");
		sb.Append("g\x2\x2\xC2\n\x3\x2\x2\x2\xC3\xC4\a\x64\x2\x2\xC4\xC5\at\x2");
		sb.Append("\x2\xC5\xC6\ag\x2\x2\xC6\xC7\a\x63\x2\x2\xC7\xC8\am\x2\x2\xC8");
		sb.Append("\f\x3\x2\x2\x2\xC9\xCA\a\x65\x2\x2\xCA\xCB\aq\x2\x2\xCB\xCC");
		sb.Append("\ap\x2\x2\xCC\xCD\av\x2\x2\xCD\xCE\ak\x2\x2\xCE\xCF\ap\x2\x2");
		sb.Append("\xCF\xD0\aw\x2\x2\xD0\xD1\ag\x2\x2\xD1\xE\x3\x2\x2\x2\xD2\xD3");
		sb.Append("\at\x2\x2\xD3\xD4\ag\x2\x2\xD4\xD5\av\x2\x2\xD5\xD6\aw\x2\x2");
		sb.Append("\xD6\xD7\at\x2\x2\xD7\xD8\ap\x2\x2\xD8\x10\x3\x2\x2\x2\xD9\xDA");
		sb.Append("\at\x2\x2\xDA\xDB\a\x63\x2\x2\xDB\xDC\ak\x2\x2\xDC\xDD\au\x2");
		sb.Append("\x2\xDD\xDE\ag\x2\x2\xDE\x12\x3\x2\x2\x2\xDF\xE0\ay\x2\x2\xE0");
		sb.Append("\xE1\aj\x2\x2\xE1\xE2\ak\x2\x2\xE2\xE3\an\x2\x2\xE3\xE4\ag\x2");
		sb.Append("\x2\xE4\x14\x3\x2\x2\x2\xE5\xE6\a\x66\x2\x2\xE6\xE7\aq\x2\x2");
		sb.Append("\xE7\x16\x3\x2\x2\x2\xE8\xE9\ah\x2\x2\xE9\xEA\aq\x2\x2\xEA\xEB");
		sb.Append("\at\x2\x2\xEB\x18\x3\x2\x2\x2\xEC\xED\ak\x2\x2\xED\xEE\ah\x2");
		sb.Append("\x2\xEE\x1A\x3\x2\x2\x2\xEF\xF0\ag\x2\x2\xF0\xF1\an\x2\x2\xF1");
		sb.Append("\xF2\au\x2\x2\xF2\xF3\ag\x2\x2\xF3\x1C\x3\x2\x2\x2\xF4\xF5\a");
		sb.Append("u\x2\x2\xF5\xF6\ay\x2\x2\xF6\xF7\ak\x2\x2\xF7\xF8\av\x2\x2\xF8");
		sb.Append("\xF9\a\x65\x2\x2\xF9\xFA\aj\x2\x2\xFA\x1E\x3\x2\x2\x2\xFB\xFC");
		sb.Append("\a/\x2\x2\xFC \x3\x2\x2\x2\xFD\xFE\a#\x2\x2\xFE\"\x3\x2\x2\x2");
		sb.Append("\xFF\x100\a-\x2\x2\x100\x101\a-\x2\x2\x101$\x3\x2\x2\x2\x102");
		sb.Append("\x103\a/\x2\x2\x103\x104\a/\x2\x2\x104&\x3\x2\x2\x2\x105\x106");
		sb.Append("\a-\x2\x2\x106\x107\a?\x2\x2\x107(\x3\x2\x2\x2\x108\x109\a/");
		sb.Append("\x2\x2\x109\x10A\a?\x2\x2\x10A*\x3\x2\x2\x2\x10B\x10C\a(\x2");
		sb.Append("\x2\x10C\x10D\a?\x2\x2\x10D,\x3\x2\x2\x2\x10E\x10F\a,\x2\x2");
		sb.Append("\x10F\x110\a?\x2\x2\x110.\x3\x2\x2\x2\x111\x112\a\x31\x2\x2");
		sb.Append("\x112\x113\a?\x2\x2\x113\x30\x3\x2\x2\x2\x114\x115\a\'\x2\x2");
		sb.Append("\x115\x116\a?\x2\x2\x116\x32\x3\x2\x2\x2\x117\x118\a>\x2\x2");
		sb.Append("\x118\x119\a>\x2\x2\x119\x11A\a?\x2\x2\x11A\x34\x3\x2\x2\x2");
		sb.Append("\x11B\x11C\a@\x2\x2\x11C\x11D\a@\x2\x2\x11D\x11E\a?\x2\x2\x11E");
		sb.Append("\x36\x3\x2\x2\x2\x11F\x120\a`\x2\x2\x120\x121\a?\x2\x2\x121");
		sb.Append("\x38\x3\x2\x2\x2\x122\x123\a~\x2\x2\x123\x124\a?\x2\x2\x124");
		sb.Append(":\x3\x2\x2\x2\x125\x126\a~\x2\x2\x126\x127\a~\x2\x2\x127<\x3");
		sb.Append("\x2\x2\x2\x128\x129\a~\x2\x2\x129>\x3\x2\x2\x2\x12A\x12B\a`");
		sb.Append("\x2\x2\x12B@\x3\x2\x2\x2\x12C\x12D\a#\x2\x2\x12D\x12E\a?\x2");
		sb.Append("\x2\x12E\x42\x3\x2\x2\x2\x12F\x130\a>\x2\x2\x130\x131\a?\x2");
		sb.Append("\x2\x131\x44\x3\x2\x2\x2\x132\x133\a@\x2\x2\x133\x134\a?\x2");
		sb.Append("\x2\x134\x46\x3\x2\x2\x2\x135\x136\a>\x2\x2\x136\x137\a>\x2");
		sb.Append("\x2\x137\x138\a>\x2\x2\x138H\x3\x2\x2\x2\x139\x13A\a>\x2\x2");
		sb.Append("\x13A\x13B\a>\x2\x2\x13BJ\x3\x2\x2\x2\x13C\x13D\a@\x2\x2\x13D");
		sb.Append("\x13E\a@\x2\x2\x13EL\x3\x2\x2\x2\x13F\x140\a@\x2\x2\x140\x141");
		sb.Append("\a@\x2\x2\x141\x142\a@\x2\x2\x142N\x3\x2\x2\x2\x143\x144\a\x31");
		sb.Append("\x2\x2\x144P\x3\x2\x2\x2\x145\x146\a\'\x2\x2\x146R\x3\x2\x2");
		sb.Append("\x2\x147\x148\t\x2\x2\x2\x148T\x3\x2\x2\x2\x149\x14A\t\x3\x2");
		sb.Append("\x2\x14AV\x3\x2\x2\x2\x14B\x14E\x5U+\x2\x14C\x14E\a\x61\x2\x2");
		sb.Append("\x14D\x14B\x3\x2\x2\x2\x14D\x14C\x3\x2\x2\x2\x14EX\x3\x2\x2");
		sb.Append("\x2\x14F\x150\t\x4\x2\x2\x150Z\x3\x2\x2\x2\x151\x152\a\x32\x2");
		sb.Append("\x2\x152\x153\az\x2\x2\x153\x155\x3\x2\x2\x2\x154\x156\x5Y-");
		sb.Append("\x2\x155\x154\x3\x2\x2\x2\x156\x157\x3\x2\x2\x2\x157\x155\x3");
		sb.Append("\x2\x2\x2\x157\x158\x3\x2\x2\x2\x158\\\x3\x2\x2\x2\x159\x15B");
		sb.Append("\x5S*\x2\x15A\x159\x3\x2\x2\x2\x15B\x15C\x3\x2\x2\x2\x15C\x15A");
		sb.Append("\x3\x2\x2\x2\x15C\x15D\x3\x2\x2\x2\x15D^\x3\x2\x2\x2\x15E\x160");
		sb.Append("\a/\x2\x2\x15F\x15E\x3\x2\x2\x2\x15F\x160\x3\x2\x2\x2\x160\x162");
		sb.Append("\x3\x2\x2\x2\x161\x163\x4\x32;\x2\x162\x161\x3\x2\x2\x2\x163");
		sb.Append("\x164\x3\x2\x2\x2\x164\x162\x3\x2\x2\x2\x164\x165\x3\x2\x2\x2");
		sb.Append("\x165\x166\x3\x2\x2\x2\x166\x168\a\x30\x2\x2\x167\x169\x4\x32");
		sb.Append(";\x2\x168\x167\x3\x2\x2\x2\x169\x16A\x3\x2\x2\x2\x16A\x168\x3");
		sb.Append("\x2\x2\x2\x16A\x16B\x3\x2\x2\x2\x16B\x16D\x3\x2\x2\x2\x16C\x16E");
		sb.Append("\ah\x2\x2\x16D\x16C\x3\x2\x2\x2\x16D\x16E\x3\x2\x2\x2\x16E`");
		sb.Append("\x3\x2\x2\x2\x16F\x173\a$\x2\x2\x170\x172\n\x5\x2\x2\x171\x170");
		sb.Append("\x3\x2\x2\x2\x172\x175\x3\x2\x2\x2\x173\x171\x3\x2\x2\x2\x173");
		sb.Append("\x174\x3\x2\x2\x2\x174\x176\x3\x2\x2\x2\x175\x173\x3\x2\x2\x2");
		sb.Append("\x176\x177\a$\x2\x2\x177\x62\x3\x2\x2\x2\x178\x179\a<\x2\x2");
		sb.Append("\x179\x64\x3\x2\x2\x2\x17A\x17B\a=\x2\x2\x17B\x66\x3\x2\x2\x2");
		sb.Append("\x17C\x17D\a}\x2\x2\x17Dh\x3\x2\x2\x2\x17E\x17F\a\x7F\x2\x2");
		sb.Append("\x17Fj\x3\x2\x2\x2\x180\x181\a>\x2\x2\x181l\x3\x2\x2\x2\x182");
		sb.Append("\x183\a@\x2\x2\x183n\x3\x2\x2\x2\x184\x185\a*\x2\x2\x185p\x3");
		sb.Append("\x2\x2\x2\x186\x187\a+\x2\x2\x187r\x3\x2\x2\x2\x188\x189\a]");
		sb.Append("\x2\x2\x189t\x3\x2\x2\x2\x18A\x18B\a_\x2\x2\x18Bv\x3\x2\x2\x2");
		sb.Append("\x18C\x18D\a?\x2\x2\x18Dx\x3\x2\x2\x2\x18E\x18F\a-\x2\x2\x18F");
		sb.Append("z\x3\x2\x2\x2\x190\x191\a.\x2\x2\x191|\x3\x2\x2\x2\x192\x193");
		sb.Append("\a\x30\x2\x2\x193~\x3\x2\x2\x2\x194\x195\a,\x2\x2\x195\x80\x3");
		sb.Append("\x2\x2\x2\x196\x197\a\x41\x2\x2\x197\x82\x3\x2\x2\x2\x198\x199");
		sb.Append("\a(\x2\x2\x199\x84\x3\x2\x2\x2\x19A\x19B\a\x80\x2\x2\x19B\x86");
		sb.Append("\x3\x2\x2\x2\x19C\x19D\a\x63\x2\x2\x19D\x19E\at\x2\x2\x19E\x19F");
		sb.Append("\a\x65\x2\x2\x19F\x1A0\aj\x2\x2\x1A0\x88\x3\x2\x2\x2\x1A1\x1A2");
		sb.Append("\ak\x2\x2\x1A2\x1A3\au\x2\x2\x1A3\x1A4\a\x63\x2\x2\x1A4\x8A");
		sb.Append("\x3\x2\x2\x2\x1A5\x1A6\ah\x2\x2\x1A6\x1A7\aq\x2\x2\x1A7\x1A8");
		sb.Append("\at\x2\x2\x1A8\x1A9\ao\x2\x2\x1A9\x1AA\a\x63\x2\x2\x1AA\x1AB");
		sb.Append("\av\x2\x2\x1AB\x8C\x3\x2\x2\x2\x1AC\x1AD\at\x2\x2\x1AD\x1AE");
		sb.Append("\ag\x2\x2\x1AE\x1AF\ai\x2\x2\x1AF\x1B0\au\x2\x2\x1B0\x1B1\a");
		sb.Append("r\x2\x2\x1B1\x1B2\a\x63\x2\x2\x1B2\x1B3\a\x65\x2\x2\x1B3\x1B4");
		sb.Append("\ag\x2\x2\x1B4\x8E\x3\x2\x2\x2\x1B5\x1B6\a\x64\x2\x2\x1B6\x1B7");
		sb.Append("\a\x63\x2\x2\x1B7\x1B8\ap\x2\x2\x1B8\x1B9\am\x2\x2\x1B9\x90");
		sb.Append("\x3\x2\x2\x2\x1BA\x1BB\ax\x2\x2\x1BB\x1BC\ag\x2\x2\x1BC\x1BD");
		sb.Append("\a\x65\x2\x2\x1BD\x1BE\av\x2\x2\x1BE\x1BF\aq\x2\x2\x1BF\x1C0");
		sb.Append("\at\x2\x2\x1C0\x92\x3\x2\x2\x2\x1C1\x1C2\au\x2\x2\x1C2\x1C3");
		sb.Append("\an\x2\x2\x1C3\x1C4\aq\x2\x2\x1C4\x1C5\av\x2\x2\x1C5\x94\x3");
		sb.Append("\x2\x2\x2\x1C6\x1C7\a\x64\x2\x2\x1C7\x1C8\ag\x2\x2\x1C8\x1C9");
		sb.Append("\aj\x2\x2\x1C9\x1CA\a\x63\x2\x2\x1CA\x1CB\ax\x2\x2\x1CB\x1CC");
		sb.Append("\ak\x2\x2\x1CC\x1CD\aq\x2\x2\x1CD\x1CE\aw\x2\x2\x1CE\x1CF\a");
		sb.Append("t\x2\x2\x1CF\x96\x3\x2\x2\x2\x1D0\x1D1\a\x66\x2\x2\x1D1\x1D2");
		sb.Append("\ag\x2\x2\x1D2\x1D3\a\x65\x2\x2\x1D3\x1D4\aq\x2\x2\x1D4\x1D5");
		sb.Append("\a\x66\x2\x2\x1D5\x1D6\ag\x2\x2\x1D6\x98\x3\x2\x2\x2\x1D7\x1D8");
		sb.Append("\ak\x2\x2\x1D8\x1D9\ap\x2\x2\x1D9\x1DA\au\x2\x2\x1DA\x1DB\a");
		sb.Append("v\x2\x2\x1DB\x1DC\at\x2\x2\x1DC\x1DD\aw\x2\x2\x1DD\x1DE\a\x65");
		sb.Append("\x2\x2\x1DE\x1DF\av\x2\x2\x1DF\x1E0\ak\x2\x2\x1E0\x1E1\aq\x2");
		sb.Append("\x2\x1E1\x1E2\ap\x2\x2\x1E2\x9A\x3\x2\x2\x2\x1E3\x1E4\a\x66");
		sb.Append("\x2\x2\x1E4\x1E5\ag\x2\x2\x1E5\x1E6\ah\x2\x2\x1E6\x1E7\a\x63");
		sb.Append("\x2\x2\x1E7\x1E8\aw\x2\x2\x1E8\x1E9\an\x2\x2\x1E9\x1EA\av\x2");
		sb.Append("\x2\x1EA\x9C\x3\x2\x2\x2\x1EB\x1EC\ao\x2\x2\x1EC\x1ED\a\x63");
		sb.Append("\x2\x2\x1ED\x1EE\av\x2\x2\x1EE\x1EF\a\x65\x2\x2\x1EF\x1F0\a");
		sb.Append("j\x2\x2\x1F0\x9E\x3\x2\x2\x2\x1F1\x1F2\a\x66\x2\x2\x1F2\x1F3");
		sb.Append("\ak\x2\x2\x1F3\x1F4\au\x2\x2\x1F4\x1F5\a\x63\x2\x2\x1F5\x1F6");
		sb.Append("\au\x2\x2\x1F6\x1F7\ao\x2\x2\x1F7\xA0\x3\x2\x2\x2\x1F8\x1F9");
		sb.Append("\a\x63\x2\x2\x1F9\x1FA\ar\x2\x2\x1FA\x1FB\ar\x2\x2\x1FB\x1FC");
		sb.Append("\ag\x2\x2\x1FC\x1FD\ap\x2\x2\x1FD\x1FE\a\x66\x2\x2\x1FE\xA2");
		sb.Append("\x3\x2\x2\x2\x1FF\x200\ay\x2\x2\x200\x201\aj\x2\x2\x201\x202");
		sb.Append("\ag\x2\x2\x202\x203\at\x2\x2\x203\x204\ag\x2\x2\x204\xA4\x3");
		sb.Append("\x2\x2\x2\x205\x206\aj\x2\x2\x206\x207\ag\x2\x2\x207\x208\a");
		sb.Append("n\x2\x2\x208\x209\ar\x2\x2\x209\x20A\ag\x2\x2\x20A\x20B\at\x2");
		sb.Append("\x2\x20B\xA6\x3\x2\x2\x2\x20C\x20D\ag\x2\x2\x20D\x20E\az\x2");
		sb.Append("\x2\x20E\x20F\a\x65\x2\x2\x20F\x210\ag\x2\x2\x210\x211\ar\x2");
		sb.Append("\x2\x211\x212\av\x2\x2\x212\x213\ak\x2\x2\x213\x214\aq\x2\x2");
		sb.Append("\x214\x215\ap\x2\x2\x215\xA8\x3\x2\x2\x2\x216\x217\ag\x2\x2");
		sb.Append("\x217\x218\az\x2\x2\x218\x219\ar\x2\x2\x219\x21A\aq\x2\x2\x21A");
		sb.Append("\x21B\at\x2\x2\x21B\x21C\av\x2\x2\x21C\xAA\x3\x2\x2\x2\x21D");
		sb.Append("\x222\x5W,\x2\x21E\x221\x5W,\x2\x21F\x221\x5S*\x2\x220\x21E");
		sb.Append("\x3\x2\x2\x2\x220\x21F\x3\x2\x2\x2\x221\x224\x3\x2\x2\x2\x222");
		sb.Append("\x220\x3\x2\x2\x2\x222\x223\x3\x2\x2\x2\x223\xAC\x3\x2\x2\x2");
		sb.Append("\x224\x222\x3\x2\x2\x2\x225\x227\t\x6\x2\x2\x226\x225\x3\x2");
		sb.Append("\x2\x2\x227\x228\x3\x2\x2\x2\x228\x226\x3\x2\x2\x2\x228\x229");
		sb.Append("\x3\x2\x2\x2\x229\x22A\x3\x2\x2\x2\x22A\x22B\bW\x2\x2\x22B\xAE");
		sb.Append("\x3\x2\x2\x2\xE\x2\x14D\x157\x15C\x15F\x164\x16A\x16D\x173\x220");
		sb.Append("\x222\x228\x3\b\x2\x2");
	    return sb.ToString();
	}

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());


}
