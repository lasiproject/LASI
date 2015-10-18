'use strict';

paragraph.$inject = ['$window'];
export default function paragraph($window: angular.IWindowService): angular.IDirective {
    return {
        restrict: 'E',
        templateUrl: '/app/document-viewer/paragraph.directive.html',
        scope: {
            paragraph: '=',
            parentId: '='
        }
    };

}