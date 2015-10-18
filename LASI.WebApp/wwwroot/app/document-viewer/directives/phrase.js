/// <amd-dependency path='./phrase.html' />
'use strict';
define(["require", "exports", "./phrase.html"], function (require, exports) {
    phrase.$inject = ['lexicalMenuBuilder'];
    function phrase(lexicalMenuBuilder) {
        return {
            restrict: 'E',
            template: require('./phrase.html'),
            scope: {
                phrase: '=',
                parentId: '='
            },
            link: link
        };
        function link(scope, element, attrs) {
            var contextmenu = lexicalMenuBuilder.buildAngularMenu(scope.phrase.contextmenu);
            scope.phrase.hasContextmenuData = !!contextmenu;
            if (scope.phrase.hasContextmenuData) {
                scope.phrase.contextmenu = contextmenu;
            }
        }
    }
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = phrase;
});
//# sourceMappingURL=phrase.js.map