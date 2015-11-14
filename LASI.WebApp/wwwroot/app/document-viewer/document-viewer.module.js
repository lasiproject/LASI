System.register(['./document.controller', './document-model-service', './lexical-menu-builder', './directives/document-viewer', './directives/page', './directives/paragraph', './directives/sentence', './directives/phrase'], function(exports_1) {
    'use strict';
    var document_controller_1, document_model_service_1, lexical_menu_builder_1, document_viewer_1, page_1, paragraph_1, sentence_1, phrase_1;
    return {
        setters:[
            function (document_controller_1_1) {
                document_controller_1 = document_controller_1_1;
            },
            function (document_model_service_1_1) {
                document_model_service_1 = document_model_service_1_1;
            },
            function (lexical_menu_builder_1_1) {
                lexical_menu_builder_1 = lexical_menu_builder_1_1;
            },
            function (document_viewer_1_1) {
                document_viewer_1 = document_viewer_1_1;
            },
            function (page_1_1) {
                page_1 = page_1_1;
            },
            function (paragraph_1_1) {
                paragraph_1 = paragraph_1_1;
            },
            function (sentence_1_1) {
                sentence_1 = sentence_1_1;
            },
            function (phrase_1_1) {
                phrase_1 = phrase_1_1;
            }],
        execute: function() {
            exports_1("default",{
                name: 'documentViewer',
                requires: [
                    'documentViewer.search',
                    'widgets',
                    'ui.bootstrap',
                    'ui.bootstrap.contextMenu'
                ],
                controllers: {
                    DocumentController: document_controller_1.DocumentController
                },
                factories: {
                    documentModelService: document_model_service_1.documentModelService,
                    lexicalMenuBuilder: lexical_menu_builder_1.lexicalMenuBuilder
                },
                directives: {
                    documentViewer: document_viewer_1.default,
                    page: page_1.default,
                    paragraph: paragraph_1.default,
                    sentence: sentence_1.default,
                    phrase: phrase_1.default
                }
            });
        }
    }
});
