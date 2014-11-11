// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module LASI.FSharpExperimentation.program

open LASI.ContentSystem
open LASI.Core
open LASI.Core.Heuristics
open LASI.Interop
open LASI.Interop.ResourceMonitoring
open System.Linq

let wrapFile (s : string) = 
    match s.Split('.').Last() with
    | "docx" -> DocXFile s :> IRawTextSource
    | "doc" -> DocFile s :> IRawTextSource
    | "txt" -> TxtFile s :> IRawTextSource
    | "pdf" -> PdfFile s :> IRawTextSource
    | _ -> null

[<EntryPoint>]
let main argv = 
    Lookup.ResourceLoading.Add(fun e -> printfn "Started loading %s" e.Message)
    Lookup.ResourceLoaded.Add(fun e -> printfn "Finished loading %s ms elapsed: %d" e.Message e.ElapsedMiliseconds)
    let resourceLoadNotifier = ResourceNotifier()
    let prog = ref 0.0
    // Register callbacks to print operation progress to the terminal
    resourceLoadNotifier.ResourceLoaded.Add(fun e -> 
        prog := e.PercentWorkRepresented + !prog
        printfn "Update: %s \nProgress: %A" e.Message (min !prog 100.0))
    resourceLoadNotifier.ResourceLoading.Add(fun e -> 
        prog := e.PercentWorkRepresented + !prog
        printfn "Update: %s \nProgress: %A" e.Message (min !prog 100.0))
    let orchestrator = AnalysisOrchestrator [ //         wrapFile @"C:\Users\Aluan\Desktop\Documents\sec22.txt"
                                              wrapFile @"C:\Users\Aluan\Desktop\Documents\cats.txt" ]
    // Register callbacks to print operation progress to the terminal
    orchestrator.ProgressChanged.Add(fun e -> 
        prog := e.PercentWorkRepresented + !prog
        printfn "Update: %s \nProgress: %A" e.Message (min !prog 100.0))
    let docTask = 
        async { 
            let b = Async.AwaitTask(orchestrator.ProcessAsync())
            return! b
        }
    
    let docs = Async.RunSynchronously(docTask)
    for doc in docs do
        let toAttack = SimpleVerb("attack")
        let bellicoseVerbals = doc.Verbals |> Seq.filter (fun v -> SimilarityResult.op_Implicit (v.IsSimilarTo toAttack))
        let bellicoseIndividuals = doc.Entities |> Seq.filter (fun e -> bellicoseVerbals.Contains e.SubjectOf)
        let attackerAttackeePairs = 
            bellicoseVerbals.WithDirectObject().WithSubject() |> Seq.map (fun v -> (v.AggregateSubject, v.AggregateDirectObject))
        do Seq.iter (fun e -> printfn "%A" e) attackerAttackeePairs
        let (|Entity|Referencer|Action|Other|) (lex : ILexical) = 
            match lex with
            | :? IReferencer as r -> Referencer r
            | :? IEntity as e -> Entity e
            | :? IVerbal as a -> Action a
            | _ -> Other lex
        
        let rec bind (head : Phrase) = 
            match head with // process the first phrase in the list
            | Entity e -> 
                head.Paragraph.Phrases
                |> Seq.takeWhile (fun x -> not (x = head))
                |> Seq.filter (fun x -> 
                       match x with
                       | Referencer r -> r.IsGenderEquivalentTo e
                       | _ -> false)
                |> Seq.iter (fun x -> 
                       match x with
                       | Referencer r -> r.BindAsReferringTo e
                       | _ -> ())
                printfn "Matched %A" head
            | Action a -> printfn "Matched %A" a
            | p -> printfn "Unmatched %A" p
        
        // print the document while pattern matching on various Phrase Types and naively binding Pronouns at the phrasal level
        let rec processPhrases (phrases : Phrase List) = 
            match phrases with
            | head :: tail -> 
                bind head
                processPhrases tail // recursive tail call to continue processing
            | [] -> () // list has been exhausted
        
        processPhrases (Seq.toList doc.Phrases) //bind and output the document.
    let rec checkForExit (unit) = 
        printfn "type exit to quit..."
        match stdin.ReadLine() with
        | "exit" -> 0
        | _ -> checkForExit()
    //    printfn "type exit to exit..."
    checkForExit()
// the last value computed by the function is the exit code
