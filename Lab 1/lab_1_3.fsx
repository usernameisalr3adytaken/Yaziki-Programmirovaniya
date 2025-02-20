open System

// ���������
let start_stop_mes () = 
    printfn "\n ____________________________ \n�������� �������� \n 1. ������ ������ \n 2. ��������� ������"
let option_mes () = 
    printfn "
    ����������: ����������� ����� ������������ � ������� a + bi, � ����� c + di. ��� ����� �� � ����������, 
    ����� ������������� ������������� ������������ a, b � c, d ��� ������� � ������� ����������� �����, ����������� ��� �����������
    \n \n�������� ��������, ������� ������ ��������� 
    \n 1. �������� ����������� ����� \n 2. ��������� ����������� ����� \n 3. ��������� ����������� �����
 4. ������� ����������� ����� \n 5. ���������� � ����� ��������������� ������� ������������ �����\n 6. ��������� � ������� ����"

// ���� ��������������� �����
let num_input () = 
    try 
        Console.ReadLine() |> float
    with
        | ex -> Double.MinValue

// ���� ������ �����
let step_input () = 
    try 
        Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "������ �����"; Int32.MinValue
        | ex -> printfn "������������"; Int32.MinValue

// �����
let sum (a, b, c, d) = 
    printfn "��������� ��������: %.1f + %.1fi" (a+c) (b+d)

// ��������
let dif (a, b, c, d) = 
    printfn "��������� ���������: %.1f + %.1fi" (a-c) (b-d)

// ���������
let mul (a: float, b: float, c: float, d: float) =
    printfn "��������� ���������: %.1f + %.1fi" (a*c - b*d) (a*d + b*c)

// �������
let div (a: float, b: float, c: float, d: float) = 
    match (c, d) with
    | (0.0, 0.0) -> printfn "���������� ����� ��������� �������, ������ ������� ����� ����"
    | _ -> printfn "��������� �������: %.3f + %.3fi" ((a*c + b*d)/(c**2 + d**2)) ((b*c - a*d)/(c**2 + d**2))

// ���������� � �������
let pow (a: float, b: float, n: int) = 
    let c = a;
    let d = b;

    // ��������
    let rec powering (a:float, b:float, n:int) : float*float = 
        match n with
            | 0 -> (1, 0)
            | 1 -> (a, b)
            | _ -> powering (a*c - b*d, a*d + b*c, n-1)

    powering (a, b, n)

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
        // �����
        | "1" -> 
            printfn "������� a:"
            let a = num_input ()
            printfn "������� b:"
            let b = num_input ()
            printfn "������� c:"
            let c = num_input ()
            printfn "������� d:"
            let d = num_input ()
            
            // �������� �����
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                sum(a, b, c, d)
            else 
                printfn "������ �����";

            next_func()
            ()

        // ��������
        | "2" -> 
            printfn "������� a:"
            let a = num_input ()
            printfn "������� b:"
            let b = num_input ()
            printfn "������� c:"
            let c = num_input ()
            printfn "������� d:"
            let d = num_input ()
            
            // �������� �����
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                dif(a, b, c, d)
            else 
                printfn "������ �����";

            next_func()
            
        // ���������
        | "3" -> 
            printfn "������� a:"
            let a = num_input ()
            printfn "������� b:"
            let b = num_input ()
            printfn "������� c:"
            let c = num_input ()
            printfn "������� d:"
            let d = num_input ()
            
            // �������� �����
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                mul(a, b, c, d)
            else 
                printfn "������ �����";

            next_func()

        // �������
        | "4" ->
            printfn "������� a:"
            let a = num_input ()
            printfn "������� b:"
            let b = num_input ()
            printfn "������� c:"
            let c = num_input ()
            printfn "������� d:"
            let d = num_input ()
            
            // �������� �����
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                div(a, b, c, d)
            else 
                printfn "������ �����";

            next_func()

        // ���������� � �������
        | "5" -> 
            printfn "������� a:"
            let a = num_input ()
            printfn "������� b:"
            let b = num_input ()
            printfn "������� ����������� ��������������� ������� �������"
            let n = step_input ()
            
            // ������� �����
            if (a <> Double.MinValue && b <> Double.MinValue && n >= 0) then            
                let (real_, imag_) = pow(a, b, n)
                printfn $"��������� ���������� � �������: {real_} + {imag_}i" 
            else 
                ()

            next_func()

        // ������� � Main 
        | "6" -> main();

        // ��������� ����
        | _ ->
            printfn "������ �����"
            next_func ()  
    ()

main()

