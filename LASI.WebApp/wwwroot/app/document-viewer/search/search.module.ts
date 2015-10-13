'use strict';
import * as angular from 'angular'
import { searchBox } from './search-box.directive';

export default {
    name: 'documentViewer.search',
    requires: ['ui.bootstrap.typeahead'],
    directives: { searchBox }
}