open System

// ���������
let start_stop_mes () = 
    printfn "\n ____________________________ \n�������� �������� \n 1. ������ ������ \n 2. ��������� ������"
let option_mes () = 
    printfn "\n �������� ��������, �������  ������ ���������
    \n 1. ��������� ������ �������� \n 2. ��������� ������ ������ \n 3. ��������� ������ �� ����� \n 4. ��������� � ������� ����" 

// ���� ���������� ��������� ������
let col_input () = 
    printfn "������� ���������� ��������� � ������: "
    
    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "������ �����"; Int32.MinValue
        | ex -> printfn "������������"; 0

// ���� ��������������� �����
let num_input () = 
    try 
        Console.ReadLine() |> float
    with
        | :? System.FormatException -> printfn "������ �����"; Double.MinValue
        | ex -> printfn "������������"; Double.MinValue

// ��������� ������ ������
type SearchTree = 
    | Node of float * SearchTree * SearchTree
    | Empty

// ������� ������� � ������
let rec insert (leaf: SearchTree, value: float) : SearchTree  =
    match leaf with
        Empty -> 
            Node(value, Empty, Empty)

        | Node (data, left, right) 
            -> 
                if value < data then 
                    Node(data, insert (left, value), right)
                else if value > data then
                    Node(data, left, insert (right, value))
                else
                    leaf
        
        // ��� ��� ��� ������
//������� ����� ���������, ������, ����� ������ ���������
let infix root left right =
    (left ()
     root ()
     right ())
// ��������� ������ �� ����� ������
let iterh f t =
    let rec tr t h =
        match t with
            | Node (x, L, R) ->
                infix
                    (fun () -> (f x h)) // ����� �����
                    (fun () -> tr L (h + 1)) // ����� ������ ���������
                    (fun () -> tr R (h + 1)) // ����� ����. ���������
            | Empty -> ()
 
    tr t 0
//����� �������� � ���������� n
let spaces n =
    List.fold (fun s _ -> s + " ") "" [ 0 .. n ]
//����� ������ �� ����� (��� ����� ��������� �� 90 ��������)
let print_tree T =
    iterh (fun x h -> printfn "%s%.3f" (spaces (h * 3)) x) T

// ������� ����� ����������� �� �����
let destroy_fractional (number:float) = 
    floor(number)

// ��� �� Map
let rec MyMap (func, root:SearchTree): SearchTree = 
    match root with
        | Node (data, left, right) -> Node(func(data), MyMap(func, left), MyMap(func, right))
        | Empty -> Empty

// ������ - ����
let rec main () = 
    start_stop_mes()
    let choice = Console.ReadLine()

    match choice with
        // ��������� � ������ �������
        | "1" -> 
            next_func ()
            ()
        // ��������� ���������
        | "2" -> ()

        // ��������� ����
        | _ ->
            printfn "������ �����"
            main ()
            ()

and next_func () = 
    option_mes()
    let option = Console.ReadLine()

    match option with
        // ���� ��������
        | "1" -> 
            let col = col_input()

            // ��������� ������ ��������� ������
            let el_list = 
                [
                    for i in 1 .. col do
                        let rdm_val = float(Random().Next(0, 50))
                        let rdm_tail = 10.0 ** float( Random().Next(0, 3))
                        yield rdm_val / rdm_tail
                ]

            // ��������
            if col <= 0 then 
                printfn " ���������� ������� ������ �� %d �����" col
            else 
                printfn "%A" el_list
                // ��������� ������ �� ������
                let derevo: SearchTree = List.fold (fun root value -> insert(root, value)) Empty el_list
                printfn "\n ������"
                print_tree(derevo)

                // ��������� ���
                let res = MyMap(destroy_fractional, derevo) 
                printfn "\n ���������"
                print_tree(res)
            
                
                    
            next_func()

        // ���� ������
        | "2" -> 
            let col = col_input()

            // ��������� ������ ��������� ������
            let el_list = 
                [
                    for i in 1 .. col do
                        printf "������� �%d: " i
                        yield num_input()
                ]

            // ��������
            if col <= 0 then 
                printfn " ���������� ������� ������ �� %d �����" col
            else 
                // ��������� ������ �� ������
                let derevo: SearchTree = List.fold (fun root value -> insert(root, value)) Empty el_list
                printfn "\n ������"
                print_tree(derevo)

                // ��������� ���
                let res = MyMap(destroy_fractional, derevo) 
                printfn "\n ���������"
                print_tree(res)
                

            next_func()

            
        // ���� �� �����
        | "3" -> 
            printfn "������� ���� � ���������� �����:"
            
            let filepath = Console.ReadLine()
            
            if IO.File.Exists(filepath) then
                let col = col_input()
                
                // ��������
                if col <= 0 then 
                    printfn " ���������� ������� ������������������ %d c����" col
                else 
                    ()

            else 
                printfn "����������� ������ ���� � �����"
                        
            next_func()

        
        // ������� � Main 
        | "4" -> main()

        // ��������� ����
        | _ ->
            printfn "������ �����"
            next_func ()  
    ()

main()
