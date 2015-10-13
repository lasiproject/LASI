'use strict';

documentViewerDirective.$inject = [];
export function documentViewerDirective(): angular.IDirective {

    var link: angular.IDirectiveLinkFn = function (scope: angular.IScope, element: angular.IAugmentedJQuery, attrs: angular.IAttributes) {
        console.log(scope);
        console.log(element);
        console.log(attrs);
    };
    return {
        restrict: 'E',
        templateUrl: '/app/document-viewer/document-viewer.directive.html',
        replace: true,
        scope: {
            document: '='
        },
        link
    };
}