/// <amd-dependency path="./paragraph.html" />
'use strict';
define(["require", "exports", "./paragraph.html"], function (require, exports) {
    paragraph.$inject = ['$window'];
    function paragraph($window) {
        return {
            restrict: 'E',
            template: require('./paragraph.html'),
            scope: {
                paragraph: '=',
                parentId: '='
            }
        };
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = paragraph;
});
//# sourceMappingURL=paragraph.js.map