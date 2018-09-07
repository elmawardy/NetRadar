const path = require('path');

module.exports = {
    devServer: {
        proxy: {
            '/api': {
                target: 'http://localhost:5051',
                changeOrigin: true
            }
        }
    },
    runtimeCompiler: true,
    baseUrl: '/',
    outputDir: path.resolve(__dirname, '../wwwroot'),
    assetsDir:path.resolve(__dirname, '../wwwroot'),
    productionSourceMap: false,
    parallel: undefined,
    css: {
        extract: { filename: '[name].css' }
    },
    chainWebpack: config => {
        if (config.plugins.has('extract-css')) {
            const extractCSSPlugin = config.plugin('extract-css')
            extractCSSPlugin && extractCSSPlugin.tap(() => [{
                filename: 'css/[name].css',
                chunkFilename: 'css/[name].css'
            }])
        }
    },
    configureWebpack: {
        output: {
            filename: 'js/[name].js',
            chunkFilename: 'js/[name].js'
        }
    }

}