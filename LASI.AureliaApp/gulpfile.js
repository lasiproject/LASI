'use strict';
const gulp = require('gulp'),
    tslint = require('tslint'),
    gulpTslintStlish = require('gulp-tslint-stylish'),
    gulpTslint = require('gulp-tslint'),
    liteServer = require('lite-server');

var onError = console.error.bind(console);

const appts = ['./src/**/*.ts', './shared/**/*.ts'];

gulp.task('tslint', function () {
    return gulp.src(appts)
        .pipe(gulpTslint())
        .pipe(gulpTslint.report(gulpTslintStlish, {
            emitError: false,
            sort: true,
            fullPath: false
        }))
});

gulp.task('watch', function () {
    gulp.watch(appts, ['tslint'])
        .on('error', onError);
});

gulp.task('default', ['watch']);