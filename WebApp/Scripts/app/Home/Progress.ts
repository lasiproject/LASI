//$(function () {

//    var jobId = setInterval(function (event) {
//        $.getJSON("./GetJobStatus?jobId=" + jobId.toString(), { cache: false }, (data, status, jqXhr) => {

//            var $progress = $(".progress-bar");
//            $progress.css("width", Math.min(100, data.percent).toString() + "%");
//            $progress.text(data.message);
//        });
//    }, 1000);
    
//});