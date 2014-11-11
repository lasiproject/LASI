declare module LASI.Results.ContextMenuProvider {
    function applyMenu(element: JQuery, menuItems: {
        text: string;
        action: () => void;
    }[]): void;
}
