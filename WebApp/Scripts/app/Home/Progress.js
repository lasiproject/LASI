var LASI;
(function (LASI) {
    (function (Progress) {
        var Status = (function () {
            function Status(message, percent) {
                this.message = message;
                this.percent = percent;
                this.formattedPercent = percent.toString() + "%";
            }
            Status.fromJson = function (jsonString) {
                var js = JSON.parse(jsonString);
                return new Status(js.message, js.percent);
            };
            return Status;
        })();
        Progress.Status = Status;
    })(LASI.Progress || (LASI.Progress = {}));
    var Progress = LASI.Progress;
})(LASI || (LASI = {}));
$(function () {
    // Import class Status
    var Status = LASI.Progress.Status;

    // Sets listening for progress automatically.
    // This needs to be refactored into more re-usable code.
    var jobId = (function () {
        // Gets all ungoing jobs from the server and generates a new
        // Id number by using a bitwise xor
        return (function () {
            var id = $.makeArray($.getJSON("./GetJobStatus")).map(function (x, i) {
                return x.id;
            }).reduce(function (sofar, x) {
                return sofar ^ x;
            }, 0);
            setInterval(function (event) {
                $.getJSON("./GetJobStatus?jobId=" + jobId, function (data, status, jqXhr) {
                    var st = Status.fromJson(jqXhr.responseText);
                    var $progress = $(".progress-bar");
                    $progress.css("width", st.formattedPercent);
                    $progress.text(st.message);
                });
            }, 1000);
            return id += 1;
        })();
    })();
});
//# sourceMappingURL=Progress.js.map
