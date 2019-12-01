import React from 'react';
import { BrowserRouter as Router, Switch } from 'react-router-dom';
import { Layout } from './components';

const App: React.FC = () => {
    return (
        <Router>
            <Layout>
                <Switch></Switch>
            </Layout>
        </Router>
    );
};

export default App;
