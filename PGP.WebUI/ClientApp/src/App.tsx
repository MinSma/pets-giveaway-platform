import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { Layout } from './components';
import { routePaths } from './utils';

const App: React.FC = () => {
    return (
        <Router>
            <Layout>
                <Switch>
                    <Route exact {...routePaths.HOME} />
                    <Route exact {...routePaths.LOGIN_PAGE} />
                </Switch>
            </Layout>
        </Router>
    );
};

export default App;
