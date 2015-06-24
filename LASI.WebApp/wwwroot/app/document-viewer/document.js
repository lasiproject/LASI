var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        angular
            .module('documentViewer')
            .directive('document', document);
        document.$inject = [];
        function document() {
            var link = function (scope, element, attrs) {
                LASI.log(scope);
                LASI.log(element);
                LASI.log(attrs);
            };
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/document.html',
                replace: true,
                scope: {
                    document: '='
                },
                link: link
            };
        }
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
