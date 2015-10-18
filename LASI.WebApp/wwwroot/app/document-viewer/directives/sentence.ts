/// <amd-dependency path="./sentence.html" />
'use strict';
import template from './sentence.html';
export default function sentence(): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: {
            sentence: '=',
            parentId: '='
        }
    };
}