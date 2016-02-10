/// <reference path="../../typings/tsd.d.ts" />
/// <reference path="./models.d.ts" />
import 'core-js';
import 'font-awesome';
import 'github:twbs/bootstrap@3.3.6/css/bootstrap.css';
import 'dist/app.css!';
import 'bootstrap';
import 'angular';
import 'angular-ui-router';
import 'angular-bootstrap';
import 'angular-bootstrap-contextmenu';
import 'angular-file-upload';
import 'api-check';
import 'angular-messages';
import 'angular-formly';
import 'angular-formly-templates-bootstrap';
import 'app/utilities/augmentations';
import $ from 'jquery';
import { bootstrap as ngBootstrap } from 'angular';
import debug from './debug/debug.module';
import widgets from './widgets/widgets.module';
import documentList from './document-list/document-list.module';
import documentUpload from './document-upload/document-upload.module';
import documentViewer from './document-viewer/document-viewer.module';
import documentViewerSearch from './document-viewer/search/search.module';
import { navbar } from './sections/navbar';
import { logoff } from './sections/logoff';
import { UserService } from './user-service';
import TokenService from './token-service';
import configureStates from './configuration/router';
import configureHttp from './configuration/http';
import run from './startup';

// Define the primary 'app' module, specifying all top level dependencies.
const app: NgModuleConfig = {
    name: 'app',
    requires: [
        'ui.router', 'ui.bootstrap.modal', 'formly', 'formlyBootstrap', 'ngMessages',
        debug, widgets, documentList, documentUpload,
        documentViewer, documentViewerSearch
    ],
    directives: { navbar, logoff },
    services: { UserService, TokenService },
    factories: { tasks },
    configs: [configureStates, configureHttp],
    runs: [run]
};

tasks.$inject = ['tasksListService'];
function tasks(tasksListService: TasksListService) {

    return tasksListService.getActiveTasks()
        .then(tasks => this.tasks = tasks.sort((x, y) => x.id.localeCompare(y.id)), console.error.bind(console));
}

const modules = [app];


function bootstrap() {
    modules.forEach(registerAngularModule);
    ngBootstrap(document.body, ['app'], {
        strictDi: true,
        debugInfoEnabled: true
    });
    $(document.body).show();
}
bootstrap();

function registerAngularModule(module: NgModuleConfig | string) {
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
        const app = angular.module(module.name, [...module.requires.map(registerAngularModule)])
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