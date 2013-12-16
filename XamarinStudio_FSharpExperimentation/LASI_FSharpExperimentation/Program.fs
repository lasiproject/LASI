    

// NOTE: If warnings appear, you may need to retarget this project to .NET 4.0. Show the Solution
// Pad, right-click on the project node, choose 'Options --> Build --> General' and change the target
// framework to .NET 4.0 or .NET 4.5.

module LASI.FSharpExperimentation.program
open LASI.Core
open LASI.Core.Heuristics
open LASI.ContentSystem
open System.Linq
open System.Threading.Tasks

let (~~)  (sim:SimilarityResult) :bool = SimilarityResult.op_Implicit(sim) 
type LexicalUnion =
            | V of IVerbal  
            | A of IAdverbial
            | E of IEntity
            | D of IDescriptor  
         
let V (v:IVerbal):LexicalUnion = V v
let A (a:IAdverbial): LexicalUnion = A a
let E (e:IEntity): LexicalUnion = E e
let D (d:IDescriptor): LexicalUnion = D d
                      
                    
let comp (x, y)=    
        match x, y with 
            | A x , A y-> Lookup.IsSimilarTo(x, y)
            | V x , V y-> Lookup.IsSimilarTo(x, y)
            | D x , D y-> Lookup.IsSimilarTo(x, y) 
            | E x , E y-> Lookup.IsSimilarTo(x, y)
            | _, _ -> SimilarityResult()
 
let (===) x y= ~~ comp(x , y)

[<EntryPoint>]

let main argv = 
    // Register callbacks to print operation progress to the terminal
    Lookup.ResourceLoading.Add(fun e-> printfn "Started loading %A" e)
    Lookup.ResourceLoaded.Add (fun e-> printfn "Finished loading %A ms elapsed: %d" e.Message  e.ElapsedTime)
     
    // tag, parse, and construct a Document 
    let doc = Tagger.DocumentFromDocX(DocXFile @"C:\Users\Aluan\Desktop\documents\sec22.docx")
    // perform default binding on the Document

    // perform default weighting on the Document
    let w = seq{  for i in [ async {Binder.Bind(doc)}, async { Weighter.Weight (doc) }] ->  (fun e-> ignore e; printfn "Task Complete") }

                   
      
    
    
    let bellicoseVerbals =  doc.OfAction().Where (fun v -> V v === V (Verb ("attack", VerbForm.Base)))
    let bellicoseIndividuals = doc.OfEntity().Where (fun (e:IEntity)-> bellicoseVerbals.Contains( e.SubjectOf))
                 
                        
    
    // print the document while pattern matching on various Phrase Types and naively binding Pronouns at the phrasal level
    let rec processPhrases (phrs:list<Phrase>)= 
        match phrs with
        |head :: tail -> 
            match head with // process the first phrase in the list
            | :? NounPhrase as np->  
                match np.Paragraph.Phrases.OfPronounPhrase().FirstOrDefault() with 
                    | null -> () // no pronoun Phrase within the paragraph of NounPhrase np
                    | pro -> np.BindPronoun pro // bind naively (this is just an example)
                printfn "NP Matched %A" np
            | :? VerbPhrase as vp-> printfn "VP Matched %A" vp        
            | p -> printfn "Not Matched %A" p
            processPhrases tail // recursive tail call to continue processing
        | [] -> printfn "" // list has been exhausted
    
    do processPhrases (Seq.toList doc.Phrases) //bind and output the document doings.

    // keep reading from the console until the string "exit" is entered.
    let rec input line = 
        match line with
        |"quit" -> ()
        |_ -> input (stdin.ReadLine())  
    input (stdin.ReadLine())
    // the last value computed by the function is the exit code
    0 
 

 
