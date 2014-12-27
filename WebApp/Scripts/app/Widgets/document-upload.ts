module LASI.Widgets.DocumentUpload {
    'use strict';
    //var $ = require('/Scripts\jquery-2.1.1.min.js');
    $(function () {
        var $uploadElement = $('#document-upload-input'), $uploadList = $('#document-upload-list'), $uploadButton = $('#document-upload-button');
        $(document).on('change', '.btn-file :file', function () {
            var $input = $(this);
            var fileCount = $uploadList.find('span.file-index').length;
            var files = (<HTMLInputElement> $input[0]).files;
            var generateUploadListItemMarkup = function (file, index) {
                return '<div class="list-group-item text-left">' +
                    '<span class="glyphicon glyphicon-remove remove-file"/>&nbsp;&nbsp;&nbsp;<span class="file-index">' + [fileCount, index, 1].sum() +
                    '</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="file-name">' + file.name + '</span></div>';
            }
            $(files)
                .toArray()
                .filter(file=> !$uploadList.children('span').filter((index, span) => $(span).text() === file.name).length)
                .forEach((file, index) => {
                    $uploadList.append(generateUploadListItemMarkup(file, index));
                    $('span.glyphicon.glyphicon-remove.remove-file')
                        .click(function (e) {
                            $(this).removeData((<any>file).name);
                            $(this).parent().parent().find('span.file-name').filter((index, span) => $(span).text() === file.name).each((index, f) => $(f).parent('div').remove());
                            $uploadList.find('span.file-index').toArray().forEach(function (e, index) { $(e).text(index + 1); })
                        });
                });
            var label = $input.val().replace(/\\/g, '/').replace(/.*\//, '');
            $input.trigger('fileselect', [files && files.length, label]);
        });
    });
}