module LASI.documentViewer {
    'use strict';

    directive.$inject = [];
    function directive(): ng.IDirective {
        function link(scope: ng.IScope, element: ng.IAugmentedJQuery, attrs: ng.IAttributes) {
            log(scope);
            log(element);
            log(attrs);
        }
        return {
            restrict: 'E',
            link: link,
            templateUrl: '/app/document-viewer/document-page.html',
            replace: true,
            scope: {
                page: '=',
                document: '='
            }
        };


    }

    angular
        .module('documentViewer')
        .directive('documentPage', directive);
}