var LASI;
(function (LASI) {
    var index;
    (function (index) {
        'use strict';
        // This function disables submit button 
        $(function () {
            $('input:submit').attr('disabled', 'true');
            $('input:file').change(function () {
                if ($(this).val()) {
                    $('input:submit').removeAttr('disabled');
                }
                else {
                    $('input:submit').attr('disabled', 'true');
                }
            });
        });
    })(index = LASI.index || (LASI.index = {}));
})(LASI || (LASI = {}));
;
