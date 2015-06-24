module LASI.documentViewer {
    'use strict';

    angular
        .module('documentViewer', [
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu'
    ]).config(configure);

    configure.$inject = [];
    function configure() {
    };
}  