import debug from './debug/debug.module';
import widgets from './widgets/widgets.module';
import documentList from './document-list/document-list.module';
import documentUpload from './document-upload/document-upload.module';
import documentViewer from './document-viewer/document-viewer.module';
import documentViewerSearch from './document-viewer/search/search.module';
import { navbar }  from './sections/navbar/navbar';
import UserService from './user-service';
import configureStates from './configuration/configure-states';
import startup from './configuration/startup';
import * as LASI from './LASI';
import { module as registerAngularModule, bootstrap  as ngBootstrap} from 'angular';

// Define the primary 'app' module, specifying all top level dependencies.
var app: NgModuleConfig = {
    name: 'app',
    requires: [
        'ui.router', 'ui.bootstrap.modal',
        debug, widgets, documentList, documentUpload,
        documentViewer, documentViewerSearch
    ],
    directives: { navbar },
    services: { UserService },
    configFn: configureStates,
    runFn: startup
};



var modules = [app];


function buildModule(m: NgModuleConfig | string) {
    function isConfig(x): x is NgModuleConfig {
        return typeof x !== 'string';
    }
    function validate() {
        if (isConfig(m)) {
            if (!m.name) {
                throw new TypeError('name is required');
            } if (!m.requires) {
                throw new TypeError('requires must be an array. Did you intend to invoke the setter?');
            }
        } else if (typeof m !== 'string') {
            throw new TypeError('module must be a string or an AngularModuleOptions options object');
        }
    }
    if (isConfig(m)) {
        registerAngularModule(m.name, [...m.requires.map(buildModule)])
            .provider(m.providers || {})
            .factory(m.factories || {})
            .service(m.services || {})
            .filter(m.filters || {})
            .controller(m.controllers || {})
            .directive(m.directives || {})
            .value(m.values || {})
            .constant(m.constants || {})
            .config(m.configFn || (() => { }))
            .run(m.runFn || (() => { }));

        return m.name;
    } else {
        return m;
    }
}
function bootstrap() {
    modules.forEach(buildModule);
    ngBootstrap(document.body, ['app'], {
        strictDi: true,
        debugInfoEnabled: true
    });
    $(document.body).show();
}
bootstrap();
// Uncoment to create a enable jspm/systemjs-builder to create a self executing bundle.
//$(function () { bootstrap(); });