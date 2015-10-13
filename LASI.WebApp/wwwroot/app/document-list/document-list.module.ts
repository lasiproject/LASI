'use strict';

import { documentsService } from './documents.service';
import { resultsService } from './results.service';
import { DocumentListServiceConfig, DocumentListServiceProvider } from './document-list-service.provider';
import { TasksListServiceProvider, tasksListServiceProvider } from './tasks-list-service.provider';
import { UploadController } from './upload.controller';
import { ListController } from './list.controller';
import { documentListTabsetItem } from './document-list-tabset-item.directive';
import { documentListMenuItem } from './document-list-menu-item.directive';

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
        'ngResource',
        'ui.bootstrap',
        'ui.bootstrap.contextMenu', 
        'ngFileUpload',
        'documentViewer',
        'debug'
    ],
    configFn: configure,
    factories: { resultsService, documentsService },
    providers: { documentListService: DocumentListServiceProvider, tasksListService: tasksListServiceProvider },
    directives: { documentListTabsetItem, documentListMenuItem },
    controllers: { ListController, UploadController },
}