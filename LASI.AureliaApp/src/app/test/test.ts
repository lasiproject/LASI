import 'jspm_packages/github/twbs/bootstrap@3.3.6/css/bootstrap.css';
import { bootstrap as ng2Bootstrap } from 'angular2/platform/browser';
import { DocumentViewerComponent } from 'app/document-viewer/components/document-viewer';
import { LexicalMenuBuilder } from 'app/document-viewer/lexical-menu-builder';
import { DocumentModelService } from 'app/document-viewer/document-model-service';
import { HTTP_PROVIDERS, Http, RequestOptions } from 'angular2/http';

export function bootstrap() {
    return ng2Bootstrap(DocumentViewerComponent, [HTTP_PROVIDERS, DocumentModelService, LexicalMenuBuilder])
        .then(success => console.log(`Bootstrap success`))
        .catch(error => console.log(error));
}