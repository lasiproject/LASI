module LASI.documentViewer {
    'use strict';

    interface IDirective extends ng.IDirective {
    }

    interface IDirectiveScope extends ng.IScope {
    }

    interface IDirectiveAttributes extends ng.IAttributes {
    }

    directive.$inject = [];
    function directive(): IDirective {
        function link(scope: IDirectiveScope, element: ng.IAugmentedJQuery, attrs: IDirectiveAttributes) {
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