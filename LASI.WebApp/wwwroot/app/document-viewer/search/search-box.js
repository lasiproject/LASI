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
                    scope: {
                        searchContext: '='
                    },
                    templateUrl: '/app/document-viewer/search/search-box.html',
                    link: function (scope, element, attrs, ctrl) {
                        ctrl.keyDown = function ($event) {
                            if ($event.keyCode === 13) {
                                ctrl.search({ value: ctrl.find }, [scope.searchContext]);
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
        })(search = documentViewer.search || (documentViewer.search = {}));
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
