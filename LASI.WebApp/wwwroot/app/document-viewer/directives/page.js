/// <amd-dependency path="./page.html" />
'use strict';
define(["require", "exports", "./page.html"], function (require, exports) {
    function documentPage() {
        function link(scope, element, attrs) {
            console.log(scope);
            console.log(element);
            console.log(attrs);
        }
        return {
            restrict: 'E',
            link: link,
            template: require('./page.html'),
            scope: {
                page: '=',
                document: '='
            }
        };
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = documentPage;
});
//# sourceMappingURL=page.js.map