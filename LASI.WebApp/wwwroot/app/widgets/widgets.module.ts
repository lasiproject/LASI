import { processingTaskProgressBar } from './processing-task-progress-bar';

export default {
    name: 'widgets',
    requires: ['ui.bootstrap'],
    components: { processingTaskProgressBar }
} as NgModuleConfig;