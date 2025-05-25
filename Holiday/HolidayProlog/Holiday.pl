% элемент по индексу
get_at(1, [H|_], H) :- !.
get_at(I, [_|T], Elem) :-
    I1 is I - 1,
    get_at(I1, T, Elem).

% если начальник -1, глубина 1
depth(PList, I, 1) :-
    get_at(I, PList, -1), !.

% глубина на 1 больше глубины начальника
depth(PList, I, D) :-
    get_at(I, PList, Boss),
    depth(PList, Boss, D1),
    D is D1 + 1.

% список глубин для всех сотрудников от 1 до n
all_depths(_, 0, []) :- !.
all_depths(PList, N, [D | Rest]) :-
    depth(PList, N, D),
    N1 is N - 1,
    all_depths(PList, N1, Rest).


max_in_list([H], H) :- !.
max_in_list([H|T], Max) :-
    max_in_list(T, MaxTail),
    (H > MaxTail -> Max = H ; Max = MaxTail).

main :-
    read(N),        
    read(PList),    
    all_depths(PList, N, Depths),
    max_in_list(Depths, Max),
    format("Count of groups: ~d~n", [Max]).
