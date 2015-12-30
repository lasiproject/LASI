﻿'use strict';

import { enableActiveHighlighting } from './result-chart-builder';
import { buildMenus } from './build-menus';
resultsService.$inject = ['$http', '$q'];
function resultsService($http: angular.IHttpService, $q: angular.IQService): ResultsService {
    var tasks: Task[] = [];
    var processDocument = function (documentId, documentName): angular.IPromise<DocumentModel> {
        tasks[documentId] = {
            id: documentId,
            name: documentName,
            percentComplete: 0,

        };

        var deferred = $q.defer<DocumentModel>();
        $http.get<DocumentModel>('/analysis/' + documentId)
            .success(success)
            .error(error);
        return deferred.promise;

        function success(data) {
            var markupHeader = $('<div class="panel panel-default">' +
                '<div class="panel-heading"><h4 class="panel-title"><a href="#' +
                documentId +
                '" data-toggle="collapse" data-parent="#accordion">' +
                documentName +
                '</a></h4></div></div>');
            var markupPanel = $('<div id="' + documentId + '" class="panel-collapse collapse in">' +
                JSON.stringify(data) + '</div>' + '</div>');
            if (!$('#' + documentId).length) {
                $('#accordion').append(markupHeader).append(markupPanel);
            } else {
                $('#' + documentId).remove();
                $('#accordion').append(markupPanel);
            }
            buildMenus();
            enableActiveHighlighting();
            tasks[documentId].percentComplete = 100;
            deferred.resolve(data);
            alert(JSON.stringify(data));
        }
        function error(xhr, message, detail) {
            deferred.reject(message);
        }
    };
    function taskFor(documentId: string) {
        return tasks.first(task=> task.id === documentId);
    }
    return {
        tasks,
        taskFor,
        processDocument
    };
}
export { resultsService }