'use strict';
(function () {
    angular
        .module(LASI.documentList.ngName, ['ngResource', 'ui.bootstrap', 'ngFileUpload'])
        .config(configure);
    configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
    function configure(tasksListServiceProvider, documentListServiceProvider) {
        tasksListServiceProvider
            .setUpdateInterval(5000)
            .setTasksListUrl('api/Tasks');
        documentListServiceProvider
            .setRecentDocumentCount(3)
            .setDocumentListUrl('api/UserDocuments/List');
    }
})();
