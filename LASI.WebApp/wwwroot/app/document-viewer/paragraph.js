var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        paragraph.$inject = ['$window'];
        function paragraph($window) {
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/paragraph.html',
                scope: {
                    paragraph: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive('paragraph', paragraph);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
