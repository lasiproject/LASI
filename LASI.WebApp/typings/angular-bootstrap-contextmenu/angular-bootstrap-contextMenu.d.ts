/// <reference path="../angularjs/angular.d.ts" />
/// <reference path="../angular-ui-bootstrap/angular-ui-bootstrap.d.ts" />

declare module ui.bootstrap.contextMenu {
    
    interface IItemScope<T> extends ng.IScope {
        item: T;
    }
    interface IContextMenuDirectiveLinkFn<T> extends ng.IDirectiveLinkFn {
        (scope: IItemScope<T>, element: ng.IAugmentedJQuery, attrs: IContextMenuDirectiveAttributes)
    }
    interface IContextMenuDirectiveAttributes extends ng.IAttributes {
        contextMenu: Array<IContextMenuItem>;
    }
    interface IContextMenuDirectiveFactory<T> extends ng.IDirectiveFactory {
        ($parse: ng.IParseService): IContextMenuDirectiveLinkFn<T>;
    }
    type ItemScope = IItemScope<any>;
    type GetItemName = (scope: ItemScope, event: JQueryEventObject) => string;
    type ItemAction = (scope: ItemScope, event: JQueryEventObject) => void;
    type ToggleItem = (scope: ItemScope, event: JQueryEventObject) => boolean;
    type IContextMenuItem =
    [
        string,
        ItemAction
    ]|[
        string,
        ItemAction,
        ToggleItem
    ]|[
        GetItemName,
        ItemAction
    ]|[
        GetItemName,
        ItemAction,
        ToggleItem
    ];


    
}