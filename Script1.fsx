let xs = [ 1..10 ]

let ys = 
    xs
    |> List.scan (*) 1
    |> List.map(fun x -> x)
