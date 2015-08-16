namespace LASI.documentViewer.search {
    'use strict';

    type SearchModelType = string|models.LexicalModel;
    interface SearchOptions {
        value: SearchModelType;
        lifted?: boolean;
    }
    export class SearchBoxController {
        static $inject = ['$q'];
        constructor(private $q: angular.IQService) { }
        private phrases: models.PhraseModel[];

        search(searchOptions: SearchOptions, searchContext: models.TextFragmentModel[]) {
            var deferred = this.$q.defer<SearchModelType[]>();

            var value = searchOptions.value;
            const term = typeof value === 'string' ? value : typeof value !== 'undefined' ? value.detailText : undefined;
            if (!term) {
                deferred.reject('search term was undefined');
            } else if (!searchContext) {
                deferred.reject('nothing to search');
                this.phrases.forEach(phrase => phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', ''));
            } else {
                if (!this.phrases) {
                    this.phrases = searchContext
                        .flatMap(m => m.paragraphs)
                        .flatMap(x => x.sentences)
                        .flatMap(e => e.phrases);
                }
                var results: models.PhraseModel[] = [];
                this.phrases.forEach(phrase => {
                    let matched = phrase.words.some(word => word.text === value);
                    if (!matched) {
                        phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', '');
                    } else {
                        phrase.style.cssClass += ' matched-by-search';
                        results.push(phrase);
                    }
                });
                deferred.resolve(typeof term === 'string' ? results.map(r => r.text) : results);
            }
            return deferred.promise;
        }
    }
    angular
        .module('documentViewer.search')
        .controller('SearchBoxController', SearchBoxController);

}
