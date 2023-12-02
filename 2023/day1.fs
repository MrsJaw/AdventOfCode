open System
open System.Linq

// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

module Day1 =

    let readLines filePath = System.IO.File.ReadLines(filePath) 
     
    let input = readLines "Input.txt"  |> Seq.cast<string> |> List.ofSeq //convert file contents to a list by line

    //split array at the "" elements
    let splitByEmptyString input = 
        let i = ref 0
        input
        |> Seq.groupBy(fun x -> 
          if x = "" then incr i
          !i)
        |> Seq.map snd

    let digits = "1234567890";

    let isDigit (input: char) = digits.Contains(input)

    let star1(input: string list) = 
        input
        |> Seq.map (fun x-> x.Trim().ToCharArray())
        |> Seq.map (Seq.filter isDigit)
        |> Seq.map (fun x -> String.Concat(x.First(), x.Last())) 
        |> Seq.sumBy Convert.ToInt32

    let answer = star1 input
    printfn "%A" answer
