module LASI.documentViewer {
    'use strict';

    angular
        .module(LASI.documentViewer.moduleName, [
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu'
    ]).config(configure);

    configure.$inject = [];
    function configure() {

    };
}  