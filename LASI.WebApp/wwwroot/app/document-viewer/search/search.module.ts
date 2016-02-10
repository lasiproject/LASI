import * as angularBootstrap from 'angular-bootstrap';
import { searchBox } from './search-box';

export default {
    name: 'documentViewer.search',
    requires: ['ui.bootstrap.typeahead'],
    directives: { searchBox }
} as NgModuleConfig;