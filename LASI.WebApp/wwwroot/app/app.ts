import { bootstrap as ngBootstrap} from 'angular';
import { registerAngularModule } from './angular-shim';
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


function bootstrap() {
    modules.forEach(registerAngularModule);
    ngBootstrap(document.body, ['app'], {
        strictDi: true,
        debugInfoEnabled: true
    });
    $(document.body).show();
}
bootstrap();
// Uncoment to create a enable jspm/systemjs-builder to create a self executing bundle.
//$(function () { bootstrap(); });