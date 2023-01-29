open System.IO

let input = File.ReadAllText(__SOURCE_DIRECTORY__ + "\input_03.txt")

let letterToNum =
    let smalls =
        [ 'a' .. 'z' ] |> List.map (fun letter -> letter, (int letter - int 'a' + 1))

    let caps =
        [ 'A' .. 'Z' ] |> List.map (fun letter -> letter, (int letter - int 'A' + 27))

    smalls @ caps |> Map.ofList

let solve_a (input: string) =
    input.Split("\n")
    |> Array.map (fun a -> Set(a.Substring(0, a.Length / 2)), Set(a.Substring(a.Length / 2)))
    |> Array.map (fun (first, second) -> (Set.intersect first second).MaximumElement)
    |> Array.map (fun s -> (letterToNum.Item s))
    |> Array.sum


let solve_b (input: string) =
    input.Split("\n")
    |> Array.chunkBySize 3
    |> Array.map (Array.map Set)
    |> Array.map (fun sets -> (Set.intersectMany sets).MaximumElement)
    |> Array.map (fun s -> (letterToNum.Item s))
    |> Array.sum

let solution_a = solve_a input
printfn "%A" solution_a
let solution_b = solve_b input
printfn "%A" solution_b
