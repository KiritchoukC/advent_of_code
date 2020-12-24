
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

module part2 =
    type State = {
        Adapters: int64 list
        Permutations: (int64 * int64 list) list
    }

    let addPermutation perm state = { state with Permutations = state.Permutations @ [perm]}

    let rec getPermutations adaptersLeft state = 
        match adaptersLeft with 
        | [] -> state.Permutations
        | x::xs -> 
            let temp = xs |> List.takeWhile (fun i -> i <= x + 3L)
            state |> addPermutation (x, temp) |> getPermutations xs

    let rec buildArrangements (permutations: (int64 * int64 list) list) arrangementsDone (arrangements: (int64 list) list) = 
        let (uncomplete, complete) = List.partition (fun x -> (x |> List.last) <> (permutations |> List.last |> fst)) arrangements
        match uncomplete  with
        | [] when arrangements = [] -> 
            let firstPerm = permutations.[0]
            firstPerm
            |> snd
            |> List.map (fun i -> [fst firstPerm; i])
            |> buildArrangements permutations 0
        | [] -> arrangementsDone
        | x::xs -> 
            match x with 
            | [] -> arrangementsDone
            | ys -> 
                let lastVoltage = ys |> List.last
                let nextPerms = permutations |> List.find (fun z -> (fst z) = lastVoltage)
                match (snd nextPerms) with 
                | [] -> buildArrangements permutations complete.Length arrangements
                | np -> 
                    let temp = np |> List.map (fun i -> ys @ [i])
                    buildArrangements permutations (arrangementsDone + complete.Length) (temp@xs)
            

    let solve adapters = 
        adapters
        |> List.map int64
        |> (fun adas -> 0L::adas)
        |> (fun adas -> adas @ [adas |> List.last |> (+) 3L])
        |> (fun adas -> { Adapters = adas; Permutations = [] })
        |> (fun s -> getPermutations s.Adapters s)
        |> (fun p -> buildArrangements p 0 [])
        |> (+) 1


[<EntryPoint>]
let main argv =
    let input =
        "input.txt"
        |> readLines
        |> Array.toList
        |> List.map int
        |> List.sortBy id
    input |> part1.solve |> printfn "%A"
    input |> part2.solve |> printfn "%A"
    0
