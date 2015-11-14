'use strict';
import 'app/angular-shim';
import * as angular from 'angular';
import { uploadPanel } from './upload-panel';

export default {
    name: 'documentUpload',
    requires: ['ngFileUpload'],
    directives: { uploadPanel }
} as AngularModuleOptions;