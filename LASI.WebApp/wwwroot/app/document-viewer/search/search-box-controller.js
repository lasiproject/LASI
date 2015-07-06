var LASI;
(function (LASI) {
    var documentViewer;
    (function (documentViewer) {
        var search;
        (function (search_1) {
            'use strict';
            //function searchBox(): ng.IDirective {
            SearchBoxController.$inject = ['$q'];

            function SearchBoxController($q) {
                var vm = this;
                vm.searchContext = {
                    value: undefined,
                    set: function (value) {
                        var context = (value instanceof Array ? value : [value]);
                        this.value = context;
                        return vm.search;
                    }, get: function () { return this.value; }
                };
                var search = function (searchFor, options) {
                    var deferred = $q.defer();
                    var term = typeof searchFor === 'string' ? searchFor : typeof searchFor !== 'undefined' ? searchFor.detailText : undefined;
                    if (term === undefined) {
                        deferred.reject('search term was undefined');
                    }
                    var results = vm.searchContext
                        .flatMap(function (m) { return m.paragraphs; })
                        .flatMap(function (x) { return x.sentences; })
                        .flatMap(function (e) { return e.phrases; })
                        .filter(function (phrase) { return phrase.text === searchFor; });
                    results.forEach(function (model) {
                        var matched = true;
                        var unmatchedStyle = model.style.cssClass;
                        var matchedStyle = unmatchedStyle + ' matched-by-search';
                        model.style = {
                            get cssClass() {
                                var result = matched ? matchedStyle : unmatchedStyle;
                                matched = !matched;
                                return result;
                            }
                        };
                    });
                    deferred.resolve(typeof term === 'string' ? results.map(function (r) { return r.text; }) : results);
                    return deferred.promise;
                };
            }
            angular
                .module('documentViewer.search')
                .controller('SearchBoxController', SearchBoxController);
        })(search = documentViewer.search || (documentViewer.search = {}));
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
