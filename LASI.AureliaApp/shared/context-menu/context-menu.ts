import { bindable, DOM, autoinject, customElement, Scope } from 'aurelia-framework';
import $ from 'jquery';
import 'jquery-contextmenu';
declare type Element = typeof DOM.Element;
declare var Element: Element;
@autoinject
@customElement('contextmenu')
export class Contextmenu {
  constructor(readonly element: Element) {
    $(this.element).contextmenu(e => {
      console.log(e);
    });
  }
  @bindable items = [];
  activate(value, source) {

    console.table(source);
    this.items = source;
  }
}

export interface ContextmenuOptions {
  template: string;
  templateUrl: string | (() => string);
}