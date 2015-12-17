'use strict';

import { documentsService } from './documents-service';
import { resultsService } from './results-service';
import { DocumentListServiceConfig, DocumentListServiceProvider } from './document-list-service-provider';
import { tasksListServiceProvider } from './tasks-list-service-provider';
import { ListController } from './list-controller';
import { documentListMenuItem } from './document-list-menu-item';

configure.$inject = ['tasksListServiceProvider', 'documentListServiceProvider'];

function configure(tasksListServiceProvider: TasksListServiceProvider, documentListServiceProvider: DocumentListServiceConfig) {
    tasksListServiceProvider
        .setUpdateInterval(500)
        .setTasksListUrl('api/Tasks');
    documentListServiceProvider
        .setRecentDocumentCount(5)
        .setDocumentListUrl('api/UserDocuments/List');
}

export default {
    name: 'documentList',
    requires: [
        'ui.bootstrap',
        'ui.bootstrap.contextMenu',
        'ngFileUpload',
        'documentViewer',
        'debug'
    ],
    configs: [configure],
    factories: { resultsService, documentsService },
    providers: { documentListService: DocumentListServiceProvider, tasksListService: tasksListServiceProvider },
    directives: { documentListMenuItem },
    controllers: { ListController }
} as NgModuleConfig;