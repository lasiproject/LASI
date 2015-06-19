module LASI.documentList {
    'use strict';


    angular.module(LASI.documentList.moduleName, [
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu',
        'ngFileUpload',
        LASI.documentViewer.moduleName
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