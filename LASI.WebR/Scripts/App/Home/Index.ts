module LASI.Index {
    'use strict';
    // This function disables submit button 
    $(function () {
        $('input:submit').attr('disabled', 'true');
        $('input:file').change(function () {
            if ($(this).val()) {
                $('input:submit').removeAttr('disabled');
            } else {
                $('input:submit').attr('disabled', 'true');
            }
        });
    });
};
