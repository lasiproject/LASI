import '../styles/site.less!css';
import '../styles/lexical.less!css';
import '../styles/vertical-tab-support-for-angular-ui-bootstrap.css!';
import 'systemjs-hot-reloader/default-listener.js';
export function __reload(m) {
    if (m.component.state)
        component.setState(m.component.state, () => jsonDoc);
}
import React from 'react';
import ReactDOM from 'react-dom';
import jsonDoc from '../doc.json!';
import App from './app';
let container = document.getElementById('container');
export let component = ReactDOM.render(React.createElement(App, jsonDoc), container);
