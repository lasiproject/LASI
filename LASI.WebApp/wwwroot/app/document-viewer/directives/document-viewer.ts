'use strict';
import template from 'app/document-viewer/directives/document-viewer.html';
documentViewerDirective.$inject = [];
export default function documentViewerDirective(): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: { document: '=' }
    };
}