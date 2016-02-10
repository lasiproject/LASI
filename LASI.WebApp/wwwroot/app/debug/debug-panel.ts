import template from './debug-panel.html';

class DebugPanelController {
    static $inject = ['tasks'];
    constructor(private tasks: Task[]) { }

    get buttonText() { return this.infoVisible ? 'Hide' : 'Show'; }

    toggle() { this.infoVisible = !this.infoVisible; }

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