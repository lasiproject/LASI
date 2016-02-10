import { processingTaskProgressBar } from './processing-task-progress-bar';

export default {
    name: 'widgets',
    requires: ['ui.bootstrap'],
    directives: { processingTaskProgressBar }
} as NgModuleConfig;