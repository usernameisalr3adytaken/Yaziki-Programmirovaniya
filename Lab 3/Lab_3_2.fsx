open System

// ���������
let start_stop_mes () = 
    printfn "\n ____________________________ \n�������� �������� \n 1. ������ ������ \n 2. ��������� ������"
let option_mes () = 
    printfn "\n �������� ��������, �������  ������ ���������
    \n 1. ��������� ������������������ ����� �������� \n 2. ��������� ������������������ ����� ������ \n 3. ��������� ������������������ ����� �� ����� \n 4. ��������� � ������� ����" 

// ���� ���������� ����� � ������
let col_input () = 
    printfn "������� ���������� ����� � ������������������: "
    
    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "������ �����"; Int32.MinValue
        | ex -> printfn "������������"; 0

// ���� ���� ������
let min_len x y = 
    if String.length x < String.length y then
        x
    else 
        y

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
                
            // ��������
            if col <= 0 then 
                printfn " ���������� ������� ������ %d c����" col
            else 
                // ������� ����� ������� ������ ��� ������������ �����
                let acc = String.init (int(Int16.MaxValue)) (fun i -> i.ToString())
                
                // ��������� ����                 
                Seq.fold min_len acc <| 
                seq {
                    for i in 1 .. col do
                        let local_strochka = randomStr (Random().Next(5, 50)) 
                        local_strochka |> printfn "������ �%d: %s" i
                        yield local_strochka
                } |> printfn "min: %s"
                    
                    
                    
            next_func()

        // ���� ������
        | "2" -> 
            let col = col_input()
                
            // ��������
            if col <= 0 then 
                printfn " ���������� ������� ������ %d c����" col
            else 
                // ������� ����� ������� ������ ��� ������������ �����
                let acc = String.init (int(Int16.MaxValue)) (fun i -> i.ToString())
                
                // ��������� ����                 
                Seq.fold min_len acc <| 
                seq {
                    for i in 1 .. col do
                        printf "������ �%d: " i
                        yield Console.ReadLine()
                } |> printfn "min: %s"
            
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
                    // ������� ����� ������� ������ ��� ������������ ����� 
                    let acc = String.init (int(Int16.MaxValue)) (fun i -> i.ToString())

                    Seq.fold min_len acc <| 
                    seq {
                        for i in 1 .. col do
                            use sr = new IO.StreamReader (filepath)
                            while not sr.EndOfStream do
                                yield sr.ReadLine ()
                    } |> printfn "min: %s"

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
