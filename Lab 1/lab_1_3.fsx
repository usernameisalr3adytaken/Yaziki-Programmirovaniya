open System

// Сообщения
let start_stop_mes () = 
    printfn "\n ____________________________ \nВыбеирте действие \n 1. Начать работу \n 2. Завершить работу"
let option_mes () = 
    printfn "
    Примечание: комлпексные числа записываются в формате a + bi, а также c + di. При вводе их с клавиатуры, 
    будут запрашиваться соответсвенно коэффициенты a, b и c, d для первого и второго комплексных чисел, необходимых при вычислениях
    \n \nВыберите действие, которое хотите выполнить 
    \n 1. Сложение комплексных чисел \n 2. Вычитание комплексных чисел \n 3. Умножение комплексных чисел
 4. Деление комплексных чисел \n 5. Возведение в целую неотрицательную степень комплексного числа\n 6. Вернуться в главное меню"

// Ввод действительного числа
let num_input () = 
    try 
        Console.ReadLine() |> float
    with
        | ex -> Double.MinValue

// Ввод целого числа
let step_input () = 
    try 
        Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "Ошибка ввода"; Int32.MinValue
        | ex -> printfn "Переполнение"; Int32.MinValue

// Сумма
let sum (a, b, c, d) = 
    printfn "Результат сложения: %.1f + %.1fi" (a+c) (b+d)

// Разность
let dif (a, b, c, d) = 
    printfn "Результат вычитания: %.1f + %.1fi" (a-c) (b-d)

// Умножение
let mul (a: float, b: float, c: float, d: float) =
    printfn "Результат умножения: %.1f + %.1fi" (a*c - b*d) (a*d + b*c)

// Деление
let div (a: float, b: float, c: float, d: float) = 
    match (c, d) with
    | (0.0, 0.0) -> printfn "Невозможно найти результат деления, второй операнд равен нулю"
    | _ -> printfn "Результат деления: %.3f + %.3fi" ((a*c + b*d)/(c**2 + d**2)) ((b*c - a*d)/(c**2 + d**2))

// Возведение в степень
let pow (a: float, b: float, n: int) = 
    let c = a;
    let d = b;

    // Рекурсия
    let rec powering (a:float, b:float, n:int) : float*float = 
        match n with
            | 0 -> (1, 0)
            | 1 -> (a, b)
            | _ -> powering (a*c - b*d, a*d + b*c, n-1)

    powering (a, b, n)

// Псевдо - Мейн
let rec main () = 
    start_stop_mes()
    let choice = Console.ReadLine()

    match choice with
        // Переходим к выбору функций
        | "1" -> 
            next_func ()
            ()
        // Завершаем программу
        | "2" -> ()

        // Повторный ввод
        | _ ->
            printfn "Ошибка ввода"
            main ()
            ()

and next_func () = 
    option_mes()
    let option = Console.ReadLine()

    match option with
        // Сумма
        | "1" -> 
            printfn "Введите a:"
            let a = num_input ()
            printfn "Введите b:"
            let b = num_input ()
            printfn "Введите c:"
            let c = num_input ()
            printfn "Введите d:"
            let d = num_input ()
            
            // Проверка ввода
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                sum(a, b, c, d)
            else 
                printfn "Ошибка ввода";

            next_func()
            ()

        // Разность
        | "2" -> 
            printfn "Введите a:"
            let a = num_input ()
            printfn "Введите b:"
            let b = num_input ()
            printfn "Введите c:"
            let c = num_input ()
            printfn "Введите d:"
            let d = num_input ()
            
            // Проверка ввода
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                dif(a, b, c, d)
            else 
                printfn "Ошибка ввода";

            next_func()
            
        // Умножение
        | "3" -> 
            printfn "Введите a:"
            let a = num_input ()
            printfn "Введите b:"
            let b = num_input ()
            printfn "Введите c:"
            let c = num_input ()
            printfn "Введите d:"
            let d = num_input ()
            
            // Проверка ввода
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                mul(a, b, c, d)
            else 
                printfn "Ошибка ввода";

            next_func()

        // Деление
        | "4" ->
            printfn "Введите a:"
            let a = num_input ()
            printfn "Введите b:"
            let b = num_input ()
            printfn "Введите c:"
            let c = num_input ()
            printfn "Введите d:"
            let d = num_input ()
            
            // Проверка ввода
            if (a <> Double.MinValue && b <> Double.MinValue && c <> Double.MinValue && d <> Double.MinValue) then            
                div(a, b, c, d)
            else 
                printfn "Ошибка ввода";

            next_func()

        // Возведение в степень
        | "5" -> 
            printfn "Введите a:"
            let a = num_input ()
            printfn "Введите b:"
            let b = num_input ()
            printfn "Введите натуральную неотрицательную степень степень"
            let n = step_input ()
            
            // Провека ввода
            if (a <> Double.MinValue && b <> Double.MinValue && n >= 0) then            
                let (real_, imag_) = pow(a, b, n)
                printfn $"Результат возведения в степень: {real_} + {imag_}i" 
            else 
                ()

            next_func()

        // Возврат в Main 
        | "6" -> main();

        // Повторный ввод
        | _ ->
            printfn "Ошибка ввода"
            next_func ()  
    ()

main()

