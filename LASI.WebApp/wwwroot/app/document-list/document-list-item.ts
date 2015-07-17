module LASI.documentList {
    export interface DocumentListItem {
        /**
         * The name of the document
         */
        name: string;
        /**
         * The id of the document
         */
        id: string;
        /**
         * The content is optional as the list item may just be a placeholder for the document.
         */
        content?: string;
    }
}