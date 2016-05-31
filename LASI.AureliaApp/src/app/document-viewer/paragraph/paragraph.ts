import {bindable} from 'aurelia-framework';
import {ParagraphModel} from 'src/models';

export class Paragraph {
    @bindable paragraph: ParagraphModel;
    @bindable parentId: number;
}