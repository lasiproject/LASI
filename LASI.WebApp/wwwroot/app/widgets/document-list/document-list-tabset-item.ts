module LASI.documentList {
    'use strict';

    interface IDocumentListTabsetItemScope extends ng.IScope {
        documentId: string;
        name: string;
        analysisProgress: any;
        showProgress: boolean;
    }

    documentListTabsetItem.$inject = ['resultsService'];

    function documentListTabsetItem(resultsService): ng.IDirective {

        return {
            restrict: 'E',
            link: (scope: IDocumentListTabsetItemScope, element, attrs) => {
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
            templateUrl: '/app/widgets/document-list/document-list-tabset-item.html'
        };
    }
    angular
        .module(moduleName)
        .directive('documentListTabsetItem', documentListTabsetItem);
}