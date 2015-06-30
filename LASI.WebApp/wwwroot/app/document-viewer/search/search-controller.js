var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        var search;
        (function (search) {
            'use strict';
            angular
                .module('documentViewer.search')
                .directive('textSearch', textSearch);
            textSearch.$inject = [];
            function textSearch() {
                return {
                    scope: {
                        find: '=',
                        searchContext: '=',
                        onFound: '@',
                        onContextChanged: '@'
                    },
                    bindToController: true,
                    controllerAs: 'search',
                    controller: controller,
                    link: link
                };
                controller.$inject = ['$q', '$interval', '$timeout', '$window'];
                var toFind = {
                    id: 1,
                    contextmenu: {
                        lexicalId: 1
                    },
                    detailText: '',
                    hasContextmenuData: true, style: { cssClass: 'lexical' },
                    text: ''
                };
                controller(null, null, null, null, null)
                    .search(toFind, [], { lifted: true })
                    .then(function (results) { return LASI.log(results[0]); });
                function controller($scope, $q, $interval, $timeout, $window) {
                    return {
                        matchedModels: [],
                        matchedTexts: [],
                        search: function (find, options) {
                            var deferred = $q.defer();
                            $timeout(function () {
                                deferred.resolve();
                            }, 0);
                            return deferred.promise;
                        }
                    };
                }
                function link(scope, element, attrs, controller) {
                    [scope, element, attrs, controller].filter(function (e) { return !!e; }).forEach(LASI.log);
                    var find = scope.find;
                    var searchContext = scope.searchContext;
                    if (!(searchContext instanceof Array)) {
                        var context = [searchContext];
                        if (typeof find === 'string') {
                            controller.search(find, context).then(function (data) { return LASI.log(data[0]); });
                        }
                        else {
                            controller.search(find, context).then(function (data) { return LASI.log(data[0].detailText); });
                        }
                    }
                }
            }
        })(search = documentViewer.search || (documentViewer.search = {}));
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
