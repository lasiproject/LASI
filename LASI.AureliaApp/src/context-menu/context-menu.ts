import { bindable, DOM, inject, customElement } from 'aurelia-framework';
import $ from 'jquery';
import 'jquery-contextmenu';
declare type Element = typeof DOM.Element;
declare const Element: Element;

@inject(Element)
@customElement('context-menu')
export default class ContextMenu {
  constructor(readonly element: Element) {
    $(this.element).contextmenu(e => {
      console.log(e);
    });
  }
  activate(value: {}, source: {}[]) {
    console.table(source);
    this.items = source;
  }

  @bindable items: {}[] = [];
}

export interface ContextMenuOptions {
  template: string;
  templateUrl: string | (() => string);
}
