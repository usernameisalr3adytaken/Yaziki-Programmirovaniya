open System

// Сообщения
let start_stop_mes () = 
    printfn "\n ____________________________ \nВыбеирте действие \n 1. Начать работу \n 2. Завершить работу"
let option_mes () = 
    printfn "\n Выберите действие, которое  хотите выполнить
    \n 1. Заполнить список строк случайно \n 2. Заполнить список строк руками \n 3. Заполнить список строк из файла \n 4. Вернуться в главное меню" 

// Ввод количества строк в списке
let col_input () = 
    printfn "Введите количество строк в списке: "
    
    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "Ошибка ввода"; Int32.MinValue
        | ex -> printfn "Переполнение"; 0

// Ищем саму строку
let min_len x y = 
    if String.length x < String.length y then
        x
    else 
        y

// Генерация рандомной строки
let randomStr =
    let chars = "АБВГД ЕЁЖЗИ ЙКЛМН ОПРСТ УФХЦЧ ШЩЪЫЬ ЭЮЯаб вгдеё жзийк лмноп рстуф хцчшщ ъыьэю яABCD EFGHI JKLMN OPQRS TUVWU XYZab cdefg hijkl mnopq rstuv wxyz0 12345 6789 "
    let charsLen = chars.Length
    let random = Random()

    fun len ->
        let randomChars = [|for i in 0..len -> chars.[random.Next(charsLen)]|]
        new String(randomChars)

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
        // Ввод рандомно
        | "1" -> 
            let col = col_input()

            // Заполняем список строк
            let str_list = 
                [
                    for i in 1 .. col do
                        yield randomStr (Random().Next(5, 50))
                ]
                
            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать список %d cтрок" col
            else 
                printfn "Список строк: "
                str_list |> List.iter (printfn "%s")

                // Применяем фолд                 
                printfn "\nРезультат :"
                List.fold min_len "999999999999999999999999999999999999999999999999999" str_list |> printfn "%s"
                    
                    
                    
            next_func()

        // Ввод руками
        | "2" -> 
            let col = col_input()

            // Заполняем список строк
            let str_list = 
                [
                    for i in 1 .. col do
                        printf "Строка №%d: " i
                        yield Console.ReadLine()
                ]
                
            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать список %d cтрок" col
            else 
                // Применяем фолд                 
                printfn "\nРезультат :"
                List.fold min_len "999999999999999999999999999999999999999999999999999" str_list |> printfn "%s"
            
            next_func()
            
        // Ввод из файла
        | "3" -> 
            printfn "Эта функция пока что не доступна. Извинитесь"
                        
            next_func()

        
        // Возврат в Main 
        | "4" -> main()

        // Повторный ввод
        | _ ->
            printfn "Ошибка ввода"
            next_func ()  
    ()

main()
