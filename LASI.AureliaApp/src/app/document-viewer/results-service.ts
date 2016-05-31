import {autoinject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import enableActiveHighlighting from './result-chart-builder';
import buildMenus from './build-menus';
import {DocumentModel, Task} from 'src/models';

@autoinject export default class ResultService {

    constructor(private http: HttpClient) { }

    async processDocument({id, name}) {
        this.tasks[id] = { id, name, percentComplete: 0 };

        const response = await this.http.fetch('/analysis/' + id);
        const json = await response.json() as DocumentModel;

        const markupHeader = $(`
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h4 class="panel-title">
                      <a href="#${id}" data-toggle="collapse" data-parent="#accordion">${name}</a> 
                    </h4>
                  </div> 
                </div>`);
        const panelMarkup = $(`<div id="${id}" class="panel-collapse collapse in">${json}</div>`);

        if (!$(`#${id}`).length) {
            $('#accordion').append(markupHeader).append(panelMarkup);
        } else {
            $(`#${id}`).remove();
            $('#accordion').append(panelMarkup);
        }
        buildMenus();
        enableActiveHighlighting();
        this.tasks[id].percentComplete = 100;
        return json;
    }

    getTasksForDocument(modelOrId: string | DocumentModel) {
        return this.tasks[typeof modelOrId === 'string' ? modelOrId : modelOrId.id];
    }

    tasks: { [documentId: string]: Task } = {};
}
