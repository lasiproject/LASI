'use strict';
var documents_service_1 = require('./documents.service');
var results_service_1 = require('./results.service');
var document_list_service_provider_1 = require('./document-list-service.provider');
var tasks_list_service_provider_1 = require('./tasks-list-service.provider');
var upload_controller_1 = require('./upload.controller');
var list_controller_1 = require('./list.controller');
var document_list_tabset_item_directive_1 = require('./document-list-tabset-item.directive');
var document_list_menu_item_directive_1 = require('./document-list-menu-item.directive');
configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
function configure(tasksListServiceProvider, documentListServiceProvider) {
    tasksListServiceProvider
        .setUpdateInterval(500)
        .setTasksListUrl('api/Tasks');
    documentListServiceProvider
        .setRecentDocumentCount(5)
        .setDocumentListUrl('api/UserDocuments/List');
}
exports.default = {
    name: 'documentList',
    requires: [
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu',
        'ngFileUpload',
        'documentViewer',
        'debug'
    ],
    configFn: configure,
    factories: { resultsService: results_service_1.resultsService, documentsService: documents_service_1.documentsService },
    providers: { documentListService: document_list_service_provider_1.DocumentListServiceProvider, tasksListService: tasks_list_service_provider_1.tasksListServiceProvider },
    directives: { documentListTabsetItem: document_list_tabset_item_directive_1.documentListTabsetItem, documentListMenuItem: document_list_menu_item_directive_1.documentListMenuItem },
    controllers: { ListController: list_controller_1.ListController, UploadController: upload_controller_1.UploadController },
};
