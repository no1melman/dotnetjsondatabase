const webpack = require('webpack');
const Merge = require('webpack-merge');
const CommonConfig = require('./webpack.config.js');

const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;

module.exports = Merge(CommonConfig, {
    plugins: [
        // new BundleAnalyzerPlugin()
    ],

    devServer: {
        contentBase: './dist', // boolean | string | array, static file location
        compress: true, // enable gzip compression
        historyApiFallback: true, // true for index.html upon 404, object for multiple paths
        hot: false, // hot module replacement. Depends on HotModuleReplacementPlugin
        https: false, // true for self-signed, object for cert authority
        noInfo: true, // only errors & warns on hot reload
        proxy: {
            "/api": "http://localhost:49561/"
        }
    }
});