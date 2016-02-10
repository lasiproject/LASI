var LASI = (function (LASI) {
    'use strict';
    LASI.validateFileExtension = (function () {
        const accepted = Object.freeze(['.txt', '.docx', '.pdf', 'doc']);
        return function (fileName) {
            const extension = fileName.substring(fileName.lastIndexOf('.')).toLowerCase();
            return accepted.some(function (ext) {
                return ext === extension;
            });
        };
    })();
    $(function () {
        const $uploadList = $('#document-upload-list');
        LASI.$uploadList = $uploadList;
        $(document)
            .find('.btn-file :file')
            .change(function () {
                const $input = $(this),
                    fileCount = $uploadList.find('span.file-index').length,
                    files = (<HTMLInputElement>$input[0]).files;
                LASI.files = files;
                LASI.fileCount = fileCount;
                const generateUploadListItemMarkup = function (file, index) {
                    return `<div class="list-group-item text-left">
                                <span class="glyphicon glyphicon-remove remove-file" />&nbsp;&nbsp;&nbsp;<span class="file-index">
                                ${[fileCount, index, 1].sum()}
                                </span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="file-name">
                                ${file.name}</span>
                            </div>`;
                },
                    label = $input.val().replace(/\\/g, '/').replace(/[.]*\//, '');
                $(files).filter((index, element) => LASI.validateFileExtension((<HTMLInputElement>element).name))
                    .toArray()
                    .filter(function (file) {
                        return !($uploadList.children('span').toArray().some(function () {
                            return $(this).text() === file.name;
                        }));
                    }).forEach(function (file, index) {
                        $uploadList.append(generateUploadListItemMarkup(file, index));
                        $('span.glyphicon.glyphicon-remove.remove-file')
                            .click(function () {
                                $(this).removeData(file.name)
                                    .parent()
                                    .parent()
                                    .find('span.file-name')
                                    .filter(function () { return $(this).text() === file.name; })
                                    .each(function () { return $(this).parent('div').remove(); });
                                $uploadList.find('span.file-index')
                                    .each(function (index) { $(this).text(index + 1); });
                            });
                    });
                $input.trigger('fileselect', [files && files.length, label]);
            });
    });
    return LASI;
})({});