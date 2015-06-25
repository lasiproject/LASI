module LASI.models {

    export interface IDocumentModel {
        title: string;
        id: number;
        style: { cssClass: string };
        paragraphs: IParagraphModel[];
        prgress: number | string;
    }

    export interface IParagraphModel {
        sentences: ISentenceModel[];
    }

    export interface ISentenceModel {
        phrases: IPhraseModel[];
    }
    export interface ILexicalModel {
        text: string;
        detailText: string;
        id: number;
        style: { cssClass: string };
        hasContextmenuData: boolean;
        contextmenu: documentViewer.IVerbalContextmenuDataSource | documentViewer.IReferencerContextmenuDataSource;
    }

    export interface IPhraseModel extends ILexicalModel {
        words: IWordModel[];
    }

    export interface IWordModel extends ILexicalModel { }
}