﻿'use strict';
documentListMenuItem.$inject = ['$window', 'resultsService'];

interface DocumentListItemScope extends angular.IScope {
    documentId: string;
    name: string;
    analysisProgress: number;
    showProgress: boolean;
}
export function documentListMenuItem($window, resultsService): angular.IDirective {

    return {
        transclude: true,
        replace: true,
        restrict: 'E',
        template: require('/app/document-list/document-list-menu-item.directive.html!'),
        scope: {
            name: '=',
            documentId: '='
        },
        link: function (scope: DocumentListItemScope, element: JQuery, attrs: angular.IAttributes) {
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