'use strict';
import template from './document-viewer.html';
documentViewerDirective.$inject = [];
export default function documentViewerDirective(): angular.IDirective {
    return {
        restrict: 'E',
        template,
        scope: { document: '=' }
    };
}