/*This file will contain all the javascript and jquery functions for Index.cshtml. 
Keeping the functions will help with organization and will allow us to create classes with collections of javascript functions and that way we can load those classes, 
which will optimize page load time. 
*/


// adds the flatMap function to  Array.prototype, endowing all Arrays instances with it.
// flatMap takes an array of items, projects each one to an array,using the provided collectionSelector function,
// and flattens the resulting array of arrays onto a single flat array. 
// A second, optional function performs a projection on each element of the resulting flat array.
// This function is analagous to SelectMany in Linq, and flatMap in Scala.
(function () {
    'use strict';
    if (!Array.prototype.flatMap) {
        Array.prototype.flatMap = function (collectionSelector, elementProjection) {
            var i, results = [];
            for (i = 0; i < this.length; i += 1) {
                var col = collectionSelector(this[i]);
                col.forEach(function (element, index) {
                    results.push(element);
                });
            }
            return elementProjection ? results.map(elementProjection) : results;
        }
    }
}());


//This function disables submit button 
$(document).ready(function () {
    'use strict';
    $('input:submit').attr('disabled', true);
    $('input:file').change(function () {
        if ($(this).val()) {
            $('input:submit').removeAttr('disabled');
        } else {
            $('input:submit').attr('disabled', true);
        }
    });
});




$("input:submit").click(function (e) {
    'use strict';// All top level functions should start with this directive. nested functions inherit it.
    //e.preventDefault();

    $('input:file').each(function (index, element) {
        var Files = $('input:file').toArray().flatMap(function (item) {
            var result = [], i; for (i = 0; i < item.files.length; i += 1) {
                result.push(item.files[i]);
            } return result;
        });
        $.ajax({
            url: '/Home/Upload/',
            type: "POST",
            data: Files,
            content: Files,
        });

    });
});
var jobIds = (function () {
    var serverJobs = [];

    $(document).ready(function () {
        'use strict';
        $.ajaxSetup({ cache: false });
        (function () {
            'use strict';
            var jobId = setInterval(function (event) {
                $.getJSON('\\Home\\GetJobStatus?jobId=' + jobId).success(function (data) {

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
