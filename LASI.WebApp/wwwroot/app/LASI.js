var LASI;
(function (LASI) {
    'use strict';
    LASI.buildMenus;
    LASI.enableActiveHighlighting;
    //export var setupDraggableDialogs: () => void;
    LASI.log = console.log.bind(console);
    LASI.editor = $('#free-editor').change(LASI.log); // TODO: parameterize selector.
})(LASI || (LASI = {}));
