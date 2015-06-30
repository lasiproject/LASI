module LASI.documentUpload {
    'use strict';

    angular
        .module('documentUpload')
        .directive('uploadPanel', uploadPanel);

    uploadPanel.$inject = ['$window'];

    function uploadPanel($window): ng.IDirective {
        return {
            link: link,
            restrict: 'E'
        };

        function link(scope, element, attrs) {
        }
    }

}