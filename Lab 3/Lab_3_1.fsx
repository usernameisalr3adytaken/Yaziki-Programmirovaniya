open System

// Сообщения
let start_stop_mes () = 
    printfn "\n ____________________________ \nВыбеирте действие \n 1. Начать работу \n 2. Завершить работу"
let option_mes () = 
    printfn "\n Выберите действие, которое  хотите выполнить
    \n 1. Заполнить последовательность строк случайно \n 2. Заполнить последовательность строк руками \n 3. Заполнить последовательность строк из файла \n 4. Вернуться в главное меню" 

// Ввод количества строк в списке
let col_input () = 
    printfn "Введите количество строк в последовательности: "
    
    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "Ошибка ввода"; Int32.MinValue
        | ex -> printfn "Переполнение"; 0

// Ввод символа для вставки
let add_symb () = 
    printfn "Введите символ, который хотите добавить в конец каждой строки: "

    Console.ReadLine()

// Генерация рандомной строки
let randomStr =
    let chars = "АБВГД ЕЁЖЗИ ЙКЛМН ОПРСТ УФХЦЧ ШЩЪЫЬ ЭЮЯаб вгдеё жзийк лмноп рстуф хцчшщ ъыьэю яABCD EFGHI JKLMN OPQRS TUVWU XYZab cdefg hijkl mnopq rstuv wxyz0 12345 6789 "
    let charsLen = chars.Length

    fun len ->
        let randomChars = [|for i in 0..len -> chars.[Random().Next(charsLen)]|]
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

            // Заполняем последовательность строк
            let str_seq = 
                seq {
                    for i in 1 .. Int32.MaxValue do
                        yield randomStr(Random().Next(5, 50))
                }

            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать последовательность %d cтрок" col
            else 
                let append_char = add_symb ()
            
                if append_char.Length > 1 then
                    printfn "Ошибка. Была введена строка, но не символ"
                else
                    // Применяем мап
                    printfn "\nРезультат :"
                    Seq.map (fun i -> i + append_char) str_seq |> Seq.take col |> Seq.iter (printfn "%s")
                    
            next_func()

        // Ввод руками
        | "2" -> 
            let col = col_input()

            // Заполняем проследовательность строк
            let str_seq = 
                seq {
                    for i in 1 .. Int32.MaxValue do
                        printf "Строка №%d: " i
                        yield Console.ReadLine()
                }
            
            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать последовательность %d cтрок" col
            else 
                let append_char = add_symb ()
            
                if append_char.Length > 1 then
                    printfn "Ошибка. Была введена строка, но не символ"
                else
                    // Применяем мап
                    Seq.map (fun i -> i + append_char) str_seq |> Seq.take col |>  Seq.iter (printfn "Результат: %s")

            next_func()

            
        // Ввод из файла
        | "3" -> 
            printfn "Укажите путь к текстовому файлу:"
            
            let filepath = Console.ReadLine()
            
            if IO.File.Exists(filepath) then
                let col = col_input()
                
                // Проверка
                if col <= 0 then 
                    printfn " Невозможно создать последовательность %d cтрок" col
                else 
                    let append_char = add_symb ()
            
                    if append_char.Length > 1 then
                        printfn "Ошибка. Была введена строка, но не символ"
                    else
                        // Заполняем проследовательность строк
                        let str_seq = 
                            seq {
                                for i in 1 .. Int32.MaxValue do
                                    use sr = new IO.StreamReader (filepath)
                                    while not sr.EndOfStream do
                                        yield sr.ReadLine ()
                            }
                        // Применяем мап
                        Seq.map (fun i -> i + append_char) str_seq |> Seq.take col |>  Seq.iter (printfn "Результат: %s")

            else 
                printfn "Некорректно указан путь к файлу"
                        
            next_func()

        
        // Возврат в Main 
        | "4" -> main()

        // Повторный ввод
        | _ ->
            printfn "Ошибка ввода"
            next_func ()  
    ()

main()
