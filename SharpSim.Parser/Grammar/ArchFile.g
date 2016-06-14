grammar ArchFile;

fragment DIGIT: [0-9];
fragment LETTER: [A-Za-z];
fragment LETTER_OR_UNDERSCORE: LETTER | '_';
fragment HEXDIGIT: [A-Fa-f0-9];

HEX_VAL: '0x' HEXDIGIT+;
INT_CONST: DIGIT+;
FLOAT_CONST: DIGIT+;
STRING: '"' ~('"')* '"';

COLON: ':';
SEMICOLON: ';';
LBRACE: '{';
RBRACE: '}';
LCHEV: '<';
RCHEV: '>';
LPAREN: '(';
RPAREN: ')';
LBRACKET: '[';
RBRACKET: ']';
EQ: '=';
PLUS: '+';
COMMA: ',';
DOT: '.';
STAR: '*';
QMARK: '?';
AMPERSAND: '&';

ARCH: 'arch';
ISA: 'isa';
FORMAT: 'format';
REGSPACE: 'regspace';
BANK: 'bank';
VECTOR: 'vector';
SLOT: 'slot';
BEHAVIOUR: 'behaviour';
HELPER: 'helper';
IDENT: LETTER_OR_UNDERSCORE (LETTER_OR_UNDERSCORE|DIGIT)*;

start: arch_ident def*;

arch_ident: ARCH IDENT SEMICOLON;

def: isa_block_def | regspace_def | behaviour_def | helper_def;

isa_block_def: ISA name=IDENT LBRACE format_def* RBRACE SEMICOLON;

format_def: FORMAT name=IDENT LBRACE format_field_def* RBRACE SEMICOLON;

format_field_def: name=IDENT COLON width=constant_number SEMICOLON;

regspace_def: REGSPACE LBRACE reg_def* RBRACE SEMICOLON;

reg_def: reg_bank_def | vector_reg_def | reg_slot_def;

reg_bank_def: BANK name=IDENT LPAREN 
	type=IDENT COMMA
	count=constant_number COMMA
	width=constant_number COMMA
	stride=constant_number COMMA
	offset=constant_number RPAREN SEMICOLON;

vector_reg_def: VECTOR name=IDENT LPAREN 
	type=IDENT COMMA
	arity=constant_number COMMA
	count=constant_number COMMA
	width=constant_number COMMA
	stride=constant_number COMMA
	offset=constant_number RPAREN SEMICOLON;

reg_slot_def: SLOT name=IDENT LPAREN 
	type=IDENT COMMA
	width=constant_number COMMA
	offset=constant_number RPAREN
	tag=IDENT? SEMICOLON;

behaviour_def: BEHAVIOUR LCHEV isa=IDENT DOT type=IDENT RCHEV name=IDENT fnbody SEMICOLON;

helper_def: HELPER prototype fnbody SEMICOLON;

prototype: rtype=IDENT name=IDENT LPAREN parameter_list? RPAREN attr*;

parameter_list: parameter (COMMA parameter)*;

attr: T='noinline';

parameter: type=IDENT ref=AMPERSAND? name=IDENT;

fnbody: LBRACE statement* RBRACE;

statement : expression_statement | selection_statement | iteration_statement | flow_statement | fnbody;

expression_statement
	: SEMICOLON
	| expression SEMICOLON;
  
flow_statement
	: S='case' constant COLON statement
	| S='default' COLON statement
	| S='break' SEMICOLON
	| S='continue' SEMICOLON
  	| S='return' expression? SEMICOLON;

iteration_statement
	: 'while' LPAREN expression RPAREN statement
	| 'do' statement 'while' LPAREN expression RPAREN SEMICOLON
	| 'for' LPAREN expression? SEMICOLON expression? SEMICOLON expression? RPAREN statement;

selection_statement
	: if_statement
	| switch_statement;

if_statement
	: KW='if' LPAREN cond=expression RPAREN tt=statement ('else' ft=statement)?;
	 
switch_statement
	: KW='switch' LPAREN expression RPAREN statement;

expression
	: declaration (assignment_operator rvalue=ternary_expression)?
	| expr=ternary_expression (assignment_operator rvalue=ternary_expression)?;

constant_expr: log_or_expression; 

argument_list: (expression (COMMA expression )*)?;

constant_number: HEX_VAL | INT_CONST | FLOAT_CONST;

constant: constant_number | STRING;

primary_expression
	: call_expression
	| sym=IDENT
	| imm=constant
	| LPAREN expr=expression RPAREN;

call_expression: fn=IDENT LPAREN argument_list RPAREN;

unary_operator:
    '*'
  | '&'
  | '+'
  | '-'
  | '~'
  | '!';

postfix_expression
	: (primary_expression postfix_operator)
    | primary_expression;
  
postfix_operator
	: LBRACKET expression RBRACKET
	| DOT member=IDENT
	| STAR symbol=IDENT
	| '++'
	| '--';
  
declaration: type=IDENT name=IDENT;
  
unary_expression
	: postfix_expression
	| '++' unary_expression
	| '--' unary_expression
	| unary_operator cast_expression;
  
lvalue: declaration | unary_expression;
  
full_assignment_expression: lvalue assignment_operator ternary_expression;
  
assignment_operator:
  S=EQ
  |S='+='
  |S='-='
  |S='&='
  |S='*='
  |S='/='
  |S='%='
  |S='<<='
  |S='>>='
  |S='^='
  |S='|=';
  
ternary_expression:
	cond=log_or_expression (QMARK left=log_or_expression COLON right=log_or_expression)?;

log_or_expression:
   L=log_and_expression (P='||' R=log_and_expression)*;  

log_and_expression:
   L=bit_or_expression (P='&&' R=bit_or_expression)*;

bit_or_expression:
   L=bit_xor_expression (P='|' R=bit_xor_expression)*;
  
bit_xor_expression:
   L=bit_and_expression (P='^' R=bit_and_expression)*;

bit_and_expression:
   L=equality_expression (P='&' R=equality_expression)*;
  
equality_expression:
   L=comparison_expression (P=('==' | '!=' ) R=comparison_expression)*;

comparison_expression:
   L=shift_expression (P=('<' | '>' | '<=' | '>=') R=shift_expression)*;

shift_expression:
   L=add_expression (P=('<<<' | '<<' | '>>' | '>>>') R=add_expression)*;

add_expression:
   L=mult_expression (P=('+' | '-') R=mult_expression)*;

mult_expression:
   L=cast_expression (P=('*'|'/'|'%') R=cast_expression)*;

cast_expression
	: LPAREN type=IDENT RPAREN cast_expression
	| expr=unary_expression;

WS: [ \t\n\r]+ -> skip;
