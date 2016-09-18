const SystemJS = new require('jspm').Loader();

SystemJS.import('test').catch(console.error);