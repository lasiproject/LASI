'use strict';
var search_box_directive_1 = require('./search-box.directive');
exports.default = {
    name: 'documentViewer.search',
    requires: ['ui.bootstrap.typeahead'],
    directives: { searchBox: search_box_directive_1.searchBox }
};
