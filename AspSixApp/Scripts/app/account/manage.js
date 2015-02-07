(function () {
    'use strict';
    $('#cancel-profile-edits')
        .click(function () {
            document.location.href = 'http://' + document.domain + ':' + document.location.port;
        });
}());