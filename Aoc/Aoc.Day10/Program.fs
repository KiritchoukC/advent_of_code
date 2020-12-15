
open System
open System.Linq

let readLines path = System.IO.File.ReadLines(path).ToArray()

module part1 =
    type AdaptersCount = {
        Ones: int
        Twos: int
        Threes: int
    }

    let count number (diff: (int * int list)list) = 
        let countOption = diff |> List.tryFind (fun x -> (fst x) = number)
        match countOption with 
        | None -> 0
        | Some x -> x |> snd |> List.length |> (+) 1
    

    let countAdapters (diff: (int * int list)list) = 
        {
            Ones    = diff |> count 1
            Twos    = diff |> count 2
            Threes  = diff |> count 3
        }

    let getResult adaptersCount = adaptersCount.Ones * adaptersCount.Threes

    let solve adapters =
        adapters
        |> List.pairwise
        |> List.map (fun p -> (snd p) - (fst p))
        |> List.groupBy id
        |> countAdapters
        |> getResult
        

[<EntryPoint>]
let main argv =
    let input =
        "input.txt"
        |> readLines
        |> Array.toList
        |> List.map int
        |> List.sortBy id
    input |> part1.solve |> printf "%A"
    0
