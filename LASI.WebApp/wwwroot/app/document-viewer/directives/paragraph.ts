/// <amd-dependency path="./paragraph.html" />
'use strict';

paragraph.$inject = ['$window'];
export default function paragraph($window: angular.IWindowService): angular.IDirective {
    return {
        restrict: 'E',
        template: require( './paragraph.html'),
        scope: {
            paragraph: '=',
            parentId: '='
        }
    };

}