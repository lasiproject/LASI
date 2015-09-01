var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        documentViewerDirective.$inject = [];
        function documentViewerDirective() {
            var link = function (scope, element, attrs) {
                LASI.log(scope);
                LASI.log(element);
                LASI.log(attrs);
            };
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/document-viewer.directive.html',
                replace: true,
                scope: {
                    document: '='
                },
                link: link
            };
        }
        angular
            .module('documentViewer')
            .directive('documentViewerDirective', documentViewerDirective);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
