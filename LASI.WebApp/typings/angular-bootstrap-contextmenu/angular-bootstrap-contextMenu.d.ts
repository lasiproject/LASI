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
        contextMenu: Array<MenuItemList>;
    }
    interface IContextMenuDirectiveFactory<T> extends ng.IDirectiveFactory {
        ($parse: ng.IParseService): IContextMenuDirectiveLinkFn<T>;
    }
    interface ItemEvent extends JQueryEventObject { }
    type ItemScope = IItemScope<any>;
    interface ComputeName {
        (s: ItemScope, e: ItemEvent): string;
    }
    /**
    * A function to call when the item is clicked.
    */
    interface ItemAction {
        /**
        * @function Invoked when the item is clicked.
        * @param s The item scope
        * @param e The event which trigged the visibility check. 
        */
        (s: ItemScope, e: ItemEvent): void;
    }
    interface ToggleItem {
        /**
        * @function Determines if the item should be displayed.
        * @param s The item scope
        * @param e The event which trigged the visibility check. 
        * @returns true to show the item; false to hide the item.
        */
        (s: ItemScope, e: ItemEvent): boolean;
    }

    type MenuItemList =
    [
        string | ComputeName,
        ItemAction
    ]|[
        string | ComputeName,
        ItemAction,
        ToggleItem
    ];

    type ContextMenu = MenuItemList[];
}