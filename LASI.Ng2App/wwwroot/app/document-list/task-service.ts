import { injectable } from 'ng2-conventions-decorators';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

@injectable export class TaskService {
    constructor(private http: Http) { }

    getTasksForDocumentById(id: string) {
        return Observable.interval(100)
            .flatMap(() => this.http.get(`api/userdocuments/tasks/${id}`))
            .flatMap(tasks => tasks.json() as models.Task[]);
    }
}