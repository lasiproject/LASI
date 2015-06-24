var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        directive.$inject = [];
        function directive() {
            function link(scope, element, attrs) {
                LASI.log(scope);
                LASI.log(element);
                LASI.log(attrs);
            }
            return {
                restrict: 'E',
                link: link,
                templateUrl: '/app/document-viewer/document-page.html',
                replace: true,
                scope: {
                    page: '=',
                    document: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive('Directive', directive);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
