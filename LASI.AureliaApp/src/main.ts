import {Aurelia} from 'aurelia-framework';

export function configure(aurelia: Aurelia) {
    aurelia.use
        .standardConfiguration()
        .developmentLogging()
        .defaultBindingLanguage()
        .defaultResources();

    aurelia.start().then(() => aurelia.setRoot('src/app/app', document.getElementById('app')));
}