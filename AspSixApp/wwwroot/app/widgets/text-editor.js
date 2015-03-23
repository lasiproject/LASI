define(['jquery', 'log'], function ($, log) {
    'use strict';
    var $editor = $('#free-editor');
    $editor.change(function (e) {
        log(e);
    });
    return {
        editor: $editor
    };
});