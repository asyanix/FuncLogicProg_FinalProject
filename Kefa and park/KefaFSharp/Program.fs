open System
open System.Diagnostics

let rec dfs (g: List<List<int>>) (cats: List<int>) m v prev cnt =
    if cnt > m then 0
    elif List.length (List.item v g) = 1 && v <> 1 then 1
    else
        List.item v g
        |> List.filter (fun toV -> toV <> prev)
        |> List.sumBy (fun toV ->
            let catVal = List.item (toV - 1) cats
            let newCnt = if catVal = 0 then 0 else cnt + 1
            dfs g cats m toV v newCnt)

let read () =
    Console.ReadLine().Split(' ') |> List.ofSeq |> List.map int

let n_m = read ()
let n = List.head n_m
let m = List.item 1 n_m

let cats = read ()

let rec readEdges count =
    match count with
    |0 -> []
    |_ ->
        let pair = read ()
        let edge = (List.head pair, List.item 1 pair)
        edge :: readEdges (count - 1)

let edges = readEdges (n - 1)

let sw = Stopwatch.StartNew()
let startMemory = GC.GetTotalMemory(true)

let g =
    let empty = List.init (n + 1) (fun _ -> [])
    edges
    |> List.fold (fun acc (a, b) ->
        acc
        |> List.mapi (fun i lst ->
            if i = a then b :: lst
            elif i = b then a :: lst
            else lst)) empty


let startCnt = if List.head cats = 1 then 1 else 0

let result = dfs g cats m 1 -1 startCnt
printfn "%d" result

let endMemory = GC.GetTotalMemory(true)
sw.Stop()

let elapsedSec = float sw.ElapsedMilliseconds / 1000.0
let usedMemoryMb = float (endMemory - startMemory) / 1024.0 / 1024.0

printfn "\nВремя выполнения: %.3f сек" elapsedSec
printfn "Использовано памяти: %.3f МБ" usedMemoryMb