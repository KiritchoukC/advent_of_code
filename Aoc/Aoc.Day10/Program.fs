
open System
open System.Linq

type AdaptersCount = {
    Ones: int
    Twos: int
    Threes: int
}

let readLines path = System.IO.File.ReadLines(path).ToArray()

let count (predicate: (int*int list) -> bool) (diff: (int * int list)list) = 
    let countOption = diff |> List.tryFind predicate
    match countOption with 
    | None -> 0
    | Some x -> x |> snd |> List.length |> (+) 1
    

let countAdapters (diff: (int * int list)list) = 
    {
        Ones    = diff |> count (fun x -> (fst x) = 1)
        Twos    = diff |> count (fun x -> (fst x) = 2)
        Threes  = diff |> count (fun x -> (fst x) = 3)
    }

let getResult adaptersCount = adaptersCount.Ones * adaptersCount.Threes

[<EntryPoint>]
let main argv =
    "input.txt"
    |> readLines
    |> Array.toList
    |> List.map int
    |> List.sortBy id
    |> List.pairwise
    |> List.map (fun p -> (snd p) - (fst p))
    |> List.groupBy id
    |> countAdapters
    |> getResult
    |> printf "%A"
    0
