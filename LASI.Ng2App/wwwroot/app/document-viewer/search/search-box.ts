import { input } from 'ng2-conventions-decorators';
import template from './search-box.html';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'search-box',
    template
})
export class SearchBoxComponent implements OnInit {
    ngOnInit() { }

    static $inject = ['$q'];
    constructor() { }
    phrases: models.PhraseModel[];
    words: models.WordModel[];

    getWords() {
        return (this.phrases || []).flatMap(p => p.words);
    }
    @input find: SearchModel;
    @input searchContext: models.TextFragmentModel[];

    search(searchOptions: SearchOptions, searchContext: models.TextFragmentModel[]) {
        const value = searchOptions.value;
        const term = typeof value === 'string'
            ? value
            : typeof value !== 'undefined'
                ? value.detailText
                : undefined;
        const promise = new Promise((resolve, reject) => {

            if (!term) {
                reject('search term was undefined');
            } else if (!searchContext) {
                reject('nothing to search');
                this.phrases.forEach(phrase => phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', ''));
            } else {
                this.phrases = this.phrases || searchContext
                    .flatMap(m => m.paragraphs)
                    .flatMap(p => p.sentences)
                    .flatMap(s => s.phrases);

                const results: models.PhraseModel[] = [];
                this.phrases.forEach(phrase => {
                    const matched = phrase.words.some(word => word.text === value);
                    if (!matched) {
                        phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', '');
                    } else {
                        phrase.style.cssClass += ' matched-by-search';
                        results.push(phrase);
                    }
                });
                resolve(results.map(r => r.text));
            }
        });
        return promise;
    }
}

type SearchModel = string | models.LexicalModel;
interface SearchOptions {
    value: SearchModel;
    lifted?: boolean;
}