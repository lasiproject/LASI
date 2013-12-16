// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

module LASI.FSharpExperimentation.program
open LASI.Core
open LASI.Core.Heuristics
open LASI.ContentSystem
open LASI.Interop
open System.Linq
open System.Threading.Tasks
[<EntryPoint>]

let main argv = 
    
    let op sr:bool = 
        SimilarityResult.op_Implicit(sr)

    // Register callbacks to print operation progress to the terminal
    Lookup.ResourceLoading.Add(fun e-> printfn "Started loading %A" e.Message)
    Lookup.ResourceLoaded.Add(fun e-> printfn "Finished loading %A ms elapsed: %d" e.Message  e.ElapsedTime)
     
    // tag, parse, and construct a Document 
 
    // perform default binding on the Document
 
    // perform default weighting on the Document
    let controller = AnalysisController(DocXFile @"C:\Users\Aluan\Desktop\documents\sec22.docx")
    controller.ProgressChanged.Add(fun e->printfn "Update: %A" e.Message)   
    let docTask = async {
        return controller.ProcessAsync(). Result
    }
    let doc = Async.RunSynchronously(docTask).First()
    
    
         
 
    let toAttack = Verb("attack",VerbForm.Base)
    let bellicoseVerbals = doc.GetActions().Where (fun v-> op (v.IsSimilarTo(toAttack)))
    let bellicoseIndividuals =  doc.GetEntities().Where(fun e-> bellicoseVerbals.Contains(e.SubjectOf))
    
    let attackerAttackeePairs = 
        bellicoseVerbals .WithDirectObject().WithSubject().Select(fun v->  (v.AggregateSubject, v.AggregateDirectObject ))
    

    do Seq.iter( fun e-> printfn "%A" e) attackerAttackeePairs        
        

    let (|Entity|Referee|Action|Other|) (lex:ILexical) = 
        match lex with
            | :? IReferencer as r-> Referee(r)  
            | :? IEntity as e-> Entity(e)
            | :? IVerbal as a-> Action(a)
            | _ -> Other
                                        
    // print the document while pattern matching on various Phrase Types and naively binding Pronouns at the phrasal level
    let rec processPhrases (phrs:Phrase List)=
        match phrs with
        |head :: tail -> 
            match head with // process the first phrase in the list
            | Entity np->  
                match head.Paragraph.Phrases with 
                    | :? IReferencer as pro when pro<> null ->np.BindReferencer pro // bind naively (this is just an example)
                    | _->()
                printfn "NP Matched %A" np
            | :? VerbPhrase as vp-> printfn "VP Matched %A" vp        
            | p -> printfn "Not Matched %A" p
            processPhrases tail // recursive tail call to continue processing
        | [] -> printfn "" // list has been exhausted
    
    do processPhrases (Seq.toList doc.Phrases) //bind and output the document doings.

    // keep reading from the console until the string "exit" is entered.
    
    let rec checkForExit line  = 
        printfn "type quit to exit..."
        match line with
        |"quit" -> ()
        |_ -> checkForExit (stdin.ReadLine())
    printfn "type quit to exit..."
    let waitForUser = checkForExit(stdin.ReadLine())
    // the last value computed by the function is the exit code
    0 
 

 
