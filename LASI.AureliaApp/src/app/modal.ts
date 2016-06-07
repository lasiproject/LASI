import {bindable, autoinject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';

@autoinject export class Modal {
    constructor(readonly dialogController: DialogController){}

    @bindable component:Function | string;
}