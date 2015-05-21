var LASI;
(function (LASI) {
    var results;
    (function (results) {
        var contextMenuProvider;
        (function (contextMenuProvider) {
            'use strict';
            var $ = require('/Scripts\jquery-2.1.1.min.js');
            var context = require('/Scripts\context.js');
            context.init([{ name: 'Item1', action: function () { alert(this.name); } }]);
            function applyMenu(element, menuItems) { context.attach(element.selector, menuItems); }
            contextMenuProvider.applyMenu = applyMenu;
        })(contextMenuProvider = results.contextMenuProvider || (results.contextMenuProvider = {}));
    })(results = LASI.results || (LASI.results = {}));
})(LASI || (LASI = {}));
;
//# sourceMappingURL=context-menu-provider.js.map