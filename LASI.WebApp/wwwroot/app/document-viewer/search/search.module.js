System.register(['./search-box'], function(exports_1) {
    'use strict';
    var search_box_1;
    return {
        setters:[
            function (search_box_1_1) {
                search_box_1 = search_box_1_1;
            }],
        execute: function() {
            exports_1("default",{
                name: 'documentViewer.search',
                requires: ['ui.bootstrap.typeahead'],
                directives: { searchBox: search_box_1.default }
            });
        }
    }
});
