'use strict';
define(["require", "exports"], function (require, exports) {
    documentListTabsetItem.$inject = ['resultsService'];
    function documentListTabsetItem(resultsService) {
        return {
            restrict: 'E',
            link: function (scope, element, attrs) {
                element.click(function (event) {
                    event.stopPropagation();
                    resultsService.processDocument(scope.documentId, scope.name);
                    event.preventDefault();
                    var promise = resultsService.processDocument(scope.documentId, scope.name);
                    scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    scope.showProgress = true;
                    promise.then(function () { return scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete; });
                });
                console.log(attrs);
            },
            scope: {
                documentId: '=',
                name: '=',
                percentComplete: '='
            },
            template: require('/app/document-list/document-list-tabset-item.directive.html')
        };
    }
    exports.documentListTabsetItem = documentListTabsetItem;
});
//# sourceMappingURL=document-list-tabset-item.directive.js.map