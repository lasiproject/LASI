import { pipe } from 'ng2-conventions-decorators';

@pipe export class ToLocaleUpperPipe {
    transform = (value: string) => value.toLocaleUpperCase();
}