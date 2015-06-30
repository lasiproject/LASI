module LASI.models {

    export interface ITextFragmentModel {
        paragraphs: IParagraphModel[];
    }

    export interface IDocumentModel extends ITextFragmentModel {
        title: string;
        id: string;
        progress: number | string;
        percentComplete: number | string;
    }

    export interface IPageModel extends ITextFragmentModel {
        pageNumber: number;
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
        contextmenu: documentViewer.ILexicalContextMenuDataSource| documentViewer.IVerbalContextmenuDataSource | documentViewer.IReferencerContextmenuDataSource;
    }

    export interface IPhraseModel extends ILexicalModel {
        words: IWordModel[];
    }

    export interface IWordModel extends ILexicalModel { }
}