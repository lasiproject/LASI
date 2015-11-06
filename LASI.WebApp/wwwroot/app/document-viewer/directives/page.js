/// <amd-dependency path="./page.html" />
'use strict';
System.register(['app/document-viewer/directives/page.html'], function(exports_1) {
    var page_html_1;
    function documentPage() {
        return {
            restrict: 'E',
            template: page_html_1.default,
            scope: {
                page: '=',
                document: '='
            }
        };
    }
    exports_1("default", documentPage);
    return {
        setters:[
            function (page_html_1_1) {
                page_html_1 = page_html_1_1;
            }],
        execute: function() {
        }
    }
});
