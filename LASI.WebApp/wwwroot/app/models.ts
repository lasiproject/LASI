import {
LexicalContextMenuDataSource,
VerbalContextmenuDataSource,
ReferencerContextmenuDataSource,
LexicalMenuBuilderFactory
} from './document-viewer/lexical-menu-builder.service';
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
    contextmenu: LexicalContextMenuDataSource |
    VerbalContextmenuDataSource |
    ReferencerContextmenuDataSource;
}

export interface PhraseModel extends LexicalModel {
    words: WordModel[];
}

export interface WordModel extends LexicalModel { }

export interface User {
    loggedIn: boolean;
    email: string;
    password: string;
}