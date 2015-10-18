/// <amd-dependency path="./document-viewer.html" />
'use strict';
define(["require", "exports", "./document-viewer.html"], function (require, exports) {
    var template = require('./document-viewer.html');
    documentViewerDirective.$inject = [];
    function documentViewerDirective() {
        return {
            restrict: 'E',
            template: template,
            scope: { document: '=' }
        };
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = documentViewerDirective;
});
//# sourceMappingURL=document-viewer.js.map