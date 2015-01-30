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
            var jobId = (function () {
                return (function (statusUrl) {
                    var id;
                    // Create a new job on the server to track progress.
                    $.ajax({
                        type: "POST",
                        // Note: The call to JSON.stringify is necessary. If a pojo is used directly, jQuery will url encode it.
                        data: JSON.stringify({ message: 'initializing', percent: 0.0 }),
                        url: "/api/jobs",
                        contentType: "application/json",
                        success: function (data) { return id = data.id; }
                    }).then(function () {
                        setInterval(function (event) {
                            $.getJSON(statusUrl + '?id=' + id, function (data, status, jqXhr) {
                                var st = Status.fromJson(jqXhr.responseText);
                                $('#progress-bar').css('width', st.percentString);
                                $('#progress-text').text(st.message);
                                // If one job is complete, check on all of others and if they are complete, prompt the user to proceed.
                                $.getJSON(statusUrl, function (data) {
                                    if (st.percent > 99.0 && data.map(function (e) { return e.percent; }).every(function (x) { return x >= 99.0; })) {
                                        var resultPageAnchor = $('#resultsnavitem')[0].children[0][0];
                                        if (resultPageAnchor) {
                                            resultPageAnchor.click();
                                        }
                                    }
                                });
                            });
                        }, 1000);
                    });
                    return id;
                })('/api/jobs');
            })();
        });
    })(Progress = LASI.Progress || (LASI.Progress = {}));
})(LASI || (LASI = {}));
;
//# sourceMappingURL=Progress.js.map