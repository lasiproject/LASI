'use strict';
import template from 'app/document-viewer/directives/sentence.html';
export function sentence(): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: {
            sentence: '=',
            parentId: '='
        }
    };
}