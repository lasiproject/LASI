(function (log) {
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
            templateUrl: '/app/widgets/document-list-app/documentListMenuItem.html',
            scope: {
                name: '=',
                documentId: '='
            },
            link: function (scope, element, attrs, ctrl) {
                log(scope);
                log(element);
                log(attrs);
                log(ctrl);
                element.click(function (e) {
                    e.preventDefault();
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
})(LASI.log);