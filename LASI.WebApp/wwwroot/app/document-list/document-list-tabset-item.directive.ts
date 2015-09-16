namespace LASI.documentList {
    'use strict';

    documentListTabsetItem.$inject = ['resultsService'];

    function documentListTabsetItem(resultsService): angular.IDirective {

        return {
            restrict: 'E',
            link: (scope: DocumentListTabsetItemScope, element, attrs) => {
                element.click(event => {
                    event.stopPropagation();
                    resultsService.processDocument(scope.documentId, scope.name); event.preventDefault();
                    var promise = resultsService.processDocument(scope.documentId, scope.name);
                    scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete;
                    scope.showProgress = true;
                    promise.then(() => scope.analysisProgress = resultsService.tasks[scope.documentId].percentComplete);
                });
                console.log(attrs);
            },
            scope: {
                documentId: '=',
                name: '=',
                percentComplete: '='
            },
            templateUrl: '/app/document-list/document-list-tabset-item.directive.html'
        };
    }

    interface DocumentListTabsetItemScope extends angular.IScope {
        documentId: string;
        name: string;
        analysisProgress: any;
        showProgress: boolean;
    }

    angular
        .module('documentList')
        .directive({ documentListTabsetItem });
}