// Threads Math Expression Parser Grammar
//
// Probably requires ANTLRv4!

grammar Math;

options {
    language = CSharp;
}

expr
    : LPAREN expr RPAREN				# paren
    | (NOT) expr						# negate
    | sign=(ADD | SUB) expr				# sign
    | expr op=(MUL | DIV | MOD) expr	# mulDiv
    | expr op=(ADD | SUB) expr			# addSub
    | expr op=(EQ | NEQ
              | LT | LTEQ
              | GT | GTEQ) expr			# comparison
    | expr op=(CONDAND | CONDOR) expr	# conditional
    | number = NUMBER					# number
	| string = STRING					# string
    | truth = BOOLEAN					# boolean
    | variable = VARIABLE				# variable
    ;

fragment ESCAPED_DOUBLEQUOTE : '\\"';
fragment ESCAPED_SINGLEQUOTE : '\\\'';

BOOLEAN
    : 'true'
    | 'false'
    ;
NUMBER
    : ('0'..'9')+ ('.' ('0'..'9')+)?
    ;
STRING
    : '"' (ESCAPED_DOUBLEQUOTE | ~["\n\r])* '"'
    | '\'' (ESCAPED_SINGLEQUOTE | ~['\n\r])* '\'';
VARIABLE
    : [a-zA-Z]+ [a-zA-Z0-9]*
    ;

ADD : '+';
SUB : '-';
MUL : '*';
DIV : '/';
MOD : '%';
CONDAND
    : '&';
CONDOR
    : '|';
EQ  : '='
    | '==';
NEQ : '!='
    | '<>';
LT  : '<';
LTEQ: '<=';
GT  : '>';
GTEQ: '>=';
NOT : '!';
LPAREN
    : '(';
RPAREN
    : ')';
WHITESPACE
	: (' ' | '\r' | '\n' | '\t') -> channel(HIDDEN)
	;
