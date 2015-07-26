var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module('documentList')
            .directive('documentListMenuItem', documentListMenuItem);
        documentListMenuItem.$inject = ['$window', 'resultsService'];
        function documentListMenuItem($window, resultsService) {
            return {
                transclude: true,
                replace: true,
                restrict: 'E',
                templateUrl: '/app/document-list/document-list-menu-item.html',
                scope: {
                    name: '=',
                    documentId: '='
                },
                link: function (scope, element, attrs) {
                    element.click(function (event) {
                        event.preventDefault();
                        event.stopPropagation();
                        var promise = resultsService.processDocument(scope.documentId, scope.name);
                        scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                        scope.showProgress = true;
                        promise.then(function () {
                            scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                        });
                    });
                }
            };
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
