/// <reference path="../../../typings/jquery/jquery.d.ts" />
(function (log) {
    $(function () {
        var debugPanel = $('#debug-panel');
        var visible = true;
        var toggleButton = $('#toggle-debug-panel');
        toggleButton.click(function () {
            visible = !visible;
            debugPanel.toggle();
            toggleButton.text(visible ? 'hide' : 'show')
                .toggleClass('btn-danger')
                .toggleClass('btn-info');
        })
        window.setInterval(function () {
            $.getJSON('api/Tasks', { cache: false })
               .then(function (tasks) {
                   debugPanel.html(tasks.map(function (task) {
                       return "<div>" + Object.keys(task).map(function (key) {
                           return "<span>&nbsp&nbsp" + task[key] + "</span>";
                       }) + "</div>";
                   }).join());
               });
        }, 800);
    });
}(console.log.bind(console)));