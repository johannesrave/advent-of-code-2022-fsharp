open System
open System.IO

let input = File.ReadAllText(__SOURCE_DIRECTORY__ + "\input_02.txt")

let pointsPerCounter =
    Map.ofList
        [ ("A", Map.ofList [ "X", 3 + 1; "Y", 6 + 2; "Z", 0 + 3 ])
          ("B", Map.ofList [ "X", 0 + 1; "Y", 3 + 2; "Z", 6 + 3 ])
          ("C", Map.ofList [ "X", 6 + 1; "Y", 0 + 2; "Z", 3 + 3 ]) ]

let pointsPerScoreTable =
    Map.ofList
        [ ("A", Map.ofList [ "X", 3 + 0; "Y", 1 + 3; "Z", 2 + 6 ])
          ("B", Map.ofList [ "X", 1 + 0; "Y", 2 + 3; "Z", 3 + 6 ])
          ("C", Map.ofList [ "X", 2 + 0; "Y", 3 + 3; "Z", 1 + 6 ]) ]

let solution (input: string) (lookUp: Map<string, Map<string, int>>) =
    input.Split("\n")
    |> Array.map (fun (s: string) ->
        (s.Split(" ", StringSplitOptions.RemoveEmptyEntries)
         |> (fun x -> (lookUp.Item x[0]).Item x[1])))
    |> Array.sum

let solution_a: int = solution input pointsPerCounter
printfn "%A" solution_a

let solution_b: int = solution input pointsPerScoreTable
printfn "%A" solution_b
