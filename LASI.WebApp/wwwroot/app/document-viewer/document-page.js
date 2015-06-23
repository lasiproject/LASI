var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        directive.$inject = ['$window'];
        function directive($window) {
            function link(scope, element, attrs) {
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
            .module(documentViewer.moduleName)
            .directive('Directive', directive);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
//# sourceMappingURL=document-page.js.map