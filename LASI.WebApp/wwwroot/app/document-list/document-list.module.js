'use strict';
System.register(['./documents-service', './results-service', './document-list-service-provider', './tasks-list-service-provider', './list-controller', './document-list-tabset-item', './document-list-menu-item'], function(exports_1) {
    var documents_service_1, results_service_1, document_list_service_provider_1, tasks_list_service_provider_1, list_controller_1, document_list_tabset_item_1, document_list_menu_item_1;
    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(500)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(5)
            .setDocumentListUrl('api/UserDocuments/List');
    }
    return {
        setters:[
            function (documents_service_1_1) {
                documents_service_1 = documents_service_1_1;
            },
            function (results_service_1_1) {
                results_service_1 = results_service_1_1;
            },
            function (document_list_service_provider_1_1) {
                document_list_service_provider_1 = document_list_service_provider_1_1;
            },
            function (tasks_list_service_provider_1_1) {
                tasks_list_service_provider_1 = tasks_list_service_provider_1_1;
            },
            function (list_controller_1_1) {
                list_controller_1 = list_controller_1_1;
            },
            function (document_list_tabset_item_1_1) {
                document_list_tabset_item_1 = document_list_tabset_item_1_1;
            },
            function (document_list_menu_item_1_1) {
                document_list_menu_item_1 = document_list_menu_item_1_1;
            }],
        execute: function() {
            configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
            exports_1("default",{
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
                directives: { documentListTabsetItem: document_list_tabset_item_1.documentListTabsetItem, documentListMenuItem: document_list_menu_item_1.documentListMenuItem },
                controllers: { ListController: list_controller_1.ListController }
            });
        }
    }
});
