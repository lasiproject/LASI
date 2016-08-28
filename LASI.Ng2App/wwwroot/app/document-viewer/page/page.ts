import { component, input } from 'ng2-conventions-decorators';
import template from './page.html';

@component(template)
export class DocumentPageComponent {
    constructor() { }

    @input document;
    @input pageData;
}