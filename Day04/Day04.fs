open System.IO

let input = File.ReadAllText(__SOURCE_DIRECTORY__ + "\input_04.txt")

let solve_a (input: string) =
    input.Split("\n")
    |> Array.map (fun a ->
        a.Split ','
        |> Array.map (fun b -> (b.Split '-' |> Array.map int))
        |> Array.map (fun c -> set [ c[0] .. c[1] ]))
    |> Array.filter (fun [| a; b |] -> a.IsSupersetOf b || b.IsSupersetOf a)
    |> Array.length

let solve_b (input: string) =
    input.Split("\n")
    |> Array.map (fun a ->
        a.Split ','
        |> Array.map (fun b -> (b.Split '-' |> Array.map int))
        |> Array.map (fun c -> set [ c[0] .. c[1] ]))
    |> Array.filter (fun [| a; b |] -> not (Set.isEmpty (Set.intersect a b)))
    |> Array.length

let solution_a = solve_a input
printfn "%A" solution_a
let solution_b = solve_b input
printfn "%A" solution_b
