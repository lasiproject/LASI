/// <reference path="../../typings/tsd.d.ts" />
'use strict';
import * as $ from 'jquery';
export var buildMenus: () => void;
export var enableActiveHighlighting: () => void;
//export var setupDraggableDialogs: () => void; 
export var log: (message?: any, ...optionalParams: any[]) => void = console.log.bind(console);
//export var editor = $('#free-editor').change(log); // TODO: parameterize selector.