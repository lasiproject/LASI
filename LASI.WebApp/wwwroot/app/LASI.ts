interface ILASIApplication {
    documentList?: {
        ngName: string
    },
    log: (value: any) => void;
    editor: JQuery;
}
var LASI: ILASIApplication = LASI || function () {
    'use strict';
    var log = console.log.bind(console),
        editor = $('#free-editor').change(log);
    return {
        log: log,
        editor: editor
    };
} ();