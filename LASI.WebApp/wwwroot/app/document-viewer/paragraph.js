var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        paragraph.$inject = ['$window'];
        function paragraph($window) {
            var link = function (scope, element, attrs) {
                //console.log(scope.parentId);
            };
            return {
                restrict: 'E',
                templateUrl: '/app/document-viewer/paragraph.html',
                link: link,
                scope: {
                    paragraph: '=',
                    parentId: '='
                }
            };
        }
        angular
            .module('documentViewer')
            .directive('paragraph', paragraph);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
