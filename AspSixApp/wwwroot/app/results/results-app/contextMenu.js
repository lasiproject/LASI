(function () {
    'use strict';

    angular
        .module(LASI.results.ngName)
        .directive('contextMenu', contextMenu);

    contextMenu.$inject = ['$window'];

    function contextMenu($window) {
        // Usage:
        //     <contextMenu></contextMenu>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'A',
            scope: {
                lexicalType: '=',
                lexicalId: '='
            },
            transclude: false
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();