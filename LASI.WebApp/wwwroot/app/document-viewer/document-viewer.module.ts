'use strict';
import { DocumentController } from './document.controller';
import { documentModelService } from './document-model-service';
import { resultsService } from './results-service';
import { lexicalMenuBuilder } from './lexical-menu-builder';
import { documentViewer, documentPage as page, paragraph, sentence, phrase } from './directives/directives';

export default {
    name: 'documentViewer',
    requires: [
        'documentViewer.search',
        'widgets',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu'
    ],
    controllers: {
        DocumentController
    },
    factories: {
        documentModelService,
        resultsService,
        lexicalMenuBuilder
    },
    directives: {
        documentViewer,
        page,
        paragraph,
        sentence,
        phrase
    }
} as NgModuleConfig;