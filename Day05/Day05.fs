module Main

open System.IO
open System.Text.RegularExpressions

type move = int * int * int
type stack = List<char>

let input = File.ReadAllText(__SOURCE_DIRECTORY__ + "\input.txt")

let parseStacks (input: string) : stack[] =
    input.Split("\n 1   2   3   4   5   6   7   8   9\n\n")[0]
    |> fun text -> text.Split '\n'
    |> Array.map (fun line ->
        Seq.toArray line
        |> Array.mapi (fun i x -> (i, x))
        |> Array.filter (fun (i, _) -> ((i - 1) % 4 = 0))
        |> Array.map snd)
    |> Array.transpose
    |> Array.map (fun s -> s |> Array.toList |> List.filter (fun c -> not (c = ' ')))

let pattern = "^move (\d*) from (\d) to (\d)$"

let parseMoves (input: string) : List<move> =
    input.Split("\n 1   2   3   4   5   6   7   8   9\n\n")[1]
    |> fun text -> text.Split '\n'
    |> Array.map (fun line ->
        let result = Regex.Match(line, pattern)

        let amt, start, target =
            int result.Groups.[1].Value, int result.Groups.[2].Value, int result.Groups.[3].Value

        (amt, start - 1, target - 1))
    |> Array.toList

let moveOneByOne ((amt, start, target): move, stacks: stack[]) : stack[] =
    stacks[target] <- (List.rev (List.take amt stacks[start]) @ stacks[target])
    stacks[start] <- stacks[start][amt..]
    stacks

let moveInSets ((amt, start, target): move, stacks: stack[]) : stack[] =
    stacks[target] <- ((List.take amt stacks[start]) @ stacks[target])
    stacks[start] <- stacks[start][amt..]
    stacks

let moveCrates (moves: List<move>, stacks: stack[], moveFunc: move * stack[] -> stack[]) =
    let mutable mutableStacks = stacks

    for move in moves do
        mutableStacks <- moveFunc (move, mutableStacks)

    mutableStacks |> Array.map List.head |> System.String


// these stacks are being mutated by moveOneByOne - i don't want that at all and was very confused to notice,
// but don't know how to fix it momentarily. that's why i'm parsing input twice.
let stacks_a = parseStacks input
let stacks_b = parseStacks input
let moves = parseMoves input
let finalStacks_a = moveCrates (moves, stacks_a, moveOneByOne)
let finalStacks_b = moveCrates (moves, stacks_b, moveInSets)

printfn "%A" stacks_a
printfn "%A" moves
printfn "%A" finalStacks_a
printfn "%A" finalStacks_b
