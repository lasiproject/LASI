var path = require('path');
var Builder = new require('systemjs-builder');

var builder = new Builder('wwwroot/', 'wwwroot/config.js', {

});
module.exports = function () {
    return builder.bundle('app', 'app.built.js')
           .then(console.log.bind(console, 'Build complete'))
           .catch(function (error) {
               console.log("Build error");
               console.log(error);
           });
};