/// <reference path = '../../typings/angular-bootstrap-contextMenu/angular-bootstrap-contextmenu.d.ts'/>
module contextmenuTests {
    'use strict';

    type Item = ui.bootstrap.contextMenu.MenuItem;
    import ItemEvent = ui.bootstrap.contextMenu.ItemEvent;

    type T = string;
    import ItemScope = ui.bootstrap.contextMenu.IItemScope;


    var item1: Item = [
        (s: ItemScope<string>, e: JQueryEventObject) => 'item1',
        (s: ItemScope<string>, e: JQueryEventObject) => console.log('item1 clicked'),
        (s: ItemScope<string>, e: JQueryEventObject) => true
    ];
    var item2: Item = [
        'item2',
        (s) => console.log(s)
    ];
    var item3: Item = [
        'item3',
        (s: ItemScope<string>, e: JQueryEventObject) => console.log('item3 clicked'),
        (s, e) => Object.keys(s).every(k1 => Object.keys(e).some(k2 => k1 === k2))
    ];
    var menu: Item[] = [
        item1, item2
    ];
}