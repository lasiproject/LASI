import { Injectable } from 'angular2/core';
import { Http } from 'angular2/http';
import 'rxjs/Rx';

@Injectable()
export class TaskService {
    constructor(private http: Http) { }

    getTasksForDocumentById(id: string) {
        return this.http.get(`api/userdocuments/tasks/${id}`)
            .bufferTime(1000, 1000)
            .sampleTime(10)
            .flatMap(tasks => tasks.map(task => task.json()));
    }
}