module LASI.Progress {
    'use strict';
    export class Status {
        percentString: string;
        constructor(public message: string, public percent: number, public id?: number) {
            this.percentString = percent.toString() + '%';
        }
        static fromJson(jsonString: string): Status {
            var json = JSON.parse(jsonString);
            return new Status(json.message, json.percent, json.id);
        }
    }

    $(function () {
        'use strict';
        // Sets listening for progress automatically.
        // This needs to be refactored into more re-usable code.
        var jobId = (function (statusUrl) {
            var jobId,
                promise = LASI.utilities.postJson(statusUrl, { message: 'initializing', percent: 0.0 }, data => jobId = data.id);
            // Create a new job on the server to track progress.
            promise.then(() => {
                setInterval(event => {
                    $.getJSON(statusUrl + '?id=' + jobId,(data, status, jqXhr) => {
                        var st = LASI.Progress.Status.fromJson(jqXhr.responseText);
                        $('#progress-bar').css('width', st.percentString);
                        $('#progress-text').text(st.message);
                        $.getJSON(statusUrl, data => {
                            var resultPageAnchor: HTMLElement,
                                // If one job is complete, check on all of others
                                // and if they are complete, prompt the user to proceed.
                                allJobsAreDone = st.percent >= 99.0 &&
                                    data.map(e => e.percent)
                                        .every(x => x >= 99.0);
                            if (allJobsAreDone) {
                                resultPageAnchor = $('#resultsnavitem')[0].children[0][0];
                                if (resultPageAnchor) { resultPageAnchor.click(); }
                            }
                        });
                    });
                }, 1000);
            });
            return jobId;
        })('/api/jobs');
    });
};