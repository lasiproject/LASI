#r @"C:\Users\Aluan\Documents\GitHub\LASI\Utilities\bin\Debug\LASI.Utilities.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\Core\bin\Debug\LASI.Core.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\Content\bin\Debug\LASI.Content.dll"
#r @"C:\Users\Aluan\Documents\GitHub\LASI\Interop\bin\Debug\LASI.Interop.dll"

System.IO.Directory.SetCurrentDirectory(@"C:\Users\Aluan\Documents\GitHub\LASI\")
type TextSource = IRawTextSource

open LASI.Core
open LASI.Content
open LASI.Interop
open LASI.Core.LexicalStructures

try Configuration.Initialize(@".\AspSixApp\config.json",ConfigFormat.Json,"Data") with | :? System.InvalidOperationException as e-> printfn "%A" e.Message


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
    |> Seq.fold (fun  x e -> x + e) ""
let result = async {
   let! analyzed= analyze(Path(@"C:\Users\Aluan\Documents\GitHub\LASI\DocXFileTest_GetTextTest\Draft_Environmental_Assessment.txt" )) 
   do analyzed |> Seq.iter(fun e-> printf  "%A" (display(e.Lexicals)))
}
Async.StartImmediate result