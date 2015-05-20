module App {
    export interface ITextualModel {
        text: string
        detailText: string
        id: number
    }
    export interface IReifiedTextualModel extends ITextualModel {
        paragraphs: IParagraphModel[]
        sentences: ISentenceModel[]
        phrases: IPhraseModel[]
        words: IWordModel[]
    }
    export interface IDocumentModel extends IReifiedTextualModel { }
    export interface IParagraphModel extends IReifiedTextualModel { }
    export interface ISentenceModel extends ITextualModel { }
    export interface IPhraseModel extends ITextualModel { }
    export interface IWordModel extends ITextualModel { }
}