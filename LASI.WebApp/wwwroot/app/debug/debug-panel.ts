'use strict';
import template from 'app/debug/debug-panel.html';

class DebugPanelController {
    static $inject = ['tasksListService'];
    constructor(tasksListService: TasksListService) {

        tasksListService.getActiveTasks()
            .then(tasks => this.tasks = tasks.sort((x, y) => x.id.localeCompare(y.id)), console.error.bind(console));
    }

    get buttonText() { return this.infoVisible ? 'Hide' : 'Show'; }

    toggle() { this.infoVisible = !this.infoVisible; }

    tasks: Task[] = [];
    infoVisible = false;
}
export function debugPanel(): ng.IDirective {
    return {
        template,
        controller: DebugPanelController,
        bindToController: {},
        controllerAs: 'debug'
    };
}