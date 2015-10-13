/// <amd-dependency path="angular" />
/// <amd-dependency path="angular-resource" />
/// <amd-dependency path="angular-bootstrap" />
/// <amd-dependency path="angular-bootstrap-contextmenu" />
/// <amd-dependency path="angular-file-upload" />
/// <amd-dependency path="jquery" />
/// <amd-dependency path="app/utilities/augmentations"

import { module, bootstrap } from 'angular';
import 'app/utilities/augmentations';
import debug from './app/debug/debug.module';
import widgets from './app/widgets/widgets.module';
import documentList from './app/document-list/document-list.module';
import documentUpload from './app/document-upload/document-upload.module';
import documentViewer from './app/document-viewer/document-viewer.module';
import documentViewerSearch from './app/document-viewer/search/search.module';

function register(m: AngularModuleOptions) {
    function validate() {
        if (!m.name) { throw new TypeError("name is required"); }
        if (!m.requires) { throw new TypeError("requires must be an array. Did you intend to invoke the setter?"); }
    }
    module(m.name, m.requires, m.configFn || (() => { }))
        .provider(m.providers || {})
        .factory(m.factories || {})
        .service(m.services || {})
        .filter(m.filters || {})
        .controller(m.controllers || {})
        .directive(m.directives || {})
        .value(m.values || {})
        .constant(m.constants || {})
        .run(m.runFn || (() => { }));
}
export function angularBootstrap() {
    function registerAll(...modules: AngularModuleOptions[]) {
        modules.forEach(register);
    }

    return function () {
        registerAll(debug, widgets, documentList, documentUpload, documentViewer, documentViewerSearch);
        bootstrap(document.body, ['documentList']);
    };
}