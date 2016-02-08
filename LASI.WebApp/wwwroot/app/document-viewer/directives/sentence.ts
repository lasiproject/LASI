'use strict';
import template from './sentence.html';
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