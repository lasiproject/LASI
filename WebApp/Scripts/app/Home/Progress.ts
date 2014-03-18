module LASI.Progress {
    export class Status {
        formattedPercent: string;
        constructor(public message: string, public percent: number) {
            this.formattedPercent = percent.toString() + "%";
        }
        static fromJson(jsonString: string): Status {
            var js = JSON.parse(jsonString);
            return new Status(js.message, js.percent);
        }
    }
}


$(() => {
    var Status = LASI.Progress.Status;
    var jobId = (function () {
        var id = $.makeArray($.getJSON("./GetJobStatus"))
            .map((x: any, i: number) => x.id)
            .reduce((sofar: number, x: number) => sofar ^ x, 0);
        return setInterval(event => {
            $.getJSON("./GetJobStatus?jobId=" + jobId,
                function (data, status, jqXhr) {
                    var st = Status.fromJson(data);
                    var $progress = $(".progress-bar");
                    $progress.css("width", st.percent);
                    $progress.text(st.message);
                });
        }, 1000);
        return id += 1;
    })();
});
