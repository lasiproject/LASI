import {bindable, autoinject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@autoinject export class Modal {
    constructor(readonly controller: DialogController) { }

    @bindable component: Function | string;
}