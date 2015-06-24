var LASI;
(function (LASI) {
    'use strict';
    LASI.buildMenus;
    LASI.enableActiveHighlighting;
    LASI.setupDraggableDialogs;
    LASI.log = console.log.bind(console);
    LASI.editor = $('#free-editor').change(LASI.log);
})(LASI || (LASI = {}));
