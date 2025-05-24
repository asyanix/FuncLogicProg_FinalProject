open System

[<EntryPoint>]
let main _argv =
    let firstLine = Console.ReadLine().Split()
    let n = int firstLine.[0]                   // Количество пользователей
    let m = int firstLine.[1]                   // Количество групп
    let total = n + m

    let g = Array.init total (fun _ -> [])      // Список смежности 
    let component = Array.create total 0        // Номера компонентов связности вершин
    let size = Array.create total 0             // Размеы компоненты связности с номером i
    
    let lines = [ for _ in 1..m -> Console.ReadLine() ] // Список всех строк ввода
    lines
    |> List.iteri (fun i line ->                // Перебираем элементы списка вместе с их индексами
        let tokens = line.Split() |> List.ofArray
        let xs = tokens.Tail |> List.map int |> List.map (fun x -> x - 1)   // Список номеров пользователей группы
        // Для каждого участника добавляем двустороннее ребро: пользователь <-> группа
        xs |> List.iter (fun x ->
            g.[x] <- (i + n) :: g.[x]
            g.[i + n] <- x :: g.[i + n]
        )
    )

    // Обход в глубину 
    let rec dfs cc x =
        match component.[x] with
        | c when c <> 0 -> 0                    // если уже в компоненте, не считаем
        | _ ->
            component.[x] <- cc                 // помечаем текущую вершину
            let baseCount = match x < n with | true -> 1 | false -> 0   // вершина < n => это человек
            g.[x]
            |> List.fold (fun acc y -> acc + dfs cc y) baseCount        // суммируем результаты для соседей

    // Главная функция
    let rec process i cc acc =
        match i < n with
        | false -> List.rev acc
        | true ->
            match component.[i] with
            | 0 ->                       
            // Если не принадлежит ни к какой компоненте, то считаем ее размер 
                let newCc = cc + 1
                let cnt = dfs newCc i
                size.[newCc] <- cnt
                process (i + 1) newCc (cnt :: acc)
            | comp ->
            // Tсли уже принадлежит, просто добавляет размер его компоненты в результат
                process (i + 1) cc (size.[comp] :: acc)

    process 0 0 []
    |> List.iter (printf "%d ")

    0 
