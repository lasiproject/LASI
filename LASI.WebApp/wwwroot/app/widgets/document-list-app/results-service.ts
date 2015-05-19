(function (app) {
    'use strict';

    angular
        .module(LASI.documentList.ngName)
        .service('resultsService', resultsService);

    resultsService.$inject = ['$http', '$q', '$log'];

    function resultsService($http, $q, $log) {
        /* jshint validthis:true */
        this.tasks = [];
        var that = this;
        this.processDocument = function (documentId, documentName) {
            that.tasks[documentId] = { percentComplete: 0 };

            var deferred = $q.defer();
            $http.get('Results/' + documentId)
                .success(success)
                .error(error);
            return deferred.promise;

            function success(data) {
                var markupHeader = $('<div class="panel panel-default">' +
                    '<div class="panel-heading"><h4 class="panel-title"><a href="#' + documentId + '" data-toggle="collapse" data-parent="#accordion">' +
                    documentName +
                    '</a></h4></div></div>');
                var markupPanel = $('<div id="' + documentId + '" class="panel-collapse collapse in">' + data + '</div>' + '</div>');
                if (!$('#' + documentId).length) {
                    $('#accordion').append(markupHeader).append(markupPanel);
                } else {
                    $('#' + documentId).remove();
                    $('#accordion').append(markupPanel);
                }
                app.buildMenus();
                app.enableActiveHighlighting();
                that.tasks[documentId].percentComplete = 100;
                deferred.resolve('success');
            }
            function error(xhr, message, detail) {
                deferred.reject(message);
            }
        };
    }
})(LASI);