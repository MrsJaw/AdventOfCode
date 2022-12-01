open System

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

    let star1 (input: string list) = 
        input
        |> Seq.map (fun x-> x.Trim())
        |> splitByEmptyString
        |> Seq.map (Seq.skipWhile(fun x -> String.IsNullOrWhiteSpace(x)))
        |> Seq.map (Seq.map  (fun x -> System.Int32.Parse(x)))
        |> Seq.map Seq.sum
        |> Seq.max
        
    let star2 (input: string list) = 
        input
        |> Seq.map (fun x-> x.Trim())
        |> splitByEmptyString
        |> Seq.map (Seq.skipWhile(fun x -> String.IsNullOrWhiteSpace(x)))
        |> Seq.map (Seq.map  (fun x -> System.Int32.Parse(x)))
        |> Seq.map Seq.sum
        |> Seq.sortDescending
        |> Seq.take(3)
        |> Seq.sum

    let answer = star1 input, star2 input
    printfn "%A" answer
