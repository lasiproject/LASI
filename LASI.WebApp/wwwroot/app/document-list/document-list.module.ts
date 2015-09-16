namespace LASI.documentList {
    'use strict';


    angular.module('documentList', [
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu',
        'ngFileUpload',
        'documentViewer',
        'debug'
    ], config);

    config.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];

    function config(tasksListServiceProvider: TasksListServiceConfig, documentListServiceProvider: DocumentListServiceConfig) {
        tasksListServiceProvider
            .setUpdateInterval(500)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(5)
            .setDocumentListUrl('api/UserDocuments/List');
    }
}