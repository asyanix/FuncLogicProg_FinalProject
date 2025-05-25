open System

let rec depth (p: int list) i =
     match List.item i p with
     |(-1) -> 1
     |boss -> 1 + depth p boss

[<EntryPoint>]
let main argv =
    let n = Console.ReadLine() |> int
    let input = Console.ReadLine().Split() |> Array.map int |> Array.toList
    let pList = 0 :: input

    let maxDepth =
        List.init n (fun i -> i + 1)
        |> List.map (depth pList)
        |> List.max

    printfn "%d" maxDepth
    0
