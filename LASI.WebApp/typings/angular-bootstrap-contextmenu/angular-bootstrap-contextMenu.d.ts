/// <reference path="../angularjs/angular.d.ts" />
/// <reference path="../angular-ui-bootstrap/angular-ui-bootstrap.d.ts" />

declare module 'angular-bootstrap-contextmenu' {
    export type ItemScope<T> = angular.ui.bootstrap.contextMenu.ItemScope<T>;
    export type ContextMenuDirectiveLinkFn<T> = angular.ui.bootstrap.contextMenu.ContextMenuDirectiveLinkFn<T>;
    export type ContextMenuDirectiveAttributes = angular.ui.bootstrap.contextMenu.ContextMenuDirectiveAttributes;
    export type ContextMenuDirectiveFactory<T> = angular.ui.bootstrap.contextMenu.ContextMenuDirectiveFactory<T>;
    export type ItemEvent = angular.ui.bootstrap.contextMenu.ItemEvent;
    export type Scope = angular.ui.bootstrap.contextMenu.Scope;
    export type ComputeName = angular.ui.bootstrap.contextMenu.ComputeName;
    export type ItemAction = angular.ui.bootstrap.contextMenu.ItemAction;
    export type ToggleItem = angular.ui.bootstrap.contextMenu.ToggleItem;
    export type ContextMenu = angular.ui.bootstrap.contextMenu.ContextMenu;
    export default '';
}
declare module angular.ui.bootstrap.contextMenu {
    interface ItemScope<T> extends angular.IScope {
        item: T;
    }
    interface ContextMenuDirectiveLinkFn<T> extends angular.IDirectiveLinkFn {
        (scope: ItemScope<T>, element: angular.IAugmentedJQuery, attrs: ContextMenuDirectiveAttributes)
    }
    interface ContextMenuDirectiveAttributes extends angular.IAttributes {
        contextMenu: Array<MenuItem>;
    }
    interface ContextMenuDirectiveFactory<T> extends angular.IDirectiveFactory {
        ($parse: angular.IParseService): ContextMenuDirectiveLinkFn<T>;
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
        ] | [
            string | ComputeName,
            ItemAction,
            ToggleItem
        ];

    type ContextMenu = MenuItem[];
}