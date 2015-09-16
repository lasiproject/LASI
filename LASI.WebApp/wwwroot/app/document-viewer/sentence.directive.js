var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        function sentence() {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/sentence.directive.html',
                scope: {
                    sentence: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive({ sentence: sentence });
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
