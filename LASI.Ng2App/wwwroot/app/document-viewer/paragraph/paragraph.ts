import { component, input } from 'ng2-conventions-decorators';
import template from './paragraph.html';

@component(template) export class ParagraphComponent {
    @input paragraph;
    @input parentId;
} 