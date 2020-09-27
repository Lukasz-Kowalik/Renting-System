import gulp from 'gulp';
import del from 'del';
import sass from 'gulp-sass';
import clean from 'gulp-clean-css';
import concat from 'gulp-concat';
import uglify from 'gulp-uglify-es';
import babel from 'gulp-babel';

const config = {
    paths: {
        src: {
            js: [
                './wwwroot/js/**/*.js'
            ],
            minjs: [
                "./wwwroot/lib/bootstrap/dist/*.min.js",
                "./wwwroot/lib/DataTables/*.min.js",
                "./wwwroot/lib/jquery*/**/*.min.js"
            ],
            sass: [
                './wwwroot/scss/**/*.scss',
                "./wwwroot/lib/fontawesome-free/scss/*.scss"
            ]
        },
        dist: {
            main: './wwwroot/dist',
            css: './wwwroot/dist/css',
            js: './wwwroot/dist/js',
        }
    }
};
export function js() {
    return gulp.src(config.paths.src.js)
        .pipe(babel({
            compact: false,
            presets: ['@babel/preset-env']
        }))
        .pipe(uglify())
        .pipe(concat("main.js"))
        .pipe(gulp.dest(config.paths.dist.js));
}
export function libs() {
    return gulp.src(config.paths.src.minjs).pipe(concat("libs.js")).pipe(gulp.dest(config.paths.dist.js));
};

export function scss() {
    return gulp.src(config.paths.src.sass)
        .pipe(sass())
        .pipe(concat("styles.css"))
        .pipe(clean())
        .pipe(gulp.dest(config.paths.dist.css));
}

export function Clean() {
    return del(config.paths.dist.main);
}

exports.build = gulp.series(Clean, gulp.parallel(scss, js));

exports.default = exports.build;

export function watch() {
    gulp.watch(config.paths.src.sass, scss);
    gulp.watch(config.paths.src.js + "/main.js", js);
    gulp.watch(config.paths.src.js + "/libs.js", libs);
    return;
}