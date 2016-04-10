const Builder = new require('systemjs-builder');

const builder = new Builder('wwwroot/', 'wwwroot/config.js', {});

module.exports = function () {
    return builder.bundle('app', 'app.built.js')
        .then(function () {
            console.info('Build complete');
        })
        .catch(function () {
            console.error("Build error");
        });
};