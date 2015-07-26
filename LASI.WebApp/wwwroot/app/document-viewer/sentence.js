var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module('documentViewer')
            .directive('sentence', sentence);
        function sentence() {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/sentence.html',
                scope: {
                    sentence: '=',
                    parentId: '='
                }
            };
        }
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
