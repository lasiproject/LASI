import {bindable, observable} from 'aurelia-framework';
import {LexicalModel, WordModel, PhraseModel, TextFragmentModel} from 'src/models';
import $ from 'jquery';
import 'typeahead';

import {TypeAhead} from 'shared/type-ahead';

export default class SearchBox {
    @bindable find: SearchModel;

    @bindable searchContext: TextFragmentModel[];

    @bindable phrases: PhraseModel[] = [];

    @bindable get words() {
        return this.phrases.flatMap(phrase => phrase.words);
    }

    *search({ value }: SearchOptions, searchContext: TextFragmentModel[]) {
        const searchTerm = typeof value === 'string' ? value : value.detailText;

        this.phrases = this.phrases || searchContext
            .flatMap(m => m.paragraphs)
            .flatMap(p => p.sentences)
            .flatMap(s => s.phrases);

        if (!searchTerm) {
            throw 'search term was undefined';
        } else if (!searchContext) {
            this.phrases.forEach(resetStyle);
            return yield;
        } else {
            for (let phrase of this.phrases) {
                const matched = phrase.words.some(word => word.text === searchTerm);
                if (!matched) {
                    resetStyle(phrase);
                } else {
                    phrase.style.cssClass += ' matched-by-search';
                    yield phrase.text;
                }
            }
        }
        function resetStyle(phrase: PhraseModel) {
            phrase.style.cssClass = phrase.style.cssClass.replace('matched-by-search', '');
        }
    }
}

export type SearchModel = string | LexicalModel;

export interface SearchOptions {
    value: SearchModel;
    lifted?: boolean;
}