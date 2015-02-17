#r @"C:\Users\Aluan\Documents\GitHub\LASI\Utilities\bin\Debug\LASI.Utilities.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\ExperimentalClientProjects\Experimentation\bin\Debug\System.Reactive.Core.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\ExperimentalClientProjects\Experimentation\bin\Debug\System.Reactive.Interfaces.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\ExperimentalClientProjects\Experimentation\bin\Debug\System.Reactive.Linq.dll"

open LASI.Utilities

type Rx = System.Reactive.Linq.Observable

let scanCs : seq<'a> -> 'b -> ('b -> 'a -> 'b) -> seq<'b> = (fun x y z -> x.Scan(y, new System.Func<'b, 'a, 'b>(z)))
let scanFs = Seq.scan
//let scanRx = Rx.Scan(fun )
let nums = [ 1..Microsoft.FSharp.Core.int.MaxValue / 50 ]
let scannWithCs (nums) = scanCs nums 0 (+) |> Seq.toList
let scannWithFs (nums) = scanFs (+) 0 nums |> Seq.toList

//let scanWithRx (nums) = 
//    scanRx (+) 0 nums
//    |> Rx.ToEnumerable
//    |> Seq.toList
let timefn (name, f) = 
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let result = f()
    let time = (float) sw.ElapsedMilliseconds / 1000.0
    (name, time, result)

let test = 
    [| for (name, f) in [ ("scanCs", fun () -> scannWithCs nums)
                          ("scanFs", fun () -> scannWithFs nums) ] //                          ("scanRx", fun () -> scanWithRx nums) 
                                                                   -> 
           Async.StartImmediate(async { 
                                    let (name, time, result) = timefn (name, f)
                                    printfn "name: %A elapsed %A result: %A\n\n" name time result
                                }) |]

do test |> Array.iter (fun t -> ignore (t))
