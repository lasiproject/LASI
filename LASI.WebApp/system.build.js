var path = require('path');
var Builder = require('systemjs-builder');

var builder = new Builder('wwwroot/', 'wwwroot/config.js', {
 
});

builder.bundle('app', 'app.built.js')
	.then(console.log.bind(console, 'Build complete'))
	.catch(function (error) {
		console.log("Build error");
		console.log(error);
	});