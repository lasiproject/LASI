var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        document.$inject = ['$window'];
        function document($window) {
            var link = function (scope, element, attrs) {
                console.log(scope);
                console.log(element);
                console.log(attrs);
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
            //return new Document('/app/widgets/document-list-app/interactive-representations/document.html', link, scope);
        }
        angular
            .module(LASI.documentViewer.moduleName)
            .directive('document', document);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
