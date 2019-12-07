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
                    <Route exact {...routePaths.REGISTER_PAGE} />
                    <Route exact {...routePaths.PET_PAGE} />
                    <Route exact {...routePaths.PETS_PAGE} />
                    <Route exact {...routePaths.LIKED_PETS_PAGE} />
                    <Route exact {...routePaths.COMMENTS_PAGE} />
                    <Route exact {...routePaths.CREATE_CATEGORY_PAGE} />
                    <Route exact {...routePaths.UPDATE_CATEGORY_PAGE} />
                    <Route exact {...routePaths.CATEGORIES_PAGE} />
                    <Route exact {...routePaths.USERS_PAGE} />
                </Switch>
            </Layout>
        </Router>
    );
};

export default App;
