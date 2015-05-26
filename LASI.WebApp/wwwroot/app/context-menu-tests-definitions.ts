/// <reference path = "typings/angular-bootstrap-contextMenu/angular-bootstrap-contextMenu.d.ts"/>
module ContextMenuTests {
    import ctx = ui.bootstrap.contextMenu;
    //var c: ng.IDirectiveFactory = ($parse: ng.IParseService): ng.IDirectiveLinkFn=> (s, e, a) => { };
    type T = string;

    var item1: ctx.IContextMenuItem = [
        (s: ctx.IItemScope<T>, e: JQueryEventObject) => "item1",
        (s: ctx.IItemScope<T>, e: JQueryEventObject) => console.log("item1 clicked"),
        (s: ctx.IItemScope<T>, e: JQueryEventObject) => true
    ];
    var item2: ctx.IContextMenuItem = [
        'item2',
        (s) => console.log(s)
    ];
    var item3: ctx.IContextMenuItem = [
        'item3',
        (s: ctx.IItemScope<T>, e: JQueryEventObject) => console.log("item3 clicked"),
        (s, e) => Object.keys(s).every(k1=> Object.keys(e).some(k2=> k1 === k2))
    ];
    var menu: ctx.IContextMenuItem[] = [
        item1, item2
    ];
}