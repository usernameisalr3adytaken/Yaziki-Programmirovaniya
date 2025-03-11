open System

// Сообщения
let start_stop_mes () = 
    printfn "\n ____________________________ \nВыбеирте действие \n 1. Начать работу \n 2. Завершить работу"


let rec main () = 
    start_stop_mes()
    let choice = Console.ReadLine()

    match choice with
        // Переходим к выполнению
        | "1" -> 
            printfn "Укажите директорию, в которой необходимо произвести поиск: "
            let directory = Console.ReadLine()

            if IO.Directory.Exists(directory) then
                
                let listik = IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories) |> Array.toList
                if listik.Length > 0 then
                    let res = listik |> List.map (fun (i:string) -> IO.Path.GetFileName(i)) |> List.sortWith compare 
                    res |> List.iter (printfn "file: %s")
                    res[res.Length-1] |> printfn "Последний по алфавиту файл: %s"

                else 
                    printfn "Указанная директория пуста"
            else
                printfn "Некорректно указана директория"


            main ()
            ()
        // Завершаем программу
        | "2" -> ()

        // Повторный ввод
        | _ ->
            printfn "Ошибка ввода"
            main ()
            ()

main()