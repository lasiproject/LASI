import { component, input } from 'ng2-conventions-decorators';
import template from './sentence.html';

@component(template) export class SentenceComponent {
    @input sentence;
    @input parentId;
}