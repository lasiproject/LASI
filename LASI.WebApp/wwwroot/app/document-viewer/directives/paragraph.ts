'use strict';
import template from './paragraph.html';
paragraph.$inject = ['$window'];
export function paragraph($window: angular.IWindowService): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: {
            paragraph: '=',
            parentId: '='
        }
    };

}