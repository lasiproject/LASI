import {bindable, customElement} from 'aurelia-framework';
import $ from 'jquery';
import 'jquery-contextmenu';

@customElement('context-menu') export default class ContextMenu {

  constructor(readonly element: Element) {
    $(element).contextmenu(e => {
      console.log(e);
    });
  }
  @bindable items: {}[] = [];

  activate(value: {}, source: {}[]) {
    console.table(source);
    this.items = source;
  }
}

export interface ContextMenuOptions {
  template: string;
  templateUrl: string | (() => string);
}