module LASI.documentUpload {
    'use strict';

    angular
        .module('documentUpload')
        .directive('uploadPanel', uploadPanel);

    uploadPanel.$inject = ['$window'];

    function uploadPanel($window): angular.IDirective {
        return {
            restrict: 'E',
            link(scope, element, attrs) { }
        };
    }
}