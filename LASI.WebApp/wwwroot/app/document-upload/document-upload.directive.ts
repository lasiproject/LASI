namespace LASI.documentUpload {
    'use strict';

    uploadPanel.$inject = ['$window'];

    function uploadPanel($window): angular.IDirective {
        return {
            restrict: 'E',
            link(scope, element, attrs) { }
        };
    }
    angular
        .module('documentUpload')
        .directive({ uploadPanel });

}