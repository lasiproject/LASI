declare module LASI.results.contextMenuProvider {
    function applyMenu(element: JQuery, menuItems: {
        text: string;
        action: () => void;
    }[]): void;
}
