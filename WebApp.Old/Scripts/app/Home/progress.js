var LASI;
(function (LASI) {
    var Progress;
    (function (Progress) {
        'use strict';
        var Status = (function () {
            function Status(message, percent, id) {
                this.message = message;
                this.percent = percent;
                this.id = id;
                this.percentString = percent.toString() + '%';
            }
            Status.fromJson = function (jsonString) {
                var json = JSON.parse(jsonString);
                return new Status(json.message, json.percent, json.id);
            };
            return Status;
        })();
        Progress.Status = Status;
        $(function () {
            'use strict';
            // Sets listening for progress automatically.
            // This needs to be refactored into more re-usable code.
            var jobId = (function (statusUrl) {
                var jobId, promise = LASI.utilities.postJson(statusUrl, { message: 'initializing', percent: 0.0 }, function (data) { return jobId = data.id; });
                // Create a new job on the server to track progress.
                promise.then(function () {
                    setInterval(function (event) {
                        $.getJSON(statusUrl + '?id=' + jobId, function (data, status, jqXhr) {
                            var st = LASI.Progress.Status.fromJson(jqXhr.responseText);
                            $('#progress-bar').css('width', st.percentString);
                            $('#progress-text').text(st.message);
                            $.getJSON(statusUrl, function (data) {
                                var resultPageAnchor, 
                                // If one job is complete, check on all of others
                                // and if they are complete, prompt the user to proceed.
                                allJobsAreDone = st.percent >= 99.0 &&
                                    data.map(function (e) { return e.percent; })
                                        .every(function (x) { return x >= 99.0; });
                                if (allJobsAreDone) {
                                    resultPageAnchor = $('#resultsnavitem')[0].children[0][0];
                                    if (resultPageAnchor) {
                                        resultPageAnchor.click();
                                    }
                                }
                            });
                        });
                    }, 1000);
                });
                return jobId;
            })('/api/jobs');
        });
    })(Progress = LASI.Progress || (LASI.Progress = {}));
})(LASI || (LASI = {}));
;
//# sourceMappingURL=progress.js.map