var LASI;
(function (LASI) {
    var documentUpload;
    (function (documentUpload) {
        'use strict';
        angular
            .module('documentUpload')
            .directive('uploadPanel', uploadPanel);
        uploadPanel.$inject = ['$window'];
        function uploadPanel($window) {
            return {
                link: link,
                restrict: 'E'
            };
            function link(scope, element, attrs) {
            }
        }
    })(documentUpload = LASI.documentUpload || (LASI.documentUpload = {}));
})(LASI || (LASI = {}));
