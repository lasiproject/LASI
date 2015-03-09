$(function () {
    $('document-list-item').click(function (event) {
        var $listItem = $(this);
        var id = $listItem.find('item-id-hidden').text();
        var $progress = $listItem
            .find('progress hidden')
            .find('progress-bar')
            .removeClass('hidden').css('width', 0);
        var contentRequest = $.ajax('Results/Single/' + id);
        var markup = $('<div class="hidden">' + contentRequest + '</div>');
        $listItem.append(markup);
    });
    $('#progress hidden-')
});