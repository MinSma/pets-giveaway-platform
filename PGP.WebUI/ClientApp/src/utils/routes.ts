import { generatePath } from 'react-router';
import HomePage from '../features/HomePage/HomePage';
import LoginPage from '../features/LoginPage/LoginPage';

export const routePaths = {
    HOME: {
        path: '/',
        component: HomePage
    },
    LOGIN_PAGE: {
        path: '/login',
        component: LoginPage
    }
};

export const routes = {
    HOME: () => generatePath(routePaths.HOME.path),
    LOGIN_PAGE: () => generatePath(routePaths.LOGIN_PAGE.path)
};
