'use strict';
var document_controller_1 = require('./document.controller');
var document_model_service_1 = require('./document-model.service');
var lexical_menu_builder_service_1 = require('./lexical-menu-builder.service');
var module = {
    name: 'documentViewer',
    requires: [
        'documentViewer.search',
        'widgets',
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu'
    ],
    controllers: { DocumentController: document_controller_1.DocumentController },
    factories: { documentModelService: document_model_service_1.documentModelService, lexicalMenuBuilder: lexical_menu_builder_service_1.lexicalMenuBuilder }
};
exports.default = module;
