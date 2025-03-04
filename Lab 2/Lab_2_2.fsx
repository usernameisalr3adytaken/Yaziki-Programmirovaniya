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

let min_len x y = 
    if String.length x < String.length y then
        x 
    else 
        y

// ��������� ��������� ������
let randomStr =
    let chars = "����� Ũ��� ����� ����� ����� ����� ����� ���� ����� ����� ����� ����� ����� �ABCD EFGHI JKLMN OPQRS TUVWU XYZab cdefg hijkl mnopq rstuv wxyz0 12345 6789 "
    let charsLen = chars.Length
    let random = Random()

    fun len ->
        let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
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

            let str_list = 
                [
                    for i in 1 .. col do
                        yield randomStr (Random().Next(5, 50))
                ]
                
            if col <= 0 then 
                printfn " ���������� ������� ������ %d c����" col
                next_func ()
            else 

                printfn "\n��������� :"
                let min_len = 
                    List.fold 
                    (fun n stroka -> 
                        //�������� �� ������������ ����� �������� ������ � �������� ������������� �����
                        if String.length stroka < n then 
                            String.length stroka
                        else n
                    ) 
                   Int32.MaxValue str_list

                str_list |> List.iter (fun stroka x -> if stroka.Length = x then printfn "%s" stroka else ()) min_len
                    
                    
                    
                next_func()

        // ���� ������
        | "2" -> 
            let col = col_input()

            let str_list = 
                [
                    for i in 1 .. col do
                        printf "������ �%d: " i
                        yield Console.ReadLine()
                ]
                
            printfn "\n��������� :"
            List.reduce min_len str_list |> printfn "%s"
            
            next_func()
            
        // ���� �� �����
        | "3" -> 
                        
            next_func()

        
        // ������� � Main 
        | "4" -> main()

        // ��������� ����
        | _ ->
            printfn "������ �����"
            next_func ()  
    ()

main()
