#r @"C:\Users\Aluan\Documents\GitHub\LASI\Utilities\bin\Debug\LASI.Utilities.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\Content\bin\Debug\LASI.Content.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\Core\bin\Debug\LASI.Core.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\Interop\bin\Debug\LASI.Interop.dll"

module Script =
    open LASI.Interop
    open LASI.Core
    open LASI.Content


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
 


    let map = List.map
    let filter = List.filter
    let fold = List.fold
    let collect = List.collect
    let zip = List.zip
    let iter = List.iter

    let ys = [ for i in 1..20-> [ 1..i ]]

    ys |> collect (fun x->x) |> map (fun x->x.ToString())|> iter stdout.WriteLine
