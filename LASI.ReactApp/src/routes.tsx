import React from 'react';
import {hashHistory, Router, Route, IndexRoute} from 'react-router';

import App from './app';
import Home from './home';
import NoMatch from './no-match';

declare var SystemJS;

const router = (
  <Router history={hashHistory}>
    <Route path="/" component={App}>
      <IndexRoute component={Home}/>

      
      <Route path="*" component={NoMatch}/>
    </Route>
  </Router>
);

export default router;