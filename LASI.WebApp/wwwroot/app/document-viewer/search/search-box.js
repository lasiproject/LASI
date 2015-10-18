/// <amd-dependency path="./search-box.html" />
//var template = ;
'use strict';
System.register(['./search-box.html'], function(exports_1) {
    var search_box_html_1;
    var SearchBoxController;
    function searchBox() {
        return {
            template: search_box_html_1.default,
            restrict: 'E',
            scope: true,
            bindToController: {
                searchContext: '=',
                find: '='
            },
            controller: SearchBoxController,
            controllerAs: 'search'
        };
    }
    exports_1("default", searchBox);
    return {
        setters:[
            function (search_box_html_1_1) {
                search_box_html_1 = search_box_html_1_1;
            }],
        execute: function() {
            SearchBoxController = (function () {
                function SearchBoxController($q) {
                    this.$q = $q;
                }
                SearchBoxController.prototype.getWords = function () {
                    return (this.phrases || []).flatMap(function (p) { return p.words; });
                };
                SearchBoxController.prototype.search = function (searchOptions, searchContext) {
                    var deferred = this.$q.defer();
                    var value = searchOptions.value;
                    var term = typeof value === 'string' ? value :
                        typeof value !== 'undefined' ? value.detailText : undefined;
                    if (!term) {
                        deferred.reject('search term was undefined');
                    }
                    else if (!searchContext) {
                        deferred.reject('nothing to search');
                        this.phrases.forEach(function (phrase) { return phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', ''); });
                    }
                    else {
                        this.phrases = this.phrases || searchContext.flatMap(function (m) { return m.paragraphs; }).flatMap(function (p) { return p.sentences; }).flatMap(function (s) { return s.phrases; });
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
                        deferred.resolve(results.map(function (r) { return r.text; }));
                    }
                    return deferred.promise;
                };
                SearchBoxController.$inject = ['$q'];
                return SearchBoxController;
            })();
        }
    }
});
//# sourceMappingURL=search-box.js.map