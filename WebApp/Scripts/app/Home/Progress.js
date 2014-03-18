$(function () {
    var jobId = (function () {
        var id = $.makeArray($.getJSON("./GetJobStatus")).map(function (x, i) {
            return x.id;
        }).reduce(function (sofar, x) {
            return sofar ^ x;
        }, 0);
        return setInterval(function (event) {
            $.getJSON("./GetJobStatus?jobId=" + (jobId), function (data, status, jqXhr) {
                var $progress = $(".progress-bar");
                $progress.css("width", data.percent.toString() + "%");
                $progress.text(data.message);
            });
        }, 1000);
        return id += 1;
    })();
});
//# sourceMappingURL=Progress.js.map
