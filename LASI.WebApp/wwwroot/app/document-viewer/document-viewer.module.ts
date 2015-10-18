'use strict';
import { DocumentController } from './document.controller';
import { documentModelService } from './document-model.service';
import { lexicalMenuBuilder } from './lexical-menu-builder.service';
import documentViewer from './directives/document-viewer';
import page from './directives/page';
import paragraph from './directives/paragraph';
import sentence from './directives/sentence';
import phrase from './directives/phrase';
var module: AngularModuleOptions = {
    name: 'documentViewer',
    requires: [
        'documentViewer.search',
        'widgets',
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu'
    ],
    controllers: {
        DocumentController
    },
    factories: {
        documentModelService,
        lexicalMenuBuilder
    },
    directives: {
        documentViewer,
        page,
        paragraph,
        sentence,
        phrase
    }
};
export default module;