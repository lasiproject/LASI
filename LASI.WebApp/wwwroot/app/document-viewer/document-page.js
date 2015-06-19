var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        'use strict';
        directive.$inject = ['$window'];
        function directive($window) {
            return {
                restrict: 'E',
                link: link
            };
            function link(scope, element, attrs) {
            }
        }
        angular
            .module(LASI.documentViewer.ngName)
            .directive('Directive', directive);
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
