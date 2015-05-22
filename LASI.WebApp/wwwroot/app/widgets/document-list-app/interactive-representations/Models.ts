module App {
    export interface ITextualModel {
        text: string
        detailText: string
        id: number
        style: { cssClass: string }
        contextMenuJson: any 
        // this needs to be revised as this was copied verbatim from the 
        // old server side view model for use in the razor template and 
        // there should be a simple way to deserialize it as a proper object.
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
    export interface ITextualDirectiveScope {
        document?: string
        paragraph?: string
        sentence?: string
        clause?: string
        phrase?: string
        word?: string
        contextmenuId?: string
        parentId?: string|number
    }
    export class TextualDirective {
        constructor(public templateUrl: string,
            public link: ng.IDirectiveLinkFn, public parent?: ITextualModel,
            public scope?: ITextualDirectiveScope) { }
        restrict = "E"
        replace = true
    }

}