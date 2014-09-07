

module LASI.Progress {
    "use strict";
    export class Status {
        percentString: string;
        constructor(public message: string, public percent: number) {
            this.percentString = percent.toString() + "%";
        }
        static fromJson(jsonString: string): Status {
            var json = JSON.parse(jsonString);
            return new Status(json.message, json.percent);
        }
    }
}
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
            var id = $.makeArray($.getJSON("\\Home\\GetJobStatus"))
                .map((x) => x.id)
                .reduce((hash: number, x: number) => hash ^ x, 0);

            setInterval(event => {
                $.getJSON("\\Home\\GetJobStatus?jobId=" + jobId,
                    function (data, status, jqXhr) {
                        var st = Status.fromJson(jqXhr.responseText);
                        $("#progress-bar").css("width", st.percentString);
                        $("#progress-text").text(st.message);
                        // If one job is complete, check on all of others and if they are complete, prompt the user to proceed.
                        if (st.percent > 99.0 && $.makeArray($.getJSON("\\Home\\GetJobStatus"))
                            .map(e => e.percent)
                            .some(x => x >= 100.0)) {
                            <HTMLElement>$("#resultsnavitem")[0].children[0][0].click();
                        }
                    });
            }, 1000);
            return id += 1;
        })();
    })();
});
