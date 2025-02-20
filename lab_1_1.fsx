open System

// Сообщения
let start_stop_mes () = 
    printfn "\n ____________________________ \nВыбеирте действие \n 1. Начать работу \n 2. Завершить работу"
let option_mes () = 
    printfn "Выберите действие, которое хотите выполнить \n 1. Заполнить и просмотреть список \n 2. Вернуться в главное меню"

// Ввод №1 + проверка
let N () = 
    printfn "Введите количество чисел, для которых хотите найти обратные: "

    try 
       Console.ReadLine() |> int
    with 
        | :? System.FormatException -> printfn "Ошибка ввода"; Int32.MinValue
        | ex -> printfn "Переполнение"; 0

// Заполнение списка
let list_obr () = 
    [
    for i in 1 .. N() do

        // Ввод №2 + проверка
        let a:float = 
            printfn "Значение № %d: " i 

            try 
                Console.ReadLine() |> float
            with
                | ex -> printfn "Ошибка ввода"; 0
        
        // Добавление в список
        if a <> 0 then
            yield a ** (-1)
        else
            ()
    ]

// Вывод списка
let print spisok = 
    printfn "Список обратных чисел: %A" spisok 

// Псевдо - Мейн
let rec main () = 
    start_stop_mes()
    let choice = Console.ReadLine()

    if choice = "1" then
        next_func ()
        ()
    elif choice = "2" then
        ()
    else
        printfn "Ошибка ввода"
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
        printfn "Ошибка ввода"
        next_func ()  
        ()

main()