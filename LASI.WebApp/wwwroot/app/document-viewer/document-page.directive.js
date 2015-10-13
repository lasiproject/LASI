'use strict';
function documentPage() {
    function link(scope, element, attrs) {
        console.log(scope);
        console.log(element);
        console.log(attrs);
    }
    return {
        restrict: 'E',
        link: link,
        template: require('/app/document-viewer/document-page.directive.html!'),
        replace: true,
        scope: {
            page: '=',
            document: '='
        }
    };
}
exports.documentPage = documentPage;
