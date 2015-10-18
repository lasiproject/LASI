/// <amd-dependency path="./paragraph.html" />
'use strict';
import template from './paragraph.html';
paragraph.$inject = ['$window'];
export default function paragraph($window: angular.IWindowService): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: {
            paragraph: '=',
            parentId: '='
        }
    };

}