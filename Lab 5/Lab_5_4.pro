/*
	Не является решением задачи
*/

DOMAINS
	sl=string*
PREDICATES
	nondeterm change(sl,sl)
	nondeterm take(string,sl,sl)
	nondeterm solution(string,string,string,string,string,string)
CLAUSES
	solution(history, math, bio, geo, angl, franc):-
    		change(["Morozov", "Morozov", "Tokarev", "Tokarev", "Vasiliev", "Vasiliev"],
        		[history, math, bio, geo, angl, franc]) ,
        	"geo" <> "franc",
        	"bio" <> "Tokarev",
        	"frac" <> "Tokarev",
        	"bio" <> "franc",          
        	"bio" <> "Morozov",
        	"bio" <> "math",
        	"angl" <> "math",
        	"angl" <> "Morozov",
        	"math" <> "Morozov".
    
	change(L, [A|L1]) :- take(A, L, L2), change(L2, L1).
	change([], []).
 
	take(A, [A|L], L).
	take(A, [B|L1], [B|L2]) :- take(A, L1, L2), B <> A.
 
goal
	solution(history, math, bio, geo, angl, franc), write(history, math, bio, geo, angl, franc).
	
