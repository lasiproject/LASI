/*This file will contain all the javascript and jquery functions for Index.cshtml.
Keeping the functions will help with organization and will allow us to create classes
 with collections of javascript functions and that way we can load those classes,
which will optimize page load time.
*/

module LASI.Index {
    (function () {

        // All top level functions should start with this directive. nested functions inherit it.
        "use strict";

        var y = [[1, 2, 3, 4], [53, 556]].flatMap(e=> e.map(a=> a), e=> e.toString());
        //This function disables submit button 
        $(function () {
            $("input:submit").attr("disabled", "true");
            $("input:file").change(function () {
                if ($(this).val()) {
                    $("input:submit").removeAttr("disabled");
                } else {
                    $("input:submit").attr("disabled", "true");
                }
            });
        });
        $("input:submit").click({}, function (event) {
            var fileCount = $("input:file")
                .map((index, element: HTMLInputElement) => element.files.length).toArray()
                .reduce((sum, current, index) => sum + current);
        });



        $("input:submit").click(function (e) {
            $("input:file").each(function (index, element) {
                var files = $("input:file").toArray().flatMap((item: HTMLInputElement) => {
                    var result = new Array<File>(), i: number;
                    for (i = 0; i < item.files.length; i += 1) {
                        result.push(item.files[i]);
                    }
                    return result;
                });
                var xhr = $.ajax({
                    url: "/Home/Upload/",
                    type: "POST",
                    data: files,
                    processData: false
                });

            });
        });
        var jobIds = (function () {


            $(document).ready(function () {
                var serverJobs = [];
                $.ajaxSetup({ cache: false });
                (function () {
                    var jobId = setInterval(function (event) {
                        $.getJSON("\\Home\\GetJobStatus?jobId=" + jobId, (data, status, jqXhr) => {
                            var $progress = $(".progress-bar");
                            $progress.width(data.Percent);
                            $progress.text(data.Message);
                        });
                    }, 1000);
                    serverJobs.push(jobId);
                });
                return serverJobs;

            });
        } ());
    } ());
}