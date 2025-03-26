PREDICATES
	nondeterm min(integer, integer, integer)
	nondeterm num_min(integer, integer, integer)
	nondeterm num_max(integer, integer, integer)
CLAUSES
	num_min (X, Y, X) :- X < Y, !.
	num_min (_, Y, Y).
	
	num_max (X, Y, X) :- X > Y, !.
	num_max (_, Y, Y).
	
	min(0, Min_dig, Max_dig) :- 
		write("Min: ", Min_dig, ", Max: ", Max_dig), nl, nl, !.
	min(Num, Min_dig, Max_dig) :- 
		Argument1 = Num mod 10, 
		num_min(Min_dig, Argument1, Minik), 
		num_max(Max_dig, Argument1, Maxik), 
		Argument2 = Num div 10, 
		min(Argument2, Minik, Maxik).
	
GOAL
	write("Num = "), readint(Number), min(Number, 9, 0).
