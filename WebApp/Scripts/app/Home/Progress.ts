$(() => {
    var jobId = (function () {
        var id = $.makeArray($.getJSON("./GetJobStatus"))
            .map((x: any, i: number) => x.id)
            .reduce((sofar: number, x: number) => sofar ^ x, 0);
        return setInterval(event => {

            $.getJSON("./GetJobStatus?jobId=" + (jobId), function (data, status, jqXhr) {
                var $progress = $(".progress-bar");
                $progress.css("width", data.percent.toString() + "%");
                $progress.text(data.message);
            });
        }, 1000);
        return id += 1;
    })();
});