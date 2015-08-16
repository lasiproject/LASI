namespace LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer', [
        'documentViewer.search',
        'widgets',
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu'
    ]).config(configure);

    configure.$inject = [];
    function configure() {
    };
}  