$(function () {
    (function (log) {
        $('.document-list-item > a').click(function (event) {
            event.preventDefault();
            var $listItem = $(this);
            var $element = $(event.target);
            var $parentListItem = $($(event.target).parent());
            var documentName = $element[0].text;
            var documentId = $element.next()[0].textContent.trim();
            log('clicked Name: ' + documentName + ', Id: ' + documentId);
            var $progress = $parentListItem
                .find('.progress hidden')
                .find('.progress-bar')
                .removeClass('.hidden').css('width', '100');
            var contentRequest =
                $.get('Results/Single/' + documentId)
                    .done(function (data, status, xhr) {
                        var markupHeader = $('<div class="panel panel-default">' +
                            '<div class="panel-heading"><h4 class="panel-title"><a href="#' + documentId + '" data-toggle="collapse" data-parent="#accordion">' +
                            documentName +
                            '</a></h4></div></div>');
                        var markupPanel = $('<div id="' + documentId + '" class="panel-collapse collapse in">' + data + '</div>' + '</div>');
                        if (!$('#' + documentId).length) {
                            $('#accordion').append(markupHeader).append(markupPanel);
                        } else {
                            $('#' + documentId).remove();
                            $('#accordion').append(markupPanel);
                        }
                        xhr.progress('100%');
                        app.buildMenus();
                        app.enableActiveHighlighting();
                    })
                    .fail(function (xhr, message, detail) {
                        log(message);
                    }).progress(function (data) {
                        $progress.css('width', data);
                    });
        });

    }(console.log.bind(console)));
});