System.register(['app/document-viewer/directives/phrase.html'], function(exports_1) {
    'use strict';
    var phrase_html_1;
    function phrase(lexicalMenuBuilder) {
        return {
            restrict: 'E',
            template: phrase_html_1.default,
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
    exports_1("default", phrase);
    return {
        setters:[
            function (phrase_html_1_1) {
                phrase_html_1 = phrase_html_1_1;
            }],
        execute: function() {
            phrase.$inject = ['lexicalMenuBuilder'];
        }
    }
});
