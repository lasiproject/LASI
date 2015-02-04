module InputFileAdapter
open LASI.Content
let wrapFile (path : string) = 
    match path.Split('.') |> Array.last with
    | "docx" -> Some(DocXFile path :> IRawTextSource)
    | "doc" -> Some(DocFile path :> IRawTextSource)
    | "txt" -> Some(TxtFile path :> IRawTextSource)
    | "pdf" -> Some(PdfFile path :> IRawTextSource)
    | _ -> None
