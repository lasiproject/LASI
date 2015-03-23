define([
    'augmentations',
    'jquery',
    'log',
    'bootstrap',
    'textEditor',
    'documentUpload',
    'documentList',
    'contextMenuProvider',
    'resultChartProvider'
], function (augmentations, $, log, bootstrap, textEditor, documentUpload, documentList, contextMenuProvider) {
    'use strict';
    return {
        moduels: [augmentations, $, log, bootstrap, textEditor, documentUpload, documentList, contextMenuProvider],
        log: log,
        textEditor: textEditor.editor,
        buildMenus: contextMenuProvider.buildMenus
    };
});