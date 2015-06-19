module LASI.documentViewer  {
    'use strict';

    interface IDirective extends ng.IDirective {
    }

    interface IDirectiveScope extends ng.IScope {
    }

    interface IDirectiveAttributes extends ng.IAttributes {
    }

    directive.$inject = ['$window'];
    function directive($window: ng.IWindowService): IDirective {
        return {
            restrict: 'E',
            link: link
        };

        function link(scope: IDirectiveScope, element: ng.IAugmentedJQuery, attrs: IDirectiveAttributes) {

        }
    }

    angular
        .module(LASI.documentViewer.moduleName)
        .directive('Directive', directive);
}