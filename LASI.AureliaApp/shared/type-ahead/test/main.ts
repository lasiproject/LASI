import {Aurelia} from 'aurelia-framework';
import getHostElement from 'src/get-host-element';
export function configure(aurelia: Aurelia): void {
    aurelia.use
        .standardConfiguration()
        .developmentLogging();

    aurelia.start().then(() => aurelia.setRoot('./app/app', getHostElement()));
}