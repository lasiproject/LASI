/// <amd-dependency path="./document-viewer.html" />
'use strict';

var template = require('./document-viewer.html');
documentViewerDirective.$inject = [];
export default function documentViewerDirective(): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: { document: '=' }
    };
}