import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import WebFont from 'webfontloader';
import App from './App';
import './index.css';

WebFont.load({
    google: {
      families: ['Titillium Web: 400', 'sans-serif']
    }
  });

ReactDOM.render(<App />, document.getElementById('root'));
