import {bindable} from 'aurelia-framework';
import {DocumentModel, PageModel} from 'models';

export class Page {
    @bindable document: DocumentModel;
    @bindable page: PageModel;
}