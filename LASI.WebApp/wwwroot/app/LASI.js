/// <reference path="../../typings/tsd.d.ts" />
'use strict';
var $ = require('jquery');
//export var setupDraggableDialogs: () => void; 
exports.log = console.log.bind(console);
exports.editor = $('#free-editor').change(exports.log); // TODO: parameterize selector.
