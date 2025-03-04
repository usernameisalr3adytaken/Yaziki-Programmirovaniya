open System

// ���������
let start_stop_mes () = 
    printfn "\n ____________________________ \n�������� �������� \n 1. ������ ������ \n 2. ��������� ������"
let option_mes () = 
    printfn "\n �������� ��������, �������  ������ ���������
    \n 1. ��������� ������ ����� �������� \n 2. ��������� ������ ����� ������ \n 3. ��������� ������ ����� �� ����� \n 4. ��������� � ������� ����" 

// ���� ���������� ����� � ������
let col_input () = 
    printfn "������� ���������� ����� � ������: "
    
    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "������ �����"; Int32.MinValue
        | ex -> printfn "������������"; 0

// ���� ������� ��� �������
let add_symb () = 
    printfn "������� ������, ������� ������ �������� � ����� ������ ������: "

    Console.ReadLine()

// ��������� ��������� ������
let randomStr =
    let chars = "����� Ũ��� ����� ����� ����� ����� ����� ���� ����� ����� ����� ����� ����� �ABCD EFGHI JKLMN OPQRS TUVWU XYZab cdefg hijkl mnopq rstuv wxyz0 12345 6789 "
    let charsLen = chars.Length

    fun len ->
        let randomChars = [|for i in 0..len -> chars.[Random().Next(charsLen)]|]
        new String(randomChars)


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

            // ��������� ������ �����
            let str_list = 
                [
                    for i in 1 .. col do
                        yield randomStr (Random().Next(5, 50))
                ]

            // ��������
            if col <= 0 then 
                printfn " ���������� ������� ������ %d c����" col
            else 
                let append_char = add_symb ()
            
                if append_char.Length > 1 then
                    printfn "������. ���� ������� ������, �� �� ������"
                else
                    // ��������� ���
                    printfn "\n��������� :"
                    (List.map (fun i -> i + append_char) str_list) |> List.iter (printfn "%s")
                    
            next_func()

        // ���� ������
        | "2" -> 
            let col = col_input()

            // ��������� ������ �����
            let str_list = 
                [
                    for i in 1 .. col do
                        printf "������ �%d: " i
                        yield Console.ReadLine()
                ]
            
            // ��������
            if col <= 0 then 
                printfn " ���������� ������� ������ %d c����" col
            else 
                let append_char = add_symb ()
            
                if append_char.Length > 1 then
                    printfn "������. ���� ������� ������, �� �� ������"
                else
                    // ��������� ���
                    printfn "\n��������� :"
                    (List.map (fun i -> i + append_char) str_list) |> List.iter (printfn "%s")

            next_func()

            
        // ���� �� �����
        | "3" -> 
            printfn "��� ������� ���� ��� �� ��������. ����������"

            next_func()

        
        // ������� � Main 
        | "4" -> main()

        // ��������� ����
        | _ ->
            printfn "������ �����"
            next_func ()  
    ()

main()
