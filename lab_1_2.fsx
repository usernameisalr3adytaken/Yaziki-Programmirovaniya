open System

// Сообщения
let start_stop_mes () = 
    printfn "\n ____________________________ \nВыбеирте действие \n 1. Начать работу \n 2. Завершить работу"

// Ввод + проверка
let N () = 
    printfn "Введите натуральное число: "

    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "Ошибка ввода"; Int32.MinValue
        | ex -> printfn "Переполнение"; 0

// Вычисление
let rec oddDigits num = 
    if ((num % 10) % 2 = 0 && num <> 0) then 
        1 + oddDigits (num / 10)
    elif ((num % 10) % 2 = 1 && num <> 0) then
        oddDigits (num / 10)
    else
        0

// Псевдо - Мейн
let rec main () = 
    start_stop_mes ()
    let choice = Console.ReadLine()

    match choice with
        | "1" -> 
            let num = N ()
            // Проверка
            if num > 0 then
                let col = oddDigits (num)
                printfn "Количество четных цифр в записи: %d" col
            else 
                ()

            main ()
            ()
        | "2" -> ()
        | _ ->
            printfn "Ошибка ввода"
            main ()
            ()

main ()