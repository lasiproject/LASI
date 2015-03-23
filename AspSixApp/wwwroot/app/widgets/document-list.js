// Need to figure out how to register resultChartProvider as a dependency of this 
// or move the enableActiveHighlighting function to another module. So far it causes 
// crash when loading.
define(['jquery', 'draggable', 'log', 'contextMenuProvider', 'resultChartProvider'], function ($, draggable, log, contextMenuProvider, resultChartProvider) {
    'use strict';
    $(function () {
        var enableModalDraggability = function () {
            $("[id^=confirm-delete-modal]").toArray().forEach(function (modal) {
                var dragHandle = $(modal).find('.handle')[0];
                draggable(modal, dragHandle);
            });
            var draggableDialog = $('#manage-documents-modal');
            var dragHandle = draggableDialog.find('.handle')[0];
            if (draggableDialog[0] || dragHandle) {
                draggable(draggableDialog[0], dragHandle);
            }
        };
        enableModalDraggability();

        $('.document-list-item > a').click(function (event) {
            event.preventDefault();
            //var $listItem = $(this);
            var $element = $(event.target);
            var $parentListItem = $($(event.target).parent());
            var documentName = $element[0].text;
            var documentId = $element.next()[0].textContent.trim();
            log('clicked Name: ' + documentName + ', Id: ' + documentId);
            var $progress = $parentListItem
                .find('.progress hidden')
                .find('.progress-bar')
                .removeClass('.hidden').css('width', '100');
            //var contentRequest =
            $.get('Results/Single/' + documentId).done(function (data, status, xhr) {
                var markupHeader = $('<div class="panel panel-default">' +
                    '<div class="panel-heading"><h4 class="panel-title"><a href="#' + documentId + '" data-toggle="collapse" data-parent="#accordion">' +
                    documentName +
                    '</a></h4></div></div>');
                var markupPanel = $('<div id="' + documentId + '" class="panel-collapse collapse in">' + data + '</div>' + '</div>');
                log(status);
                if (!$('#' + documentId).length) {
                    $('#accordion').append(markupHeader).append(markupPanel);
                } else {
                    $('#' + documentId).remove();
                    $('#accordion').append(markupPanel);
                }
                xhr.progress('100%');
                contextMenuProvider.buildMenus();
                resultChartProvider.enableActiveHighlighting();
            }).fail(function (xhr, message, detail) {
                log(xhr);
                log(message);
                log(detail);
            }).progress(function (data) {
                $progress.css('width', data);
            });

        });
        var deleteUserDocument = function (documentId, element) {
            $.ajax({
                url: 'api/tasks/' + documentId,
                method: 'DELETE',
                cache: false
            }).done(function (data, status, xhr) {
                log(data);
                log(status);
                log(xhr);
                $(element).remove();
            });
        };

        $('.glyphicon.glyphicon-remove').click(function () {
            var element = this;
            var id = element.id.substring(element.id.indexOf('-') + 1);
            deleteUserDocument(id, element);
        });
    });
});