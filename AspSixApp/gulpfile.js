module.exports = function (gulp) {
    var gulp = require('gulp'),
        bower = require('gulp-bower'),
        jslint = require('gulp');
    gulp.task('bower', function () {
        return bower();
    });

    //gulp.task('bower', function () {
    //    return bower()
    //});
};