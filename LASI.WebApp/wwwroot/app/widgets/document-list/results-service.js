var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        angular
            .module(documentList.moduleName)
            .factory('resultsService', resultsService);
        resultsService.$inject = ['$http', '$q'];
        function resultsService($http, $q) {
            var tasks = [];
            var processDocument = function (documentId, documentName) {
                tasks[documentId] = { percentComplete: 0 };
                var deferred = $q.defer();
                $http.get('Analysis/' + documentId)
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
                    }
                    else {
                        $('#' + documentId).remove();
                        $('#accordion').append(markupPanel);
                    }
                    LASI.buildMenus();
                    LASI.enableActiveHighlighting();
                    tasks[documentId].percentComplete = 100;
                    deferred.resolve(data);
                }
                function error(xhr, message, detail) {
                    deferred.reject(message);
                }
            };
            return {
                tasks: tasks,
                processDocument: processDocument
            };
        }
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
//# sourceMappingURL=results-service.js.map