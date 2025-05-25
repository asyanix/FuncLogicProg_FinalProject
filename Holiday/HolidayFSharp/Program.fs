open System
open System.Diagnostics

let rec depth (p: int list) i =
     match List.item i p with
     |(-1) -> 1
     |boss -> 1 + depth p boss

[<EntryPoint>]
let main argv =
    let n = Console.ReadLine() |> int
    let input = Console.ReadLine().Split() |> Array.map int |> Array.toList
    let pList = 0 :: input

    let sw = Stopwatch.StartNew()
    let startMemory = GC.GetTotalMemory(true)

    let maxDepth =
        List.init n (fun i -> i + 1)
        |> List.map (depth pList)
        |> List.max

    printfn "%d" maxDepth

    let endMemory = GC.GetTotalMemory(true)
    sw.Stop()

    let elapsedSec = float sw.ElapsedMilliseconds / 1000.0
    let usedMemoryMb = float (endMemory - startMemory) / 1024.0 / 1024.0

    printfn "\nВремя выполнения: %.3f сек" elapsedSec
    printfn "Использовано памяти: %.3f МБ" usedMemoryMb
    0
