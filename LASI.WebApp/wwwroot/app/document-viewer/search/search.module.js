'use strict';
define(["require", "exports", './search-box'], function (require, exports, search_box_1) {
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.default = {
        name: 'documentViewer.search',
        requires: ['ui.bootstrap.typeahead'],
        directives: { searchBox: search_box_1.default }
    };
});
//# sourceMappingURL=search.module.js.map