module LASI.documentList {
    'use strict';


    angular.module('documentList', [
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu',
        'ngFileUpload',

        'documentViewer',
        'debug'
    ]).config(configure);

    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];

    function configure(tasksListServiceProvider: ITasksListServiceConfig, documentListServiceProvider: IDocumentListServiceConfig) {
        tasksListServiceProvider
            .setUpdateInterval(100)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(3)
            .setDocumentListUrl('api/UserDocuments/List');
    }
}