var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        var search;
        (function (search) {
            'use strict';
            var SearchBoxController = (function () {
                function SearchBoxController($q) {
                    this.$q = $q;
                }
                SearchBoxController.prototype.search = function (searchOptions, searchContext) {
                    var deferred = this.$q.defer();
                    var value = searchOptions.value;
                    var term = typeof value === 'string' ? value : typeof value !== 'undefined' ? value.detailText : undefined;
                    if (!term) {
                        deferred.reject('search term was undefined');
                    }
                    else if (!searchContext) {
                        deferred.reject('nothing to search');
                        this.phrases.forEach(function (phrase) { return phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', ''); });
                    }
                    else {
                        if (!this.phrases) {
                            this.phrases = searchContext
                                .flatMap(function (m) { return m.paragraphs; })
                                .flatMap(function (x) { return x.sentences; })
                                .flatMap(function (e) { return e.phrases; });
                        }
                        var results = [];
                        this.phrases.forEach(function (phrase) {
                            var matched = phrase.words.some(function (word) { return word.text === value; });
                            if (!matched) {
                                phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', '');
                            }
                            else {
                                phrase.style.cssClass += ' matched-by-search';
                                results.push(phrase);
                            }
                        });
                        deferred.resolve(typeof term === 'string' ? results.map(function (r) { return r.text; }) : results);
                    }
                    return deferred.promise;
                };
                SearchBoxController.$inject = ['$q'];
                return SearchBoxController;
            })();
            function searchBox() {
                return {
                    restrict: 'E',
                    controller: SearchBoxController,
                    controllerAs: 'search',
                    bindToController: true,
                    scope: {
                        searchContext: '='
                    },
                    templateUrl: '/app/document-viewer/search/search-box.directive.html'
                };
            }
            angular
                .module('documentViewer.search')
                .directive('searchBox', searchBox);
        })(search = documentViewer.search || (documentViewer.search = {}));
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
