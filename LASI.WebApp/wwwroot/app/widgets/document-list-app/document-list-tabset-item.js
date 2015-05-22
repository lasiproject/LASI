(function () {
    'use strict';
    angular
        .module(LASI.documentList.ngName)
        .directive('documentListTabsetItem', documentListTabsetItem);
    documentListTabsetItem.$inject = [];
    function documentListTabsetItem() {
        var directive = {
            transclude: true,
            replace: true,
            restrict: 'E',
            link: link,
            scope: {
                documentId: '=',
                name: '=',
                percentComplete: '=',
                documentModel: '='
            },
            templateUrl: '/app/widgets/document-list-app/document-list-tabset-item.html',
        };
        return directive;
        function link(scope, element, attrs) {
            console.log(attrs);
        }
    }
})();
