'use strict';
documentViewerDirective.$inject = [];
function documentViewerDirective() {
    var link = function (scope, element, attrs) {
        console.log(scope);
        console.log(element);
        console.log(attrs);
    };
    return {
        restrict: 'E',
        templateUrl: '/app/document-viewer/document-viewer.directive.html',
        replace: true,
        scope: {
            document: '='
        },
        link: link
    };
}
exports.documentViewerDirective = documentViewerDirective;
