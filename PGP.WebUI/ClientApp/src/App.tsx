import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { getTokenDecoded } from './apiClient';
import { Layout } from './components';
import { DeleteConfirmationServiceProvider } from './components/DeleteConfirmationService';
import { routePaths } from './utils';

const App: React.FC = () => {
    const decodedToken = getTokenDecoded();

    return (
        <Router>
            <Layout>
                <DeleteConfirmationServiceProvider>
                    <Switch>
                        <Route exact {...routePaths.HOME} />
                        <Route exact {...routePaths.LOGIN_PAGE} />
                        <Route exact {...routePaths.REGISTER_PAGE} />

                        {decodedToken ? (
                            <Switch>
                                <Route exact {...routePaths.LIKED_PETS_PAGE} />

                                {(decodedToken.role === 'Moderator' || decodedToken.role === 'Admin') && (
                                    <Switch>
                                        <Route exact {...routePaths.CREATE_PET_PAGE} />
                                        <Route exact {...routePaths.UPDATE_PET_PAGE} />
                                        <Route exact {...routePaths.PET_PAGE} />
                                        <Route exact {...routePaths.PETS_PAGE} />

                                        {decodedToken.role === 'Admin' && (
                                            <Switch>
                                                <Route exact {...routePaths.UPDATE_COMMENT_PAGE} />
                                                <Route exact {...routePaths.COMMENTS_PAGE} />
                                                <Route exact {...routePaths.CREATE_CATEGORY_PAGE} />
                                                <Route exact {...routePaths.UPDATE_CATEGORY_PAGE} />
                                                <Route exact {...routePaths.CATEGORIES_PAGE} />
                                                <Route exact {...routePaths.UPDATE_USER_PAGE} />
                                                <Route exact {...routePaths.USER_PAGE} />
                                                <Route exact {...routePaths.USERS_PAGE} />
                                            </Switch>
                                        )}
                                    </Switch>
                                )}
                            </Switch>
                        ) : (
                            <Switch>
                                <Route exact {...routePaths.PET_PAGE} />
                                <Route exact {...routePaths.USER_PAGE} />
                            </Switch>
                        )}

                        <Route exact {...routePaths.NOT_FOUND_PAGE} />
                    </Switch>
                </DeleteConfirmationServiceProvider>
            </Layout>
        </Router>
    );
};

export default App;
