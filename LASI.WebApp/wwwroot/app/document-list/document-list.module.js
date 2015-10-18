'use strict';
define(["require", "exports", './documents.service', './results.service', './document-list-service.provider', './tasks-list-service.provider', './list.controller', './document-list-tabset-item.directive', './document-list-menu-item.directive'], function (require, exports, documents_service_1, results_service_1, document_list_service_provider_1, tasks_list_service_provider_1, list_controller_1, document_list_tabset_item_directive_1, document_list_menu_item_directive_1) {
    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(500)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(5)
            .setDocumentListUrl('api/UserDocuments/List');
    }
    Object.defineProperty(exports, "__esModule", { value: true });
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
        controllers: { ListController: list_controller_1.ListController }
    };
});
//# sourceMappingURL=document-list.module.js.map