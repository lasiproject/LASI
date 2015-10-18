/// <amd-dependency path="./sentence.html" />
'use strict';
define(["require", "exports", "./sentence.html"], function (require, exports) {
    var template = require('./sentence.html');
    function sentence() {
        return {
            restrict: 'E',
            template: template,
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = sentence;
});
//# sourceMappingURL=sentence.js.map