import {autoinject, bindable, customElement, Scope, DOM} from 'aurelia-framework';
@autoinject
@customElement('contextmenu')
export class Contextmenu {

    @bindable items = [];

    bind(source: Scope) {
        console.table(source);
    }
}

export interface ContextmenuOptions {
    template: string;
    templateUrl: string | (() => string);
}

// import {customElement, bindable, autoinject, ElementEvents} from 'aurelia-framework';

// @autoinject
// @customElement('contextmenu')
// export class Contextmenu {

//     constructor(private element: Element) { }
//     @bindable open: boolean;
//     @bindable template: string;
//     @bindable templateUrl: string | (() => string);
// }
