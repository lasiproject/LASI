module LASI.Results.ContextMenuProvider {
    'use strict';
    //var $ = require('/Scripts\jquery-2.1.1.min.js');
    //var context = require('/Scripts\context.js');
    declare var context;
    context.init([{ name: 'Item1', action: function () { alert(this.name); } }]);
    export function applyMenu(element: JQuery, menuItems: {
        text: string;
        action: () => void
    }[]) { context.attach(element.selector, menuItems) }
};