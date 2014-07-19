var LASI;
(function (LASI) {
    (function (Progress) {
        "use strict";
        var Status = (function () {
            function Status(message, percent) {
                this.message = message;
                this.percent = percent;
                this.percentString = percent.toString() + "%";
            }
            Status.fromJson = function (jsonString) {
                var json = JSON.parse(jsonString);
                return new Status(json.message, json.percent);
            };
            return Status;
        })();
        Progress.Status = Status;
    })(LASI.Progress || (LASI.Progress = {}));
    var Progress = LASI.Progress;
})(LASI || (LASI = {}));
$(function () {
    "use strict";

    // Import class Status
    var Status = LASI.Progress.Status;

    // Sets listening for progress automatically.
    // This needs to be refactored into more re-usable code.
    var jobId = (function () {
        // Gets all ungoing jobs from the server and generates a new
        // Id number by using a bitwise xor
        return (function () {
            var id = $.makeArray($.getJSON("\\Home\\GetJobStatus")).map(function (x) {
                return x.id;
            }).reduce(function (hash, x) {
                return hash ^ x;
            }, 0);

            setInterval(function (event) {
                $.getJSON("\\Home\\GetJobStatus?jobId=" + jobId, function (data, status, jqXhr) {
                    var st = Status.fromJson(jqXhr.responseText);
                    $(".progress-bar").css("width", st.percentString);
                    $("#progress-text").text(st.message);

                    // If one job is complete, check on all of others and if they are complete, prompt the user to proceed.
                    if (st.percent > 99.0 && $.makeArray($.getJSON("\\Home\\GetJobStatus")).map(function (e) {
                        return e.percent;
                    }).some(function (x) {
                        return x >= 100;
                    })) {
                        $("#resultsnavitem").click();
                    }
                });
            }, 1000);
            return id += 1;
        })();
    })();
});
//# sourceMappingURL=Progress.js.map
