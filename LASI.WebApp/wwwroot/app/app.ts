import debug from './debug/debug.module';
import widgets from './widgets/widgets.module';
import documentList from './document-list/document-list.module';
import documentUpload from './document-upload/document-upload.module';
import documentViewer from './document-viewer/document-viewer.module';
import documentViewerSearch from './document-viewer/search/search.module';
import { navbar }  from './sections/navbar/navbar';
import { UserService } from './user-service';
import { configureStates } from './configuration/configure-states';
import { configureHttp } from './configuration/http-configuration';
import { startup } from './configuration/startup';
import * as LASI from './LASI';
import { module as ngModule, bootstrap  as ngBootstrap} from 'angular';

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
    configs: [configureStates, configureHttp],
    runs: [startup]
};



var modules = [app];


function buildModule(module: NgModuleConfig | string) {
    function isConfig(x): x is NgModuleConfig {
        return typeof x !== 'string';
    }
    function validate() {
        if (isConfig(module)) {
            if (!module.name) {
                throw new TypeError('name is required');
            } if (!module.requires) {
                throw new TypeError('requires must be an array. Did you intend to invoke the setter?');
            }
        } else if (typeof module !== 'string') {
            throw new TypeError('module must be a string or an AngularModuleOptions options object');
        }
    }
    if (isConfig(module)) {
        const configs = module.configs;
        const runs = module.runs;
        const configBlocks = configs && (Array.isArray(configs) ? configs : [configs]) || [];
        const runBlocks = runs && (Array.isArray(runs) ? runs : [runs]) || [];
        const app = ngModule(module.name, [...module.requires.map(buildModule)])
            .provider(module.providers || {})
            .factory(module.factories || {})
            .service(module.services || {})
            .filter(module.filters || {})
            .controller(module.controllers || {})
            .directive(module.directives || {})
            .value(module.values || {})
            .constant(module.constants || {});
        configBlocks.forEach(app.config.bind(module));
        runBlocks.forEach(app.run.bind(module));
        return module.name;
    } else {
        return module;
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