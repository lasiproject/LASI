#r @"C:\Users\Aluan\Documents\GitHub\LASI\Utilities\bin\Debug\LASI.Utilities.dll"

type Tree<'a> = 
    | Tree of Tree<'a> * Tree<'a>
    | Leaf of 'a

let t = Tree(Leaf(1), Tree(Leaf(2), Tree(Leaf(7), Tree(Leaf(4), Leaf(5)))))

let rec bottomToTop t = 
    match t with
    | Tree(a, Leaf(b)) -> b :: bottomToTop a
    | Tree(a, b) -> 
        List.concat ([ bottomToTop b
                       bottomToTop a ])
    | Leaf a -> a :: []

bottomToTop t

let rec topToBottom t = 
    match t with
    | Tree(Leaf(b), a) -> List.Cons(b, topToBottom a)
    | Leaf a -> a :: []
    | Tree(a, b) -> 
        List.concat ([ topToBottom a
                       topToBottom b ])

topToBottom t

let v1 = bottomToTop t
let v2 = topToBottom t
let res = v1.Equals(List.rev v2)

let xs  =
    v1
    |> List.zip
    >> List.map (fun u -> float (fst u) + float (snd u))
    >> List.fold (/) 1.0
    <| v2
