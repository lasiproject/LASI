import 'jspm_packages/github/twbs/bootstrap@3.3.6/css/bootstrap.css';
import { bootstrap as ng2Bootstrap } from '@angular/platform-browser-dynamic';
import { DocumentViewerComponent } from 'app/document-viewer/document-viewer';
import { LexicalMenuBuilder } from 'app/document-viewer/lexical-menu-builder';
import { DocumentModelService } from 'app/document-viewer/document-model-service';
import { HTTP_PROVIDERS, Http, RequestOptions } from '@angular/http';

export function bootstrap() {
    return ng2Bootstrap(DocumentViewerComponent, [HTTP_PROVIDERS, DocumentModelService, LexicalMenuBuilder])
        .then(success => console.log(`Bootstrap success`))
        .catch(error => console.log(error));
}      