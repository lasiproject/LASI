/// <amd-dependency path="./document-list-menu-item.directive.html" />
'use strict';
define(["require", "exports", "./document-list-menu-item.directive.html"], function (require, exports) {
    documentListMenuItem.$inject = ['$window', 'resultsService'];
    var template = require('./document-list-menu-item.directive.html');
    function documentListMenuItem($window, resultsService) {
        return {
            transclude: true,
            replace: true,
            restrict: 'E',
            template: template,
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
    exports.documentListMenuItem = documentListMenuItem;
});
//# sourceMappingURL=document-list-menu-item.directive.js.map