var LASI;
(function (LASI) {
    'use strict';
    LASI.buildMenus;
    LASI.enableActiveHighlighting;
    LASI.setupDraggableDialogs;
    LASI.log = console.log.bind(console);
    LASI.editor = $('#free-editor').change(LASI.log);
    var documentList;
    (function (documentList) {
        documentList.moduleName = 'documentList';
    })(documentList = LASI.documentList || (LASI.documentList = {}));
    var documentViewer;
    (function (documentViewer) {
        documentViewer.moduleName = 'documentViewer';
    })(documentViewer = LASI.documentViewer || (LASI.documentViewer = {}));
})(LASI || (LASI = {}));
