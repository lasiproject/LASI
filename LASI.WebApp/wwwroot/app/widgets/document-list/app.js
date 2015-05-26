(function () {
    'use strict';
    angular.module(LASI.documentList.ngName, [
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu',
        'ngFileUpload'
    ]).config(configure);
    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(2000)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(3)
            .setDocumentListUrl('api/UserDocuments/List');
    }
})();
