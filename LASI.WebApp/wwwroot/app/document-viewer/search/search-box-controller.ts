module LASI.documentViewer.search {
    'use strict';
    //function searchBox(): ng.IDirective {

    SearchBoxController.$inject = ['$q'];

    //return {
    //    restrict: 'E',
    //    controller,
    //    controllerAs: 'search',
    //    scope: {
    //        find: '=',
    //        searchContext: '=',
    //        onFound: '='
    //    },
    //    bindToController: true
    //};
    function SearchBoxController($q: ng.IQService) {
        var vm = this;
        vm.searchContext = {
            value: undefined,
            set: function (value) {
                var context = <models.ITextFragmentModel[]>(value instanceof Array ? value : [value]);
                this.value = context;
                return vm.search;
            }, get: function () { return this.value; }
        };

        var search = (searchFor: string | models.ILexicalModel, options: ITextSearchOptions<string | models.ILexicalModel>) => {
            var deferred = $q.defer<string[]| models.ILexicalModel[]>();
            const term = typeof searchFor === 'string' ? searchFor : typeof searchFor !== 'undefined' ? searchFor.detailText : undefined;
            if (term === undefined) {
                deferred.reject('search term was undefined');
            }
            const results = vm.searchContext
                .flatMap(m => m.paragraphs)
                .flatMap(x => x.sentences)
                .flatMap(e => e.phrases)
                .filter(phrase => phrase.text === searchFor);
            results.forEach(model => {
                let matched = true;
                const unmatchedStyle = model.style.cssClass;
                const matchedStyle = unmatchedStyle + ' matched-by-search';
                model.style = {
                    get cssClass() {
                        var result = matched ? matchedStyle : unmatchedStyle;
                        matched = !matched;
                        return result;
                    }
                };
            });
            deferred.resolve(typeof term === 'string' ? results.map(r => r.text) : results);
            return deferred.promise;
        };
    }
    interface ITextSearchOptions<TFor> {
        lifted?: boolean;
    }

    angular
        .module('documentViewer.search')
        .controller('SearchBoxController', SearchBoxController);

}
