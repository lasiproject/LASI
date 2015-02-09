#r @"C:\Users\Aluan\Documents\GitHub\LASI\Utilities\bin\Debug\LASI.Utilities.dll"

open LASI.Utilities

let scanCs : seq<'a> -> 'b -> ('b -> 'a -> 'b) -> seq<'b> = (fun x y z -> x.Scan(y, new System.Func<'b, 'a, 'b>(z)))
let scanFs = Seq.scan
let scanRx = Observable.scan
let nums = [ 1..Microsoft.FSharp.Core.int.MaxValue / 50 ]
let scannWithCs (nums) = Seq.toList <| scanCs nums 0 (+)
let scannWithFs (nums) = Seq.toList <| scanFs (+) 0 nums

let timef (name, f) = 
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let result = f()
    let time = (float) sw.ElapsedMilliseconds / 1000.0
    (name, time, result)

let test = 
    [| for (name, f) in [ ("scanCs", fun () -> scannWithCs nums)
                          ("scanFs", fun () -> scannWithFs nums) ] -> 
           Async.StartImmediate(async { 
                                    let (name, time, result) = timef (name, f)
                                    printfn "name: %A elapsed %A result: %A\n\n" name time result
                                }) |]

do test |> Array.iter (fun t -> ignore (t))
