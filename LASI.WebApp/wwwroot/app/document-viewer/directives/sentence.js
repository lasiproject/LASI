/// <amd-dependency path="./sentence.html" />
'use strict';
System.register(['./sentence.html'], function(exports_1) {
    var sentence_html_1;
    function sentence() {
        return {
            restrict: 'E',
            template: sentence_html_1.default,
            scope: {
                sentence: '=',
                parentId: '='
            }
        };
    }
    exports_1("default", sentence);
    return {
        setters:[
            function (sentence_html_1_1) {
                sentence_html_1 = sentence_html_1_1;
            }],
        execute: function() {
        }
    }
});
//# sourceMappingURL=sentence.js.map