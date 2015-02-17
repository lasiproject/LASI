// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open LASI.Content
open LASI.Core
open LASI.Core.Heuristics
open LASI.Interop
open LASI.Interop.ResourceManagement

let config = LASI.Utilities.JsonConfig("C:\Users\Aluan\Documents\GitHub\LASI\AspSixApp\resources.json")

TaggerInterop.SharpNLPTagger.set_InjectedConfiguration (config)
LASI.Core.Heuristics.Lexicon.set_InjectedConfiguration (config)

let wrapFile (path : string) = 
    match path.Split('.') |> Array.last with
    | "docx" -> Some(DocXFile path :> IRawTextSource)
    | "doc" -> Some(DocFile path :> IRawTextSource)
    | "txt" -> Some(TxtFile path :> IRawTextSource)
    | "pdf" -> Some(PdfFile path :> IRawTextSource)
    | _ -> None

let (|Entity|Referencer|Verbal|Other|) (lex : ILexical) = 
    match lex with
    | :? IReferencer as a -> Referencer a
    | :? IEntity as a -> Entity a
    | :? IVerbal as a -> Verbal a
    | _ -> Other lex

let run = 
    Lexicon.ResourceLoading.Add(fun e -> printfn "Started loading %s" e.Message)
    Lexicon.ResourceLoaded.Add(fun e -> printfn "Finished loading %s ms elapsed: %d" e.Message e.ElapsedMiliseconds)
    let resourceLoadNotifier = ResourceNotifier()
    let prog = ref 0.0
    // Register callbacks to print operation progress to the terminal
    resourceLoadNotifier.ResourceLoaded.Add(fun e -> 
        prog := e.PercentWorkRepresented + !prog
        printfn "Update: %s \nProgress: %A" e.Message (min !prog 100.0))
    resourceLoadNotifier.ResourceLoading.Add(fun e -> 
        prog := e.PercentWorkRepresented + !prog
        printfn "Update: %s \nProgress: %A" e.Message (min !prog 100.0))
    let orchestrator = 
        AnalysisOrchestrator [ @"C:\Users\Aluan\Documents\GitHub\LASI\LASI.Core.Tests\MockUserFiles\Test paragraph about house fires.txt"
                               |> wrapFile
                               |> Option.get ]
    // Register callbacks to print operation progress to the terminal
    orchestrator.ProgressChanged.Add(fun e -> 
        prog := e.PercentWorkRepresented + !prog
        printfn "Update: %s \nProgress: %A" e.Message (min !prog 100.0))
    let docTask = 
        async { 
            let x = Async.AwaitTask <| orchestrator.ProcessAsync()
            return! x
        }
    
    let docs = Async.RunSynchronously docTask
    for doc in docs do
        let toAttack = BaseVerb("attack")
        let bellicoseVerbals = doc.Verbals |> Seq.filter (fun v -> Similarity.op_Implicit (v.IsSimilarTo toAttack))
        let bellicoseIndividuals = doc.Entities |> Seq.filter (fun e -> bellicoseVerbals |> Seq.contains e.SubjectOf)
        let attackerAttackeePairs = 
            bellicoseVerbals.WithDirectObject().WithSubject() 
            |> Seq.map (fun v -> (v.AggregateSubject, v.AggregateDirectObject))
        attackerAttackeePairs |> Seq.iter (fun e -> printfn "%A" e)
        let rec bind (head : Phrase) = 
            match head with // process the first phrase in the list
            | Entity e -> 
                head.Paragraph.Phrases
                |> Seq.takeWhile ((<>) head)
                |> Seq.filter (function 
                       | Referencer r -> r.IsGenderEquivalentTo e
                       | _ -> false)
                |> Seq.iter (function 
                       | Referencer r -> r.BindAsReferringTo e
                       | _ -> ())
                printfn "Matched %A" head
            | Verbal a -> printfn "Matched %A" a
            | p -> printfn "Unmatched %A" p
        
        // print the document while pattern matching on various Phrase Types and naively binding Pronouns at the phrasal level
        let rec processPhrases phrases = 
            match phrases with
            | x :: xs -> 
                bind x
                processPhrases xs // recursive tail call to continue processing
            | [] -> () // list has been exhausted
        
        processPhrases << Seq.toList <| doc.Phrases //bind and output the document.

[<EntryPoint>]
let main argv = 
    do run
    let rec waitForInput unit = 
        printfn "type exit to quit..."
        match stdin.ReadLine() with
        | "exit" -> 0
        | _ -> waitForInput()
    //    printfn "type exit to exit..."
    waitForInput()
// the last value computed by the function is the exit code
