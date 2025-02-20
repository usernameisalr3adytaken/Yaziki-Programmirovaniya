open System

// ���������
let start_stop_mes () = 
    printfn "\n ____________________________ \n�������� �������� \n 1. ������ ������ \n 2. ��������� ������"

// ���� + ��������
let N () = 
    printfn "������� ����������� �����: "

    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "������ �����"; Int32.MinValue
        | ex -> printfn "������������"; 0

// ����������
let rec oddDigits num = 
    if ((num % 10) % 2 = 0 && num <> 0) then 
        1 + oddDigits (num / 10)
    elif ((num % 10) % 2 = 1 && num <> 0) then
        oddDigits (num / 10)
    else
        0

// ������ - ����
let rec main () = 
    start_stop_mes ()
    let choice = Console.ReadLine()

    match choice with
        | "1" -> 
            let num = N ()
            // ��������
            if num > 0 then
                let col = oddDigits (num)
                printfn "���������� ������ ���� � ������: %d" col
            else 
                ()

            main ()
            ()
        | "2" -> ()
        | _ ->
            printfn "������ �����"
            main ()
            ()

main ()