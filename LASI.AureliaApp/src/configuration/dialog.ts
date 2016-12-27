import {DialogConfiguration} from 'aurelia-dialog';
export default (config:DialogConfiguration) => {
  config.useDefaults().settings;
  config.settings.lock = false;
  config.settings.centerHorizontalOnly = false;
  config.settings.startingZIndex = 1005;
  return config;
};