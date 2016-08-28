import { Injectable } from 'app/ng2-utils';
import { Observable } from 'rxjs/Rx';
import { Http } from '@angular/http';
import { enableActiveHighlighting } from './result-chart-builder';
import buildMenus from './build-menus';

@Injectable
export class ResultService {
    tasks: models.Task[] = [];

    constructor(private http: Http) { }

    processDocument(id: string, name: string) {
        this.tasks[id] = {
            id,
            name,
            percentComplete: 0,
        };


        return this.http.get('/analysis/' + id)
            .map(response => response.json() as models.DocumentModel)
            .map(data => {
                const markupHeader = $(`
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h4 class="panel-title">
                      <a href="#${id}" data-toggle="collapse" data-parent="#accordion">${name}</a> 
                    </h4>
                  </div> 
                </div>`);

                const panelMarkup = $(`<div id="${id}" class="panel-collapse collapse in">${JSON.stringify(data)}</div>`);

                if (!$(`#${id}`).length) {
                    $('#accordion').append(markupHeader).append(panelMarkup);
                } else {
                    $(`#${id}`).remove();
                    $('#accordion').append(panelMarkup);
                }
                buildMenus();
                enableActiveHighlighting();
                this.tasks[id].percentComplete = 100;
                return data;
            });
    }
    
    getTasksForDocument(documentId) {
        return Observable.from(this.tasks.filter(task => task.id === documentId));
    }
}
