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
    | literal = NUMBER					# literal
    | truth = BOOLEAN					# boolean
    | variable = VARIABLE				# variable
    ;

BOOLEAN
    : 'true'
    | 'false'
    ;
NUMBER
    : ('0'..'9')+ ('.' ('0'..'9')+)?
    ;
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
