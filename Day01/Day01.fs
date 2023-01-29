module advent_of_code_2022_fsharp.Day01

open System
open System.IO

let input = File.ReadAllText(__SOURCE_DIRECTORY__ + "\input_01.txt")

let solution (input: string) (n: int) =
    input.Split("\n\n")
    |> Array.map (fun (s: string) ->
        (s.Split("\n", StringSplitOptions.RemoveEmptyEntries)
         |> Array.map int
         |> Array.sum))
    |> Array.sortByDescending id
    |> Array.take n
    |> Array.sum

let solution_a: int = solution input 1
let solution_b: int = solution input 3

printfn "%A" solution_a
printfn "%A" solution_b
