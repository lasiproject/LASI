import {Aurelia} from 'aurelia-framework';
import { getHostElement } from 'src/helpers';
export function configure(aurelia: Aurelia): void {
    aurelia.use
        .standardConfiguration()
        .developmentLogging();

    aurelia.start().then(() => aurelia.setRoot('./app/app', getHostElement()));
}