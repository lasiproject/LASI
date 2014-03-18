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
    var Status = LASI.Progress.Status;
    var jobId = (function () {
        var id = $.makeArray($.getJSON("./GetJobStatus")).map(function (x, i) {
            return x.id;
        }).reduce(function (sofar, x) {
            return sofar ^ x;
        }, 0);
        return setInterval(function (event) {
            $.getJSON("./GetJobStatus?jobId=" + jobId, function (data, status, jqXhr) {
                var st = Status.fromJson(data);
                var $progress = $(".progress-bar");
                $progress.css("width", st.percent);
                $progress.text(st.message);
            });
        }, 1000);
        return id += 1;
    })();
});
//# sourceMappingURL=Progress.js.map
