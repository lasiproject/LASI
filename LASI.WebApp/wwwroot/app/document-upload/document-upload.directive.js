var LASI;
(function (LASI) {
    var documentUpload;
    (function (documentUpload) {
        'use strict';
        uploadPanel.$inject = ['$window'];
        function uploadPanel($window) {
            return {
                restrict: 'E',
                link: function (scope, element, attrs) { }
            };
        }
        angular
            .module('documentUpload')
            .directive({ uploadPanel: uploadPanel });
    })(documentUpload = LASI.documentUpload || (LASI.documentUpload = {}));
})(LASI || (LASI = {}));
