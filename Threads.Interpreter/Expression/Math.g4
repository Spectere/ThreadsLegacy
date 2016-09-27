// Threads Math Expression Parser Grammar
//
// Probably requires ANTLRv4!

grammar Math;

options {
    language = CSharp;
}

expr
    : expr conditionalOp expr
    | LPAREN expr RPAREN
    | (NOT) expr
    | (ADD | SUB) expr
    | expr (MUL | DIV | MOD) expr
    | expr (ADD | SUB) expr
    | expr comparisonOp expr
    | literal = (NUMBER
                | VARIABLE
                | BOOLEAN)
    ;

conditionalOp
    : CONDAND
    | CONDOR
    ;

comparisonOp
    : EQ
    | NEQ
    | LT
    | LTEQ
    | GT
    | GTEQ
    ;

value
    : NUMBER
    | VARIABLE
    | BOOLEAN
    ;

NUMBER
    : ('0'..'9')+ ('.' ('0'..'9')+)?
    ;
VARIABLE
    : [a-zA-Z]+ [a-zA-Z0-9]*
    ;
BOOLEAN
    : 'true'
    | 'false'
    ;

ADD : '+';
SUB : '-';
MUL : '*';
DIV : '/';
MOD : '%';
CONDAND
    : '&'
    | 'and';
CONDOR
    : '|'
    | 'or';
EQ  : '=';
NEQ : '!='
    | '<>';
LT  : '<';
LTEQ: '<=';
GT  : '>';
GTEQ: '>=';
NOT : '!'
    | 'not';
LPAREN
    : '(';
RPAREN
    : ')';
WHITESPACE
	: (' ' | '\r' | '\n' | '\t') -> channel(HIDDEN)
	;