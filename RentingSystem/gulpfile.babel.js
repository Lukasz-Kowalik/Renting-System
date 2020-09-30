const { series, src, dest } = require('gulp');
const sass = require('gulp-sass');
const clean = require('gulp-clean-css');
const concat = require('gulp-concat');

const destDir = './wwwroot';
const sassFiles = [
    './node_modules/font-awesome/scss/font-awesome.scss',
    './src/scss/**/*.scss'
];

function copyFonts() {
    return src("./node_modules/font-awesome/fonts/*")
        .pipe(dest(destDir + "/fonts"));
}

function scss() {
    return src(sassFiles)
        .pipe(sass())
        .pipe(src("./node_modules/datatables/media/css/*.min.css"))
        .pipe(concat("styles.css"))
        .pipe(clean())
        .pipe(dest(destDir));
}
exports.default = series(scss, copyFonts)