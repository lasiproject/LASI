module LASI.documentUpload {
    'use strict';

    angular
        .module('documentUpload')
        .directive('uploadPanel', uploadPanel);

    uploadPanel.$inject = ['$window'];

    function uploadPanel($window): ng.IDirective {
        return {
            restrict: 'E',
            link(scope, element, attrs) { }
        };
    }
}