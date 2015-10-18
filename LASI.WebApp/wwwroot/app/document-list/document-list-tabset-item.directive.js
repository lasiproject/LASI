/// <amd-dependency path="./document-list-tabset-item.directive.html" />
'use strict';
System.register(['./document-list-tabset-item.directive.html'], function(exports_1) {
    var document_list_tabset_item_directive_html_1;
    function documentListTabsetItem(resultsService) {
        return {
            template: document_list_tabset_item_directive_html_1.default,
            restrict: 'E',
            link: function (scope, element, attrs) {
                element.click(function (event) {
                    event.stopPropagation();
                    resultsService.processDocument(scope.documentId, scope.name);
                    event.preventDefault();
                    var promise = resultsService.processDocument(scope.documentId, scope.name);
                    scope.percentComplete = resultsService.tasks[scope.documentId].percentComplete;
                    scope.showProgress = true;
                    promise.then(function () { return scope.percentComplete = resultsService.tasks[scope.documentId].percentComplete; });
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
    exports_1("documentListTabsetItem", documentListTabsetItem);
    return {
        setters:[
            function (document_list_tabset_item_directive_html_1_1) {
                document_list_tabset_item_directive_html_1 = document_list_tabset_item_directive_html_1_1;
            }],
        execute: function() {
            documentListTabsetItem.$inject = ['resultsService'];
        }
    }
});
//# sourceMappingURL=document-list-tabset-item.directive.js.map