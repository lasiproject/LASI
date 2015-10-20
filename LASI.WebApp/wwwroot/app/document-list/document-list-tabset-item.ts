/// <amd-dependency path="./document-list-tabset-item.directive.html" />
'use strict';

import template from 'app/document-list/document-list-tabset-item.html';

documentListTabsetItem.$inject = ['resultsService'];
export function documentListTabsetItem(resultsService): angular.IDirective {

    return {
        template,
        restrict: 'E',
        link: (scope: DocumentListTabsetItemScope, element, attrs) => {
            element.click(event => {
                event.stopPropagation();
                resultsService.processDocument(scope.documentId, scope.name); event.preventDefault();
                var promise = resultsService.processDocument(scope.documentId, scope.name);
                scope.percentComplete = resultsService.tasks[scope.documentId].percentComplete;
                scope.showProgress = true;
                promise.then(() => scope.percentComplete = resultsService.tasks[scope.documentId].percentComplete);
            });
            console.log(attrs);
        },
        scope: {
            documentId: '=',
            name: '=',
            percentComplete: '=',
            showProgress: '='
        }
    };
}

interface DocumentListTabsetItemScope extends angular.IScope {
    documentId: string;
    name: string;
    percentComplete: any;
    showProgress: boolean;
} 