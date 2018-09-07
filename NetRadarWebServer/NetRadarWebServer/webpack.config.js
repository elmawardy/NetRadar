let path = require('path');
let webpack = require('webpack');
const { VueLoaderPlugin } = require('vue-loader')

const config = {

    entry: {
        'admin': path.resolve(__dirname, 'wwwroot/src/index.js')
    },
    output: {
        path: path.resolve(__dirname, './wwwroot/dist'),
        filename: '[name].js',
        publicPath: '/dist/apps/'
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        scss: 'vue-style-loader!css-loader!sass-loader', // <style lang="scss">
                        sass: 'vue-style-loader!css-loader!sass-loader?indentedSyntax', // <style lang="sass">
                        //hotReload: true // disables Hot Reload
                    }
                }
            },
            {
                test : /\.(css)$/,
                use: [
                    {
                        loader: 'css-loader',
                        options: {}
                    }
                ]
            },
            {
                test: /\.(png|jpg|gif|svg|ttf|otf|woff2|woff|eot)$/,
                use: [
                    {
                        loader: 'file-loader',
                        options: {}
                    }
                ]
            }
        ]
    },
    //DEBUG VUE
    //devtool: '#eval-source-map',
    //!DEBUG VUE
    plugins: [
        // new BundleAnalyzerPlugin(), // Anaylze dependencies size with GUI
        require('babel-plugin-syntax-dynamic-import'),
        new VueLoaderPlugin()
    ],
}


//DEBUG VUE
//if (process.env.NODE_ENV === 'production') {
//    config.devtool = '#source-map';
//    config.plugins = (config.plugins || []).concat([
//        // Short-circuit all warning code.
//        new webpack.DefinePlugin({
//            'process.env': {
//                NODE_ENV: '"production"'
//            }
//        }),
//        // Minify with dead-code elimination.
//        new webpack.optimize.UglifyJsPlugin({
//            sourceMap: true,
//            compress: {
//                warnings: false
//            },
//            parallel: true
//        })
//    ]);
//}
//!DEBUG VUE

module.exports = config

// if(process.env.NODE_ENV === 'production') {

//     module.exports.optimization = {
//         minimizer: [
//           // we specify a custom UglifyJsPlugin here to get source maps in production
//           new UglifyJsPlugin()
//         ]
//       }


//     module.exports.plugins.push(
//         new webpack.DefinePlugin({
//             'process.env.NODE_ENV': '"production"'
//         })
//     )
// }



// else{ // development environment
//     // module.exports.resolve = { //allowing DOM templates : causes less performance : must delete in production
//     //     alias: {
//     //       'vue$': 'vue/dist/vue.esm.js' // 'vue/dist/vue.common.js' for webpack 1
//     //     }
//     // }
// }
