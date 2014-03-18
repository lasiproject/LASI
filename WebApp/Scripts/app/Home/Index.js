/*This file will contain all the javascript and jquery functions for Index.cshtml.
Keeping the functions will help with organization and will allow us to create classes
with collections of javascript functions and that way we can load those classes,
which will optimize page load time.
*/
var LASI;
(function (LASI) {
    (function (Index) {
         {
            jobIds:
            (function () {
                // All top level functions should start with this directive. nested functions inherit it.
                "use strict";

                var y = [[1, 2, 3, 4], [53, 556]].flatMap(function (element) {
                    return element.map(function (a) {
                        return a;
                    });
                }, function (element) {
                    return element.toString();
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
                $("#submitdocumentbutton").click(function (e) {
                    $("input:file").each(function (index, element) {
                        var file = element.files[0];
                        $.ajax("\\Home\\Upload", {
                            processData: false, data: file,
                            type: "POST",
                            success: function (d, s, t) {
                                console.log(t.status);
                            }
                        });
                        $(e.target).css("width", "0%");
                    });
                    //e.preventDefault();
                });

                $(function () {
                    var jobId = setInterval(function (event) {
                        $.getJSON("./GetJobStatus?jobId=" + (jobId), function (data, status, jqXhr) {
                            var st = LASI.Progress.Status.fromJson(data);
                            var $progress = $(".progress-bar");
                            $progress.css("width", Math.min(99, st.percent).toString() + "%");
                            $progress.text(data.message);
                        });
                    }, 1000);
                });
            }());
        }
    })(LASI.Index || (LASI.Index = {}));
    var Index = LASI.Index;
})(LASI || (LASI = {}));
;
//# sourceMappingURL=Index.js.map
