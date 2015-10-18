import 'github:twbs/bootstrap@3.3.5/css/bootstrap.css!';
import 'font-awesome';
import 'dist/app.css!';
import 'bootstrap';
import './utilities/augmentations';
import './LASI';
import { module, bootstrap as ngBootstrap } from 'angular';
import debug from './debug/debug.module';
import widgets from './widgets/widgets.module';
import documentList from './document-list/document-list.module';
import documentUpload from './document-upload/document-upload.module';
import documentViewer from './document-viewer/document-viewer.module';
import documentViewerSearch from './document-viewer/search/search.module';
var modules: AngularModuleOptions[] = [documentList, debug, widgets, documentUpload, documentViewer, documentViewerSearch];
function register(m: AngularModuleOptions) {
    function validate() {
        if (!m.name) {
            throw new TypeError('name is required');
        }
        if (!m.requires) {
            throw new TypeError('requires must be an array. Did you intend to invoke the setter?');
        }
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
export function bootstrap() {
    modules.forEach(register);
    ngBootstrap(document, modules.map(m => m.name));
}