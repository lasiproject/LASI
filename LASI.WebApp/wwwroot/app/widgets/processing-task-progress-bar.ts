import template from './processing-task-progress-bar.html';

export const processingTaskProgressBar: ng.IComponentOptions = {
    bindings: {
        task: '='
    },
    controllerAs: 'taskBar',
    template
};

