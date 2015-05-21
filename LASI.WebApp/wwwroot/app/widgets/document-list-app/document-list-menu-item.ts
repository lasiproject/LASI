(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .directive('documentListMenuItem', documentListMenuItem);

    documentListMenuItem.$inject = ['$window', 'resultsService'];

    function documentListMenuItem($window, resultsService) {

        return {
            transclude: true,
            replace: true,
            restrict: 'E',
            templateUrl: '/app/widgets/document-list-app/document-list-menu-item.html',
            scope: {
                name: '=',
                documentId: '='
            },
            link: function (scope, element, attrs, ctrl) {
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
})();