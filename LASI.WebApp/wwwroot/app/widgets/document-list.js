/* global: draggable, enableDragging */
(function (app) {
    'use strict';
    var log = app.log;
    var createHeaderMarkup = function (documentId, documentName) {
        return '<div class="panel panel-default">' +
            '<div class="panel-heading">' +
            '<h4 class="panel-title">' +
            '<a href="#' + documentId +
            '" data-toggle="collapse" data-parent="#accordion">' + documentName +
            '</a></h4></div></div>';
    };
    $(function () {
        app.setupDraggableDialogs = function () {
            var enableDragging = function (e) {
                var handle = $(e).find('.handle')[0];
                draggable(e, handle);
                e.style.position = '';
            };
            // Make all confirm delete modals draggable.
            $("[id^=confirm-delete-modal]").toArray().forEach(enableDragging);
            // Make the manage documents modal draggable.
            var draggableDialog = $('#manage-documents-modal');
            var dragHandle = draggableDialog.find('.handle')[0];
            if (draggableDialog[0] || dragHandle) {
                enableDragging(draggableDialog[0], dragHandle);
            }
        };
        app.setupDraggableDialogs();
        $('.document-list-item > a').click(function (event) {
            event.preventDefault();
            var $listItem = $(this);
            var $element = $(event.target);
            var documentName = $element[0].text;
            var documentId = $element.next()[0].textContent.trim();
            log('clicked Name: ' + documentName + ', Id: ' + documentId);
            var $parentListItem = $($(event.target).parent());
            var $progress = $parentListItem
                .find('.progress hidden')
                .find('.progress-bar')
                .removeClass('.hidden').css('width', '100');
            var contentRequest = $.get('Results/' + documentId).done(function (data, status, xhr) {
                var headerMarkup = $(createHeaderMarkup(documentId, documentName));
                var panelMarkup = $('<div id="' + documentId + '" class="panel-collapse collapse in">' + data + '</div>' + '</div>');
                if (!$('#' + documentId).length) {
                    $('#accordion').append(headerMarkup).append(panelMarkup);
                }
                else {
                    $('#' + documentId).remove();
                    $('#accordion').append(panelMarkup);
                }
                xhr.progress('100%');
                app.buildMenus();
                app.enableActiveHighlighting();
            }).fail(function (xhr, message, detail) {
                log(message);
            }).progress(function (data) {
                $progress.css('width', data);
            });
        });
    });
}(LASI));
