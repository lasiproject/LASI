'use strict';
import { processingTaskProgressBar } from './processing-task-progress-bar';
//import './document-upload';
export default {
    name: 'widgets',
    requires: ['ui.bootstrap'],
    directives: { processingTaskProgressBar }
} as NgModuleConfig;