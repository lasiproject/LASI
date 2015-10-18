/// <amd-dependency path="./document-viewer.html" />
'use strict';
System.register(['./document-viewer.html'], function(exports_1) {
    var document_viewer_html_1;
    function documentViewerDirective() {
        return {
            restrict: 'E',
            template: document_viewer_html_1.default,
            scope: { document: '=' }
        };
    }
    exports_1("default", documentViewerDirective);
    return {
        setters:[
            function (document_viewer_html_1_1) {
                document_viewer_html_1 = document_viewer_html_1_1;
            }],
        execute: function() {
            documentViewerDirective.$inject = [];
        }
    }
});
//# sourceMappingURL=document-viewer.js.map