import { generatePath } from 'react-router';
import {
    CategoriesPage,
    CommentsPage,
    CreateUpdateCategoryPage,
    HomePage,
    LikedPetsPage,
    LoginPage,
    PetPage,
    PetsPage,
    RegisterPage,
    UpdateCommentPage,
    UsersPage
} from '../features';

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
    },
    PET_PAGE: {
        path: '/pets/:petId',
        component: PetPage
    },
    PETS_PAGE: {
        path: '/pets',
        component: PetsPage
    },
    LIKED_PETS_PAGE: {
        path: '/likes',
        component: LikedPetsPage
    },
    UPDATE_COMMENT_PAGE: {
        path: '/comments/:commentId/update',
        component: UpdateCommentPage
    },
    COMMENTS_PAGE: {
        path: '/comments',
        component: CommentsPage
    },
    CREATE_CATEGORY_PAGE: {
        path: '/categories/create',
        component: CreateUpdateCategoryPage
    },
    UPDATE_CATEGORY_PAGE: {
        path: '/categories/:categoryId/edit',
        component: CreateUpdateCategoryPage
    },
    CATEGORIES_PAGE: {
        path: '/categories',
        component: CategoriesPage
    },
    USERS_PAGE: {
        path: '/users',
        component: UsersPage
    }
};

export const routes = {
    HOME: () => generatePath(routePaths.HOME.path),
    LOGIN_PAGE: () => generatePath(routePaths.LOGIN_PAGE.path),
    REGISTER_PAGE: () => generatePath(routePaths.REGISTER_PAGE.path),
    PET_PAGE: (petId: number) => generatePath(routePaths.PET_PAGE.path, { petId }),
    PETS_PAGE: () => generatePath(routePaths.PETS_PAGE.path),
    LIKED_PETS_PAGE: () => generatePath(routePaths.LIKED_PETS_PAGE.path),
    UPDATE_COMMENT_PAGE: (commentId: number) => generatePath(routePaths.UPDATE_COMMENT_PAGE.path, { commentId }),
    COMMENTS_PAGE: () => generatePath(routePaths.COMMENTS_PAGE.path),
    CREATE_CATEGORY_PAGE: () => generatePath(routePaths.CREATE_CATEGORY_PAGE.path),
    UPDATE_CATEGORY_PAGE: (categoryId: number) => generatePath(routePaths.UPDATE_CATEGORY_PAGE.path, { categoryId }),
    CATEGORIES_PAGE: () => generatePath(routePaths.CATEGORIES_PAGE.path),
    USERS_PAGE: () => generatePath(routePaths.USERS_PAGE.path)
};
