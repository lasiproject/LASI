import debug from './debug/debug.module';
import widgets from './widgets/widgets.module';
import documentList from './document-list/document-list.module';
import documentUpload from './document-upload/document-upload.module';
import documentViewer from './document-viewer/document-viewer.module';
import documentViewerSearch from './document-viewer/search/search.module';
import configureRouter from './configuration/state-config';
import startup from './configuration/startup';
import UserService from './user-service';
var modules = [debug, widgets, documentList, documentUpload, documentViewer, documentViewerSearch];

var app: AngularModuleOptions = {
    name: 'app',
    requires: ['ui.router', 'ui.bootstrap.modal', ...modules],
    services: { UserService },
    configFn: configureRouter,
    runFn: startup
};
export default app;