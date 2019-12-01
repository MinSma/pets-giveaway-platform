import { generatePath } from 'react-router';
import HomePage from '../features/HomePage/HomePage';
import LoginPage from '../features/LoginPage/LoginPage';
import RegisterPage from '../features/RegisterPage/RegisterPage';

export const routePaths = {
    HOME: {
        path: '/',
        component: HomePage
    },
    LOGIN_PAGE: {
        path: '/login',
        component: LoginPage
    },
    REGISTER_PAGE: {
        path: '/register',
        component: RegisterPage
    }
};

export const routes = {
    HOME: () => generatePath(routePaths.HOME.path),
    LOGIN_PAGE: () => generatePath(routePaths.LOGIN_PAGE.path),
    REGISTER_PAGE: () => generatePath(routePaths.REGISTER_PAGE.path)
};
