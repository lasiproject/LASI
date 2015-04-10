// Initailizes the top level module of the application the LASI object.
var LASI = (function () {
    'use strict';
    var log = console.log.bind(console),
        editor = $('#free-editor');
    return {
        editor: editor,
        log: log,
        bindDefaultEvents: function () {
            editor.change(function (e) {
                log(e);
            });
        }
    };
}());
LASI.bindDefaultEvents();