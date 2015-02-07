var app = function (app) {
    'use strict';
    var account = require('/account/manage.js'),
        widgets = require('/widgets/document-upload.js');
    app.exports = { account: account, widgets: widgets };
};