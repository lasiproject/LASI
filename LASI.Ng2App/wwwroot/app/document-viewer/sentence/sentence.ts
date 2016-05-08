import { Component} from 'angular2/core';
import { Input } from 'app/ng2-utils';
import template from './sentence.html';
import { PhraseComponent } from 'app/document-viewer/components';
@Component({
    selector: 'sentence',
    directives: [PhraseComponent],
    template
})
export class SentenceComponent {
    @Input sentence;
    @Input parentId;
}