'use strict';
import { DocumentController } from './document.controller';
import { documentModelService } from './document-model.service';
import { lexicalMenuBuilder } from './lexical-menu-builder.service';

var module: AngularModuleOptions = {
    name: 'documentViewer',
    requires: [
        'documentViewer.search',
        'widgets',
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu'
    ],
    controllers: { DocumentController },
    factories: { documentModelService, lexicalMenuBuilder }
};
export default module;