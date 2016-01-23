'use strict';

import { enableActiveHighlighting } from './result-chart-builder';
import { buildMenus } from './build-menus';
resultsService.$inject = ['$q', '$http'];
function resultsService($q:ng.IQService, $http: ng.IHttpService): ResultsService {
    var tasks: Task[] = [];
    var processDocument = function (id, name): ng.IPromise<DocumentModel> {
        tasks[id] = {
            id,
            name,
            percentComplete: 0,
        };

         var deferred = $q.defer<DocumentModel>();
        $http.get<DocumentModel>('/analysis/' + id)
            .success(success)
            .error(error);
        return deferred.promise;
        function success(data){
            var markupHeader = $(`
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h4 class="panel-title">
                      <a href="#${id}" data-toggle="collapse" data-parent="#accordion">${name}</a>
                    </h4>
                  </div>
                </div>`);
            var panelMarkup = $(`<div id="${id}" class="panel-collapse collapse in">${JSON.stringify(data)}</div>`);
            if (!$(`#${id}`).length) {
                $('#accordion').append(markupHeader).append(panelMarkup);
            } else {
                $(`#${id}`).remove();
                $('#accordion').append(panelMarkup);
            }
            buildMenus();
            enableActiveHighlighting();
            tasks[id].percentComplete = 100;   
            deferred.resolve(data);
            //return data;
        }
        function error(xhr, message, detail) {
            deferred.reject(message);
        }
    };
    function getTasksForDocument(documentId) {
        return $q.when(tasks.filter(task=> task.id === documentId));
    }
    return {
        tasks,
        getTasksForDocument,
        processDocument
    };
}
export { resultsService }