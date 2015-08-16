var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        var search;
        (function (search) {
            'use strict';
            function searchBox() {
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
        })(search = documentViewer.search || (documentViewer.search = {}));
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
