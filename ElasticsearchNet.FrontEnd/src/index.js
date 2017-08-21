import React from 'react';
import { Provider } from 'react-redux';
import { render } from 'react-dom';

import Es6Promise from 'es6-promise';
Es6Promise.polyfill();

import configureStore from './redux/configureStore';
const store = configureStore();

import App from './App';

import 'file-loader?name=[name].[ext]!../node_modules/font-awesome/css/font-awesome.css';
import 'file-loader?name=fonts/[name].[ext]!../node_modules/font-awesome/fonts/fontawesome-webfont.eot';
import 'file-loader?name=fonts/[name].[ext]!../node_modules/font-awesome/fonts/fontawesome-webfont.svg';
import 'file-loader?name=fonts/[name].[ext]!../node_modules/font-awesome/fonts/fontawesome-webfont.ttf';
import 'file-loader?name=fonts/[name].[ext]!../node_modules/font-awesome/fonts/fontawesome-webfont.woff';
import 'file-loader?name=fonts/[name].[ext]!../node_modules/font-awesome/fonts/fontawesome-webfont.woff2';
import 'file-loader?name=fonts/[name].[ext]!../node_modules/font-awesome/fonts/FontAwesome.otf';

render(
    <Provider store={store}>
        <App />
    </Provider>,
    document.getElementById('root')
);