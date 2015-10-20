/// <amd-dependency path="./document-list-menu-item.directive.html" />
'use strict';
System.register(['app/document-list/document-list-menu-item.html'], function(exports_1) {
    var document_list_menu_item_html_1;
    function documentListMenuItem($window, resultsService) {
        return {
            transclude: true,
            replace: true,
            restrict: 'E',
            template: document_list_menu_item_html_1.default,
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
    exports_1("documentListMenuItem", documentListMenuItem);
    return {
        setters:[
            function (document_list_menu_item_html_1_1) {
                document_list_menu_item_html_1 = document_list_menu_item_html_1_1;
            }],
        execute: function() {
            documentListMenuItem.$inject = ['$window', 'resultsService'];
        }
    }
});
