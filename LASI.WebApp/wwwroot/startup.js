require.config({
    baseUrl: './',
    deps: ['app/main'], // cause the main file to be run.
    paths: {
        augmentations: 'app/utilities/augmentations', // contains prototype polyfills.
        log: 'app/utilities/log',
        textEditor: 'app/widgets/text-editor',
        documentList: 'app/widgets/document-list',
        documentUpload: 'app/widgets/document-upload',
        contextMenuProvider: 'app/results/context-menu-provider',
        resultChartProvider: 'app/results/result-chart-provider',
        draggable: 'lib/draggable/draggable',
        jquery: 'lib/jquery/jquery',
        bootstrap: 'lib/bootstrap/js/bootstrap',
        bootstrapContextmenu: 'lib/bootstrap-contextmenu/bootstrap-contextmenu'
    },
    shim: {
        bootstrap: ['jquery'],
        bootstrapContextmenu: ['jquery', 'bootstrap']
    }
});