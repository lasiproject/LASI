(function () {
    'use strict';

    angular
        .module(LASI.documentList.ngName, ['ngResource', 'ui.bootstrap', 'ngFileUpload'])
        .config(configure);

    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];

    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(500)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(3)
            .setDocumentListUrl('api/UserDocuments/List');
    }
})();