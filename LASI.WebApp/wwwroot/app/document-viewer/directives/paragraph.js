/// <amd-dependency path="./paragraph.html" />
'use strict';
System.register(['app/document-viewer/directives/paragraph.html'], function(exports_1) {
    var paragraph_html_1;
    function paragraph($window) {
        return {
            restrict: 'E',
            template: paragraph_html_1.default,
            scope: {
                paragraph: '=',
                parentId: '='
            }
        };
    }
    exports_1("default", paragraph);
    return {
        setters:[
            function (paragraph_html_1_1) {
                paragraph_html_1 = paragraph_html_1_1;
            }],
        execute: function() {
            paragraph.$inject = ['$window'];
        }
    }
});
