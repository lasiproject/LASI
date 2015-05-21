var LASI;
(function (LASI) {
    var Widgets;
    (function (Widgets) {
        var DocumentUpload;
        (function (DocumentUpload) {
            'use strict';
            var validateFileExtension = (function () {
                var acceptedFormats = Object.freeze(['.txt', '.docx', '.pdf', 'doc']);
                return function (fileName) {
                    var extension = fileName.substring(fileName.lastIndexOf('.')).toLowerCase();
                    return acceptedFormats.some(function (extension) { return extension === extension; });
                };
            }());
            $(function () {
                var $uploadElement = $('#document-upload-input'), $uploadList = $('#document-upload-list'), $uploadButton = $('#document-upload-button');
                $(document).on('change', '.btn-file :file', function () {
                    var $input = $(this);
                    var fileCount = $uploadList.find('span.file-index').length;
                    var files = $input[0].files;
                    var generateUploadListItemMarkup = function (file, index) {
                        return '<div class="list-group-item text-left">' +
                            '<span class="glyphicon glyphicon-remove remove-file"/>&nbsp;&nbsp;&nbsp;<span class="file-index">' + [fileCount, index, 1].sum() +
                            '</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="file-name">' + file.name + '</span></div>';
                    };
                    $(files)
                        .toArray()
                        .filter(function (file) { return !$uploadList.children('span').filter(function (index, span) { return $(span).text() === file.name; }).length; })
                        .forEach(function (file, index) {
                        $uploadList.append(generateUploadListItemMarkup(file, index));
                        $('span.glyphicon.glyphicon-remove.remove-file')
                            .click(function (e) {
                            $(this).removeData(file.name);
                            $(this).parent().parent().find('span.file-name').filter(function (index, span) { return $(span).text() === file.name; }).each(function (index, f) { return $(f).parent('div').remove(); });
                            $uploadList.find('span.file-index').toArray().forEach(function (e, index) { $(e).text(index + 1); });
                        });
                    });
                    var label = $input.val().replace(/\\/g, '/').replace(/.*\//, '');
                    $input.trigger('fileselect', [files && files.length, label]);
                });
            });
        })(DocumentUpload = Widgets.DocumentUpload || (Widgets.DocumentUpload = {}));
    })(Widgets = LASI.Widgets || (LASI.Widgets = {}));
})(LASI || (LASI = {}));
