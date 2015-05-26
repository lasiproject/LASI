module App {
    export interface ILexicalModel {
        text: string;
        detailText: string;
        id: number;
        style: { cssClass: string };
        contextmenu?: IVerbalContextmenu | IReferencerContextmenu;
        // this needs to be revised as this was copied verbatim from the 
        // old server side view model for use in the razor template and 
        // there should be a simple way to deserialize it as a proper object.
    }


    export interface IDocumentModel {
        title: string;
        id: number;
        style: { cssClass: string };
        paragraphs: IParagraphModel[];
    }

    export interface IParagraphModel {
        sentences: ISentenceModel[];
    }
    export interface ISentenceModel {
        phrases: IPhraseModel[];
    }
    export interface IPhraseModel extends ILexicalModel {
        words: IWordModel[];
    }
    export interface IWordModel extends ILexicalModel { }
    export interface ITextualDirectiveScope {
        document?: string;
        paragraph?: string;
        sentence?: string;
        clause?: string;
        phrase?: string;
        word?: string;
        contextmenuId?: string;
        parentId?: string|number;
    }
    export interface IVerbalContextmenu {
        /**
         * The id of the verbal for which the menu is defined.
         */
        lexicalId: number;
        /**
         * The ids of any subjects.
         */
        subjectIds: number[];
        /**
         * The ids of any direct objects.
         */
        directObjectIds: number[];
        /**
         * The ids of any direct objects.
         */
        indirectObjectIds: number[];
    }

    export interface IReferencerContextmenu {
        /**
         * The id of the referencer for which the menu is defined.
         */
        lexicalId: number;
        /**
         * The ids of any entities the referred to.
         */
        refersToIds: number[];
    }
}