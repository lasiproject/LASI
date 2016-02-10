import * as angular from 'angular';
import { uploadPanel } from './upload-panel';

export default {
    name: 'documentUpload',
    requires: ['ngFileUpload'],
    directives: { uploadPanel }
} as NgModuleConfig;