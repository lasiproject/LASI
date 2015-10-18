/// <amd-dependency path="./page.html" />
'use strict';

export default function documentPage(): angular.IDirective {
    function link(scope: angular.IScope, element: angular.IAugmentedJQuery, attrs: angular.IAttributes) {
        console.log(scope);
        console.log(element);
        console.log(attrs);
    }
    return {
        restrict: 'E',
        link,
        template: require('./page.html'),
        scope: {
            page: '=',
            document: '='
        }
    };
}