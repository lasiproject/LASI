/*This file will contain all the javascript and jquery functions for Index.cshtml.
Keeping the functions will help with organization and will allow us to create classes
with collections of javascript functions and that way we can load those classes,
which will optimize page load time.
*/
var LASI;
(function (LASI) {
    (function (Index) {
        (function () {
            // All top level functions should start with this directive. nested functions inherit it.
            "use strict";

            var y = [[1, 2, 3, 4], [53, 556]].flatMap(function (e) {
                return e.map(function (a) {
                    return a;
                });
            }, function (e) {
                return e.toString();
            });

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
                var fileCount = $("input:file").map(function (index, element) {
                    return element.files.length;
                }).toArray().reduce(function (sum, current, index) {
                    return sum + current;
                });
            });

            $("input:submit").click(function (e) {
                $("input:file").each(function (index, element) {
                    var files = $("input:file").toArray().flatMap(function (item) {
                        var result, i;
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
                            $.getJSON("\\Home\\GetJobStatus?jobId=" + jobId, function (data, status, jqXhr) {
                                var $progress = $(".progress-bar");
                                $progress.width(data.Percent);
                                $progress.text(data.Message);
                            });
                        }, 1000);
                        serverJobs.push(jobId);
                    });
                    return serverJobs;
                });
            }());
        }());
    })(LASI.Index || (LASI.Index = {}));
    var Index = LASI.Index;
})(LASI || (LASI = {}));
//# sourceMappingURL=Index.js.map
