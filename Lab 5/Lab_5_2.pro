DOMAINS
	list = integer*

PREDICATES	
	nondeterm count(integer,  list)
	nondeterm create(integer, integer, list)
CLAUSES
	create(_, 0, []) :- !.
	create(N1, 1, [N1|L]) :-
		create(N1, 0, L), !.
	create(N1, N2, [N1|L]) :- 
		N2 >= 1, 
		write("Enter list element: "), readint(N), 
		N3 = N2 - 1,
		create(N, N3, L).
		
	count(0,[]).
	count(N, [H|T]) :- 
		H < 0, 
		Nechet = H mod 2, Nechet <> 0,
		count(N1, T), 
		N = N1 + 1, !.
	count(N, [_|T]) :- 
		count(N, T), !.
	
GOAL
	write("List size: "), readint(Size), write("Enter list element: "), readint(El),create(El, Size, L), write("_______\nList of entered elements: ", L), nl,
	count(Counter, L), write("Number of negative odd numbers: ", Counter), nl, nl.