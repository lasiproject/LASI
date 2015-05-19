var LASI = LASI || function () {
    'use strict';
    var log = console.log.bind(console), editor = $('#free-editor').change(log);
    return {
        log: log,
        editor: editor
    };
}();
