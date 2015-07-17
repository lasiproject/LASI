/// <reference path="../angularjs/angular.d.ts" />
/// <reference path="../angular-ui-bootstrap/angular-ui-bootstrap.d.ts" />

declare module ui.bootstrap.contextMenu {

    interface ItemScope<T> extends ng.IScope {
        item: T;
    }
    interface ContextMenuDirectiveLinkFn<T> extends ng.IDirectiveLinkFn {
        (scope: ItemScope<T>, element: ng.IAugmentedJQuery, attrs: ContextMenuDirectiveAttributes)
    }
    interface ContextMenuDirectiveAttributes extends ng.IAttributes {
        contextMenu: Array<MenuItem>;
    }
    interface ContextMenuDirectiveFactory<T> extends ng.IDirectiveFactory {
        ($parse: ng.IParseService): ContextMenuDirectiveLinkFn<T>;
    }
    interface ItemEvent extends JQueryEventObject { }
    type Scope = ItemScope<any>;
    interface ComputeName {
        (s: Scope, e: ItemEvent): string;
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
        (s: Scope, e: ItemEvent): void;
    }
    interface ToggleItem {
        /**
        * @function Determines if the item should be displayed.
        * @param s The item scope
        * @param e The event which trigged the visibility check. 
        * @returns true to show the item; false to hide the item.
        */
        (s: Scope, e: ItemEvent): boolean;
    }

    type MenuItem =
    [
        string | ComputeName,
        ItemAction
    ]|[
        string | ComputeName,
        ItemAction,
        ToggleItem
    ];

    type ContextMenu = MenuItem[];
}