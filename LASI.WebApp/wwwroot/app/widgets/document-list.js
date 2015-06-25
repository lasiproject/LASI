/* global:draggable */
var LASI;
(function (LASI) {
    var documentList;
    (function (documentList) {
        'use strict';
        var draggable = window.draggable;
        var createHeaderMarkup = function (documentId, documentName) {
            return '<div>' +
                '<div class="panel-heading">' +
                '<h4 class="panel-title">' +
                '<a href="#' + documentId + '">'
                + documentName + '</a></h4></div></div>';
        };
        $(function () {
            window.setTimeout(function () {
                LASI.setupDraggableDialogs = function () {
                    var enableDragging = function (e, h) {
                        var handle = $(e).find('.handle')[0];
                        draggable(e, handle);
                        e.style.position = '';
                    };
                    // Make all confirm delete modals draggable.
                    $('[id^=confirm-delete-modal]').toArray().forEach(enableDragging);
                    // Make the manage documents modal draggable.
                    var draggableDialog = $('#manage-documents-modal');
                    var dragHandle = draggableDialog.find('.handle')[0];
                    if (draggableDialog[0] || dragHandle) {
                        enableDragging(draggableDialog[0], dragHandle);
                    }
                };
                LASI.setupDraggableDialogs();
                var $tabs = $('.document-viewer-tab-heading');
                var $listItemRefs = $('.document-list-item > a');
                var click = function (event) {
                    event.preventDefault();
                    var $listItem = $(this);
                    var $element = $(event.target);
                    //var documentName = $element[0].text;
                    var documentName = $element[0].innerText;
                    var documentId = $element.find('span.hidden')[0].textContent.trim();
                    LASI.log('clicked Name: ' + documentName + ', Id: ' + documentId);
                    var $parentListItem = $($(event.target).parent());
                    var $progress = $parentListItem.find('.progress hidden')
                        .find('.progress-bar')
                        .removeClass('.hidden').css('width', '100');
                    $.get('Analysis/' + documentId).done(function (data, status, xhr) {
                        var headerMarkup = $(createHeaderMarkup(documentId, documentName));
                        var panelMarkup = $("<div id=\"" + documentId + "\">\n                           <document-viewer-directive document=\"document\"></document-viewer-directive></div></div>");
                        if (!$('#' + documentId).length) {
                            $('#accordion').append(headerMarkup).append(panelMarkup);
                        }
                        else {
                            $('#' + documentId).remove();
                            $('#accordion').append(panelMarkup);
                        }
                        xhr.progress('100%');
                        LASI.buildMenus();
                        LASI.enableActiveHighlighting();
                    }).fail(function (xhr, message, detail) {
                        LASI.log(message);
                    }).progress(function (data) {
                        $progress.css('width', data);
                    });
                };
                $listItemRefs.click(click);
                //$tabs.click(click);
            }, 1000);
        });
    })(documentList = LASI.documentList || (LASI.documentList = {}));
})(LASI || (LASI = {}));
