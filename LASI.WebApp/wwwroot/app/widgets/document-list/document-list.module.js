var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular.module('documentList', [
            'ngResource',
            'ui.bootstrap',
            'ui.bootstrap.contextMenu',
            'ngFileUpload',
            'documentViewer'
        ]).config(configure);
        configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];
        function configure(tasksListServiceProvider, documentListServiceProvider) {
            tasksListServiceProvider
                .setUpdateInterval(100)
                .setTasksListUrl('api/Tasks');
            documentListServiceProvider
                .setRecentDocumentCount(3)
                .setDocumentListUrl('api/UserDocuments/List');
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
