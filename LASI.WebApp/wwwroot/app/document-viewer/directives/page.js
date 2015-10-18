/// <amd-dependency path="./page.html" />
'use strict';
System.register(['./page.html'], function(exports_1) {
    var page_html_1;
    function documentPage() {
        function link(scope, element, attrs) {
            console.log(scope);
            console.log(element);
            console.log(attrs);
        }
        return {
            restrict: 'E',
            link: link,
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
//# sourceMappingURL=page.js.map