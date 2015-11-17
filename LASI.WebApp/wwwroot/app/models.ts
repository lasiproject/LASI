'use strict';
import { LexicalContextmenuData, VerbalContextmenuData, ReferencerContextmenuData, LexicalMenuBuilderFactory } from './document-viewer/lexical-menu-builder';

export interface DocumentListItemModel {
    id: string;
    name: string;
    progress: number;
    percentComplete: number;
    showProgress: boolean;
    statusMessage: string;
    raeification: DocumentModel;
}
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
    contextmenu: LexicalContextmenuData |
    VerbalContextmenuData | ReferencerContextmenuData;
}

export interface PhraseModel extends LexicalModel {
    words: WordModel[];
}

export interface WordModel extends LexicalModel { }

export interface User {
    loggedIn: boolean;
    email: string;
    password: string;
    documents: any[];
}