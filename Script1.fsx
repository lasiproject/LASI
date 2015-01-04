#r @"C:\Users\Aluan\Documents\GitHub\LASI\Utilities\bin\Debug\LASI.Utilities.dll"

open LASI
open LASI.Utilities

let xs = [ 1..10 ]

xs.Aggregate(fun x y index -> x + y + index)
(xs |> List.fold (fun x y -> x + y) 0) + (xs
                                          |> List.map (fun x -> x - 1)
                                          |> List.sum)

let lxs = xs.Length

let folder = 
    fun x y -> 
        printfn "%A %A" x y
        x + y

let q = 
    match xs with
    | x :: xs -> List.fold (folder) x xs
    | [] -> 0

q
//let lys = ys.Length
printfn "\r\n\n%s"
//
//let laxg = xs.Aggregate(folder)
//laxg
