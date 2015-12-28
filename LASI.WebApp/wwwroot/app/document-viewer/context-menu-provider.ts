/// <reference path="../../../typings/jquery/jquery.d.ts"/>
/// <reference path="../lasi.ts"/>

interface JQuery { contextmenu: any; }
(function (app) {
    'use strict';
    app.buildMenus =
        $(app.buildMenus);
} (LASI));