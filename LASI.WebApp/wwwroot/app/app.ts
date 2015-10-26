
import 'github:twbs/bootstrap@3.3.5/css/bootstrap.css!';
import 'font-awesome';
import 'dist/app.css!';
import 'bootstrap';
import 'angular-ui-router';
import './utilities/augmentations';
import * as LASI from './LASI';
import { module, bootstrap as ngBootstrap } from 'angular';
import debug from './debug/debug.module';
import widgets from './widgets/widgets.module';
import documentList from './document-list/document-list.module';
import documentUpload from './document-upload/document-upload.module';
import documentViewer from './document-viewer/document-viewer.module';
import documentViewerSearch from './document-viewer/search/search.module';
import configureRouter from './routing/configuration';
import UserService from './user-service';
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
run.$inject = ['$state'];
function run($state: ng.ui.IStateService) {
    $state.go('app');
}
angular
    .module('app', ['ui.router', 'ui.bootstrap.modal', ...modules.map(m => m.name)])
    .service({ UserService })
    .run(run);
export function bootstrap() {
    modules.forEach(register);
    ngBootstrap(document, ['app', configureRouter], { strictDi: true, debugInfoEnabled: true });
}