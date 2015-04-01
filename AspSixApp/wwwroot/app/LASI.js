var app = {};
(function () {
    'use strict';
    var $editor = $('#free-editor');
    $editor.change(function (e) {
    });
    //var account = require('/account/manage'),
    //    widgets = require('/widgets/document-upload');

    app.log = console.log.bind(console);
    app.editor = $editor;
    return app;
}(app || {}));
var LASI = app;