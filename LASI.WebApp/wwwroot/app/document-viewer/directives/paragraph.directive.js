'use strict';
define(["require", "exports"], function (require, exports) {
    paragraph.$inject = ['$window'];
    function paragraph($window) {
        return {
            restrict: 'E',
            templateUrl: '/app/document-viewer/paragraph.directive.html',
            scope: {
                paragraph: '=',
                parentId: '='
            }
        };
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = paragraph;
});
