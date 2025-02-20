open System

// ���������
let start_stop_mes () = 
    printfn "\n ____________________________ \n�������� �������� \n 1. ������ ������ \n 2. ��������� ������"
let option_mes () = 
    printfn "�������� ��������, ������� ������ ��������� \n 1. ��������� � ����������� ������ \n 2. ��������� � ������� ����"

// ���� �1 + ��������
let N () = 
    printfn "������� ���������� �����, ��� ������� ������ ����� ��������: "

    try 
       Console.ReadLine() |> int
    with 
        | :? System.FormatException -> printfn "������ �����"; Int32.MinValue
        | ex -> printfn "������������"; 0

// ���������� ������
let list_obr () = 
    [
    for i in 1 .. N() do

        // ���� �2 + ��������
        let a:float = 
            printfn "�������� � %d: " i 

            try 
                Console.ReadLine() |> float
            with
                | ex -> printfn "������ �����"; 0
        
        // ���������� � ������
        if a <> 0 then
            yield a ** (-1)
        else
            ()
    ]

// ����� ������
let print spisok = 
    printfn "������ �������� �����: %A" spisok 

// ������ - ����
let rec main () = 
    start_stop_mes()
    let choice = Console.ReadLine()

    if choice = "1" then
        next_func ()
        ()
    elif choice = "2" then
        ()
    else
        printfn "������ �����"
        main ()
        ()

and next_func () = 
    option_mes()
    let option = Console.ReadLine()

    if option = "1" then
        let listik = list_obr ()
        print listik
        main()
    elif option = "2" then
        main ()
        ()
    else 
        printfn "������ �����"
        next_func ()  
        ()

main()