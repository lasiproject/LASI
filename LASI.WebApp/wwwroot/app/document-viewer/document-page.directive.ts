'use strict';

export function documentPage(): angular.IDirective {
    function link(scope: angular.IScope, element: angular.IAugmentedJQuery, attrs: angular.IAttributes) {
        console.log(scope);
        console.log(element);
        console.log(attrs);
    }
    return {
        restrict: 'E',
        link: link,
        template: require('/app/document-viewer/document-page.directive.html!'),
        replace: true,
        scope: {
            page: '=',
            document: '='
        }
    };
}