(function () {
    'use strict';
    angular
        .module(LASI.results.ngName)
        .directive('contextMenu', contextMenu);

    contextMenu.$inject = ['$window', '$log'];

    function contextMenu($window) {
        return {
            restrict: 'A',
            scope: {
                lexicalType: '=',
                lexicalId: '='
            },
            transclude: false,
            link: function link(scope, element, attrs, ctrl) {
                $log("scope");
                $log(scope);
                $log("element");
                $log(element);
                $log("attrs");
                $log(attrs);
                $log("ctrl");
                $log(ctrl);
            },
        };
    }

})();