// Threads Math Expression Parser Grammar
//
// Probably requires ANTLRv4!

grammar Math;

options {
    language = CSharp;
}

expr
    : expr op=(CONDAND | CONDOR) expr	# conditional
    | LPAREN expr RPAREN				# paren
    | (NOT) expr						# negate
    | sign=(ADD | SUB) expr				# sign
    | expr op=(MUL | DIV | MOD) expr	# mulDiv
    | expr op=(ADD | SUB) expr			# addSub
    | expr op=(EQ | NEQ
              | LT | LTEQ
              | GT | GTEQ) expr			# comparison
    | literal = (NUMBER
                | BOOLEAN)				# literal
    | variable = VARIABLE				# variable
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