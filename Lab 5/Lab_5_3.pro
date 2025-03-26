DOMAINS
	list = integer*
	
PREDICATES	
	nondeterm create(integer, list)
	nondeterm mul_A_B(list, list, list)
	nondeterm not_set(list, list, list)
	nondeterm sum_A_B(list, list, list)
	nondeterm member(integer, list)
	
CLAUSES
	create(0, []) :- !.
	create(Size, [N|Tail]) :- 
		Size > 0, 
		write("Enter list element: "), readint(N), 
		S_Size = Size - 1,
		create(S_Size, Tail).
		
	member(Elem, [Elem|_]).
	member(Elem, [_|Tail]) :- member(Elem,Tail).
 
	sum_A_B([], A, A).
	sum_A_B([A|A_tail], B, C) :- member(A, B), !, sum_A_B(A_tail, B, C).
	sum_A_B([A|A_tail], B, [A|C_tail]) :- sum_A_B(A_tail, B, C_tail).
	
	mul_A_B([], _, []).
	mul_A_B([A|A_tail], B, [A|C_tail]) :- member(A, B), !, mul_A_B(A_tail, B, C_tail). 
 	mul_A_B([_|A_tail], B, C):- mul_A_B(A_tail, B, C). 
 	
 	not_set([], _, []):-!.
	not_set([A|A_tail], B, C):- member(A, B), !, not_set(A_tail, B, C).
	not_set([A|A_tail], B, [A|C_tail]):- not_set(A_tail, B, C_tail).
		
	
GOAL
	write("1. Set Universum size and elements: "), readint(Uni_Size), create(Uni_Size, Universum),
	write("\n2. Set size of set A and elements: "), readint(A_Size), create(A_Size, A),
	write("\n3. Set size of set B and elements: "), readint(B_Size), create(B_Size, B),
	write("\nDe Morgan's Law: not(A^B) = not(A) + not(B)"), nl, 
	
	mul_A_B(A, B, Multik), write("A^B = ", Multik), nl, nl,
	not_set(Universum, Multik, Not_A_B), write("not(A^B) = ", Not_A_B), nl, nl,
	
	not_set(Universum, A, Not_A), not_set(Universum, B, Not_B), 
	sum_A_B(Not_A, Not_B, Sumka), write("not(A) + not(B) = ", Sumka), nl.