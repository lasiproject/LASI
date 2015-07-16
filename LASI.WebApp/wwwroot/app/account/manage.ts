/// <reference path="../../../typings/jquery/jquery.d.ts" />
module LASI.accountManagement {
    'use strict';
    $(() => $('#cancel-profile-edits').click(() => document.location.href = `http://${document.domain}:${document.location.port}`));
}