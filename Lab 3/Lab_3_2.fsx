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
                
            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать список %d cтрок" col
            else 
                // Создаем очень длинную строку для аккумулятора фолда
                let acc = String.init (int(Int16.MaxValue)) (fun i -> i.ToString())
                
                // Применяем фолд                 
                Seq.fold min_len acc <| 
                seq {
                    for i in 1 .. col do
                        let local_strochka = randomStr (Random().Next(5, 50)) 
                        local_strochka |> printfn "Строка №%d: %s" i
                        yield local_strochka
                } |> printfn "min: %s"
                    
                    
                    
            next_func()

        // Ввод руками
        | "2" -> 
            let col = col_input()
                
            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать список %d cтрок" col
            else 
                // Создаем очень длинную строку для аккумулятора фолда
                let acc = String.init (int(Int16.MaxValue)) (fun i -> i.ToString())
                
                // Применяем фолд                 
                Seq.fold min_len acc <| 
                seq {
                    for i in 1 .. col do
                        printf "Строка №%d: " i
                        yield Console.ReadLine()
                } |> printfn "min: %s"
            
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
                    // Создаем очень длинную строку для аккумулятора фолда 
                    let acc = String.init (int(Int16.MaxValue)) (fun i -> i.ToString())

                    Seq.fold min_len acc <| 
                    seq {
                        for i in 1 .. col do
                            use sr = new IO.StreamReader (filepath)
                            while not sr.EndOfStream do
                                yield sr.ReadLine ()
                    } |> printfn "min: %s"

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
