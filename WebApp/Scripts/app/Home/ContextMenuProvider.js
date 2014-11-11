var LASI;
(function (LASI) {
    (function (Results) {
        (function (ContextMenuProvider) {
            'use strict';
            var $ = require('/Scripts\jquery-2.1.1.min.js');
            var context = require('/Scripts\context.js');
            context.init([{ name: 'Item1', action: function () {
                        alert(this.name);
                    } }]);
            function applyMenu(element, menuItems) {
                context.attach(element.selector, menuItems);
            }
            ContextMenuProvider.applyMenu = applyMenu;
        })(Results.ContextMenuProvider || (Results.ContextMenuProvider = {}));
        var ContextMenuProvider = Results.ContextMenuProvider;
    })(LASI.Results || (LASI.Results = {}));
    var Results = LASI.Results;
})(LASI || (LASI = {}));
;
//# sourceMappingURL=ContextMenuProvider.js.map
