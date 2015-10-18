'use strict';
define(["require", "exports", './document.controller', './document-model.service', './lexical-menu-builder.service', './directives/document-viewer', './directives/page', './directives/paragraph', './directives/sentence', './directives/phrase'], function (require, exports, document_controller_1, document_model_service_1, lexical_menu_builder_service_1, document_viewer_1, page_1, paragraph_1, sentence_1, phrase_1) {
    var module = {
        name: 'documentViewer',
        requires: [
            'documentViewer.search',
            'widgets',
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu'
        ],
        controllers: {
            DocumentController: document_controller_1.DocumentController
        },
        factories: {
            documentModelService: document_model_service_1.documentModelService,
            lexicalMenuBuilder: lexical_menu_builder_service_1.lexicalMenuBuilder
        },
        directives: {
            documentViewer: document_viewer_1.default,
            page: page_1.default,
            paragraph: paragraph_1.default,
            sentence: sentence_1.default,
            phrase: phrase_1.default
        }
    };
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = module;
});
//# sourceMappingURL=document-viewer.module.js.map