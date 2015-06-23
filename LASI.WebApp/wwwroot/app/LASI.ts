module LASI {
    'use strict';

    export var buildMenus: () => void;
    export var enableActiveHighlighting: () => void;
    export var setupDraggableDialogs: () => void;

    export var log = console.log.bind(console);
    export var editor = $('#free-editor').change(log);

    export module documentList {
        export var moduleName = 'documentList';
    }
    export module documentViewer {
        export var moduleName = 'documentViewer';
    }
}