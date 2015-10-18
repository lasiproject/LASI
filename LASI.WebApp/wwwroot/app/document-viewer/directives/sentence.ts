/// <amd-dependency path="./sentence.html" />
'use strict';
var template = require('./sentence.html');
export default function sentence(): angular.IDirective {
    return {
        restrict: 'E',
        template: template,
        scope: {
            sentence: '=',
            parentId: '='
        }
    };
}