open System

// Сообщения
let start_stop_mes () = 
    printfn "\n ____________________________ \nВыбеирте действие \n 1. Начать работу \n 2. Завершить работу"
let option_mes () = 
    printfn "\n Выберите действие, которое  хотите выполнить
    \n 1. Заполнить дерево случайно \n 2. Заполнить дерево руками \n 3. Заполнить дерево из файла \n 4. Вернуться в главное меню" 

// Ввод количества элементов дерева
let col_input () = 
    printfn "Введите количество элементов в дереве: "
    
    try 
       Console.ReadLine() |> int
    with
        | :? System.FormatException -> printfn "Ошибка ввода"; Int32.MinValue
        | ex -> printfn "Переполнение"; 0

// Ввод действительного числа
let num_input () = 
    try 
        Console.ReadLine() |> float
    with
        | :? System.FormatException -> printfn "Ошибка ввода"; Double.MinValue
        | ex -> printfn "Переполнение"; Double.MinValue

// Структура дерева поиска
type SearchTree = 
    | Node of float * SearchTree * SearchTree
    | Empty

// Функция вставки в дерево
let rec insert (leaf: SearchTree, value: float) : SearchTree  =
    match leaf with
        Empty -> 
            Node(value, Empty, Empty)

        | Node (data, left, right) 
            -> 
                if value < data then 
                    Node(data, insert (left, value), right)
                else if value > data then
                    Node(data, left, insert (right, value))
                else
                    leaf
        
        // Это все для вывода
//Обходим левое поддерево, корень, потом правое поддерево
let infix root left right =
    (left ()
     root ()
     right ())
// Выполняем проход по всему дереву
let iterh f t =
    let rec tr t h =
        match t with
            | Node (x, L, R) ->
                infix
                    (fun () -> (f x h)) // обход корня
                    (fun () -> tr L (h + 1)) // обход левого поддерева
                    (fun () -> tr R (h + 1)) // обход прав. поддерева
            | Empty -> ()
 
    tr t 0
//Вывод пробелов в количестве n
let spaces n =
    List.fold (fun s _ -> s + " ") "" [ 0 .. n ]
//Вывод дерева на экран (оно будет повернуто на 90 градусов)
let print_tree T =
    iterh (fun x h -> printfn "%s%.3f" (spaces (h * 3)) x) T

// Фолдина
let rec MyFold (func, root: SearchTree, listik) =
    match root with
    | Empty -> listik
    | Node (_, Empty, Empty) -> listik
    | Node(data, left, right) 
        ->
            let listik = MyFold (func, left, listik)
            let listik = MyFold (func, right, listik)
            func (root, listik, data)

// Проверка, если ровно один из потомков - лист
let check_if_leaf (root: SearchTree): bool = 
    match root with
        | Empty -> false
        | Node (data, left, right)
            -> 
                if left = Empty && right = Empty then
                    true
                else 
                    false

// Функция для проверки, что лист только один
let check_if_only_one (root: SearchTree, listik, data: float) =
    match root with
    | Node(_, left, right) 
        -> 
            if (check_if_leaf(left) && not(check_if_leaf(right)) || not(check_if_leaf(left)) && check_if_leaf(right)) then
                data :: listik
            else 
                listik

    | _ 
        -> listik


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

            // Заполняем список элементов дерева
            let el_list = 
                [
                    for i in 1 .. col do
                        let rdm_val = float(Random().Next(0, 50))
                        let rdm_tail = 10.0 ** float( Random().Next(0, 3))
                        yield rdm_val / rdm_tail
                ]

            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать дерево из %d чисел" col
            else 
                // Заполняем дерево из списка
                let derevo: SearchTree = List.fold (fun root value -> insert(root, value)) Empty el_list
                printfn "\n Дерево"
                print_tree(derevo)

                // Применяем фолд
                printfn "Результат"
                MyFold(check_if_only_one, derevo, []) |> List.iter (printfn "%.3f")
                
                                
            next_func()

        // Ввод руками
        | "2" -> 
            let col = col_input()

            // Заполняем список элементов дерева
            let el_list = 
                [
                    for i in 1 .. col do
                        printf "Элемент №%d: " i
                        yield num_input()
                ]

            // Проверка
            if col <= 0 then 
                printfn " Невозможно создать дерево из %d чисел" col
            else 
                // Заполняем дерево из списка
                let derevo: SearchTree = List.fold (fun root value -> insert(root, value)) Empty el_list
                printfn "\n Дерево"
                print_tree(derevo)

                // Применяем фолд
                printfn "Результат"
                let xxx = MyFold(check_if_only_one, derevo, []) 
                xxx |> List.iter (printfn "%.3f")
                

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
                    ()

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
