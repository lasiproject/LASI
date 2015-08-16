namespace LASI.models {

    export interface TextFragmentModel {
        paragraphs: ParagraphModel[];
    }

    export interface DocumentModel extends TextFragmentModel {
        title: string;
        id: string;
        progress: number | string;
        percentComplete: number | string;
    }

    export interface PageModel extends TextFragmentModel {
        pageNumber: number;
    }

    export interface ParagraphModel {
        sentences: SentenceModel[];
    }

    export interface SentenceModel {
        phrases: PhraseModel[];
    }
    export interface LexicalModel {
        text: string;
        detailText: string;
        id: number;
        style: { cssClass: string };
        hasContextmenuData: boolean;
        contextmenu: documentViewer.LexicalContextMenuDataSource |
        documentViewer.VerbalContextmenuDataSource |
        documentViewer.ReferencerContextmenuDataSource;
    }

    export interface PhraseModel extends LexicalModel {
        words: WordModel[];
    }

    export interface WordModel extends LexicalModel { }
}