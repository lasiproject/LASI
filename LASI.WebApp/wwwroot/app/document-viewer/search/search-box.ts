namespace LASI.documentViewer.search {
    'use strict';
    function searchBox(): angular.IDirective {
        return {
            restrict: 'E',
            controller: 'SearchBoxController',
            controllerAs: 'search',
            bindToController: true,
            scope: {
                searchContext: '='
            },
            templateUrl: '/app/document-viewer/search/search-box.html'
        };
    }

    angular
        .module('documentViewer.search')
        .directive('searchBox', searchBox);
}