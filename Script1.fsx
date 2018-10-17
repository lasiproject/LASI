#r @".\LASI.Utilities\bin\Debug\LASI.Utilities.dll"
#r @".\LASI.Core\bin\Debug\LASI.Core.dll"
#r @".\LASI.Content\bin\Debug\LASI.Content.dll"
#r @".\LASI.Interop\bin\Debug\LASI.Interop.dll"

System.IO.Directory.SetCurrentDirectory(@"C:\Users\Aluan\Documents\GitHub\LASI\")
type TextSource = IRawTextSource

open LASI.Core
open LASI.Content
open LASI.Content.FileTypes
open LASI.Interop
open LASI.Core.LexicalStructures
open Microsoft.FSharp.Compiler.Interactive

try 
    let  init: string ->  ^b -> ^c -> unit = fun a -> fun b  -> fun c -> Configuration.Initialize (a, b, c)
        

    init @".\AspSixApp\config.json" ConfigurationFormat.Json "Data"
with
| :? System.InvalidOperationException as e-> printfn "%A" e.Message

LASI.Utilities.Logger.SetToSilent 
type Source =  
    | Path of string 
    | Paths of seq<string>
    
let analyze (sources : Source) =
    let sources = 
        match sources with
        | Paths p-> p |> Seq.map TxtFile 
        | Path p -> Seq.singleton (TxtFile p)
    let analyzer = AnalysisOrchestrator(sources|>Seq.cast<IRawTextSource>)
    async{ return! Async.AwaitTask(analyzer.ProcessAsync())  }

let display (ls: seq<ILexical>) =
    ls 
    |> Seq.map(fun e->e.Text) 
    |> Seq.fold (fun x e -> x + e) ""
let result = async {
   let! analyzed= analyze(Path(@"C:\Users\Aluan\Documents\GitHub\LASI\DocXFileTest_GetTextTest\Draft_Environmental_Assessment.txt" )) 
   do analyzed |> Seq.iter(fun e-> printf  "%A" (display(e.Lexicals)))
}
Async.StartImmediate result


//let emptyset = Set.empty
let emptyset<'a when 'a:comparison> : Set<'a> = Set.empty
let inline add x y = x + y;
let i = add 2  3
let s = add "Hello " "World"; // produces "Hello World"
