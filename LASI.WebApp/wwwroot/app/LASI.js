var LASI;
(function () {
    LASI = LASI || function () {
        'use strict';
        var log = console.log.bind(console);
        return {
            log: log,
            editor: $('#free-editor').change(log)
        };
    }();
})();
