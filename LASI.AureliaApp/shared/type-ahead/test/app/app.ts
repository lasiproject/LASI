import {bindable, autoinject, observable} from 'aurelia-framework';

@autoinject export class App {
  typeAheadSource = (query: string, syncResults: (...args: {}[]) => {}) => {
    const fragments = query.split(' ');
    return syncResults(this.sourceArray.filter(item => fragments.some(item.text.match.bind(item))));
  };

  @observable sourceArray = ['hello', 'cruel', 'world', 'goodbye', 'cruel', 'world'].map(text => ({ text }));
}