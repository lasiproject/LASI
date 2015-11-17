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

export default {
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
} as NgModuleConfig;