//(function (log) {
//    $(function () {
//        var debugPanel = $('#debug-panel');
//        window.setInterval(function () {
//            $.getJSON('api/Tasks', { cache: false })
//               .then(function (tasks) {
//                   debugPanel.html(tasks.map(function (task) {
//                       return "<div>" + Object.keys(task).map(function (key) {
//                           return "<span>&nbsp&nbsp" + task[key] + "</span>";
//                       }) + "</div>";
//                   }).join());
//               });
//        }, 800);
//    });
//}(LASI.log));