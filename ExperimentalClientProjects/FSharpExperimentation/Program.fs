// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open LASI.Core
open LASI.Core.Heuristics
open LASI.Core.Heuristics.Morphemization
open LASI.ContentSystem
open LASI.Interop
open System.Linq
open System.Threading.Tasks
open LASI.Core.DocumentStructures



[<EntryPoint>]

let main argv = 
    
    let op sr:bool = 
        SimilarityResult.op_Implicit sr

    // Register callbacks to print operation progress to the terminal
    Lookup.ResourceLoading.Add(fun e-> printfn "Started loading %s" e.Message)
    Lookup.ResourceLoaded.Add(fun e-> printfn "Finished loading %s ms elapsed: %d" e.Message  e.ElapsedTime)
     
    // tag, parse, and construct a Document 
 
    // perform default binding on the Document
 
    // perform default weighting on the Document
    let controller = AnalysisController(DocXFile @"C:\Users\Aluan\Desktop\unsound.docx")
    controller.ProgressChanged.Add(fun e->printfn "Update: %s" e.Message)   
    let docTask = async {
        return controller.ProcessAsync(). Result
    }
    let doc = Async.RunSynchronously(docTask).First()
    
    
         
 
    let toAttack = Verb("attack",VerbForm.Base)
    let bellicoseVerbals = doc.GetActions()|>Seq.filter (fun v-> op (v.IsSimilarTo toAttack))
    let bellicoseIndividuals =  doc.GetEntities()|>Seq.filter (fun e-> bellicoseVerbals.Contains e.SubjectOf)
    
    let attackerAttackeePairs = 
        bellicoseVerbals .WithDirectObject().WithSubject()|>Seq.map(fun v->  (v.AggregateSubject, v.AggregateDirectObject ))
    

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
            | Entity e ->  
                match head.Paragraph.Phrases with 
                    | :? IReferencer as pro -> e.BindReferencer pro // bind naively (this is just an example)
                    | _->()
                printfn "Matched %A" e
            | Action a -> printfn "Matched %A" a
            | p -> printfn "Unmatched %A" p
            processPhrases tail // recursive tail call to continue processing
        | [] -> printfn "" // list has been exhausted
    
    do processPhrases (Seq.toList doc.Phrases) //bind and output the document.
//
//    let svgs = 
//        query {
//            for a in doc.GetActions() do 
//            for a2 in doc.GetActions() do 
//            where(op(a.IsSimilarTo a2)  && a <> a2)
//            groupBy a
//        }
//    let r= 
//        seq { 
//            for x in svgs -> x.Key.Text + x.Select(fun e->(snd e).Text).Distinct().Aggregate(" ",(fun s i->s + i+ "\n\t" )) 
//        } 
//    do Seq.iter(fun i-> printfn "Group:  %s" i) r 
      // keep reading from the console until the string "exit" is entered.
    let rec checkForExit line  = 
        printfn "type quit to exit..."
        match line with
        |"quit" -> ()
        |_ -> checkForExit (stdin.ReadLine())
    do printfn "type quit to exit..."
    let waitForUser = checkForExit(stdin.ReadLine())
    // the last value computed by the function is the exit code
    0 
 

 
