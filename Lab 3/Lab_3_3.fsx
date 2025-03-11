open System

// ���������
let start_stop_mes () = 
    printfn "\n ____________________________ \n�������� �������� \n 1. ������ ������ \n 2. ��������� ������"


let rec main () = 
    start_stop_mes()
    let choice = Console.ReadLine()

    match choice with
        // ��������� � ����������
        | "1" -> 
            printfn "������� ����������, � ������� ���������� ���������� �����: "
            let directory = Console.ReadLine()

            if IO.Directory.Exists(directory) then
                
                let listik = IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories) |> Array.toList
                if listik.Length > 0 then
                    let res = listik |> List.map (fun (i:string) -> IO.Path.GetFileName(i)) |> List.sortWith compare 
                    res |> List.iter (printfn "file: %s")
                    res[res.Length-1] |> printfn "��������� �� �������� ����: %s"

                else 
                    printfn "��������� ���������� �����"
            else
                printfn "����������� ������� ����������"


            main ()
            ()
        // ��������� ���������
        | "2" -> ()

        // ��������� ����
        | _ ->
            printfn "������ �����"
            main ()
            ()

main()