module LASI.documentViewer.search {
    'use strict';
    function searchBox(): angular.IDirective {
        return {
            restrict: 'E',
            controller: 'SearchBoxController',
            controllerAs: 'search',
            scope: {
                searchContext: '='
            },
            templateUrl: '/app/document-viewer/search/search-box.html',
            link(scope, element, attrs, ctrl) {
                ctrl.keyDown = function ($event) {
                    if ($event.keyCode === 13) {
                        ctrl.search({ value: ctrl.find }, [(<any>scope).searchContext]);
                        $event.stopPropagation();
                        $event.preventDefault();
                    }
                };
            }
        };
    }

    angular
        .module('documentViewer.search')
        .directive('searchBox', searchBox);
}