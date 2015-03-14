$(function () {
    $('.document-list-item > a').click(function (event) {
        event.preventDefault();
        var $listItem = $(this);
        var $element = $(event.target);
        var $parentListItem = $($(event.target).parent());
        var documentName = $element[0].text;
        var documentId = $element.next()[0].textContent.trim();
        alert('clicked Name: ' + documentName + ', Id: ' + documentId);
        var $progress = $parentListItem
            .find('.progress hidden')
            .find('.progress-bar')
            .removeClass('.hidden').css('width', '100');
        var contentRequest =
            $.get('Results/Single/' + documentId)
                .done(function (data, status, xhr) {
                    var markup = $('<div class="panel panel-default">' +
                        '<div class="panel-heading"><h4 class="panel-title"><a href="#' + documentId + '" data-toggle="collapse" data-parent="#accordion">' +
                        documentName +
                        '</a></h4></div></div>' +
                        '<div id="' + documentId + '" class="panel-collapse collapse">' +
                        data + '</div>' +
                        '</div>');
                    $('#accordion').append(markup);
                    xhr.progress('100%');
                    app.buildMenus();
                    app.enableActiveHighlighting();
                })
                .fail(function (xhr, message, detail) {
                    alert(message);
                }).progress(function (data) {
                    $progress.css('width', data);
                });
    });


});