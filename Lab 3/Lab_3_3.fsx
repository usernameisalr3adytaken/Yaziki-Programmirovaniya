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
                
                let file_seq = IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories) |> Array.toSeq  
                
                if IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories).Length > 0 then
                    let res = file_seq |> Seq.map (fun (i:string) -> IO.Path.GetFileName(i)) |> Seq.sortWith compare 
                    res |> Seq.iter (printfn "file: %s")

                    Seq.reduce (fun _ i -> i) res |> printfn "Последний по алфавиту файл: %s"

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
