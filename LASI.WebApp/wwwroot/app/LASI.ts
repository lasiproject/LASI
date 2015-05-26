
interface ILASIApplication {
    documentList?: {
        ngName: string;
    };
    documentViewer?: {
        ngName: string;
    };
    log: (value: any) => void;
    editor: JQuery;
}

var LASI: ILASIApplication;
(function () {
    LASI = LASI || function () {
        'use strict';
        var log = console.log.bind(console);
        return {
            log: log,
            editor: $('#free-editor').change(log)
        };
    } ();
})();


