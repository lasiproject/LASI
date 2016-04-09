const path = require('path');
const Builder = new require('systemjs-builder');

const builder = new Builder('wwwroot/', 'wwwroot/config.js', {

});
module.exports = () => builder.bundle('app', 'app.built.js')
    .then(() => console.log('Build complete'))
    .catch(error => {
        console.log("Build error");
        console.log(error);
    });