const webpack = require('webpack');
const packages = require('./package.json');
const path = require('path');

const ExtractTextPlugin = require('extract-text-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

const filterDependencies = ['normalize.css', 'font-awesome'];
const dependencies = Object.keys(packages.dependencies).filter(f => !filterDependencies.some(fd => fd === f));

module.exports = {
    entry: {
        main: './src/index.js',
        vendor: dependencies
    },

    devtool: 'source-map', // enum

    target: 'web', // enum

    output: {
        filename: '[name].[chunkhash].js',
        path: path.resolve(__dirname, 'dist')
    },

    plugins: [
        new webpack.optimize.CommonsChunkPlugin({
            name: "vendor",
            minChunks: 2
        }),
        new webpack.optimize.CommonsChunkPlugin({
            name: 'runtime',
            minChunks: Infinity
        }),
        new ExtractTextPlugin('styles.css'),
        new HtmlWebpackPlugin({
            template: 'index.html'
        })
    ],

    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                use: [
                    'babel-loader'
                ]
            },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: 'style-loader',
                    use: [
                        { loader: 'css-loader', options: { import: false } }
                    ]
                }),
                exclude: /node_modules/
            },
            {
                test: /(\.png|\.jpg|\.otf)$/,
                use: ['file-loader?name=[name].[ext]&publicPath=assets/&outputPath=assets/']
            }
        ]
    },

    performance: {
        hints: 'warning', // enum
        maxAssetSize: 200000, // int (in bytes),
        maxEntrypointSize: 400000, // int (in bytes)
        assetFilter: assetFilename => {
            // Function predicate that provides asset filenames
            return assetFilename.endsWith('.css') || assetFilename.endsWith('.js');
        }
    },

    stats: 'errors-only'
};