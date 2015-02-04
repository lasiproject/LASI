module AdapterUnions
open LASI.Core
type Lexical = 
    | Entity of IEntity : ILexical 
    | Verbal of IVerbal : ILexical 
    | Referencer of IReferencer : ILexical 
    | Adverbial of IAdverbial : ILexical 
    | Descriptor of IDescriptor : ILexical 
    | Phrase of Phrase : ILexical 
    | Word of Word : ILexical 
    | Other of ILexical 