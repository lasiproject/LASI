'use strict';
import template from 'app/document-viewer/directives/paragraph.html';
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