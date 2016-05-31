import {bindable} from 'aurelia-framework';
import {SentenceModel} from 'src/models';

export class Sentence {
    @bindable sentence: Sentence;
    @bindable parentId: number;
}