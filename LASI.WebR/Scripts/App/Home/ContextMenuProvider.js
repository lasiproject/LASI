var LASI;
(function (LASI) {
    var Results;
    (function (Results) {
        var ContextMenuProvider;
        (function (ContextMenuProvider) {
            'use strict';
            context.init([{ name: 'Item1', action: function () {
                alert(this.name);
            } }]);
            function applyMenu(element, menuItems) {
                context.attach(element.selector, menuItems);
            }
            ContextMenuProvider.applyMenu = applyMenu;
        })(ContextMenuProvider = Results.ContextMenuProvider || (Results.ContextMenuProvider = {}));
    })(Results = LASI.Results || (LASI.Results = {}));
})(LASI || (LASI = {}));
;
//# sourceMappingURL=ContextMenuProvider.js.map