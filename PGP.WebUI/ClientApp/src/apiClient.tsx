import { toaster } from 'evergreen-ui';
import { ICategory } from './models';

interface IUserLoginResponse {
    jwtToken: string;
}

interface IUserRegistration {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    photoCode: string;
}

const url = 'https://localhost:5001';

export const getToken = () => {
    return localStorage.getItem('jwtToken');
};

export const getTokenDecoded = () => {
    const jwt = require('jsonwebtoken');
    return jwt.decode(localStorage.getItem('jwtToken'));
};

export const setToken = (newToken: string) => {
    localStorage.setItem('jwtToken', newToken);
};

export const deleteToken = () => {
    localStorage.removeItem('jwtToken');
};

export const getAllPets = async () => {
    return await fetch(`${url}/api/pets`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during pets pull from server.');
        }
    });
};

export const getPetById = async (petId: number) => {
    return await fetch(`${url}/api/pets/${petId}`, {
        method: 'GET'
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during pet pull from server.');
        }
    });
};

export const userLogin = async (email: string, password: string) => {
    return await fetch(`${url}/api/users/login`, {
        method: 'POST',
        body: JSON.stringify({ email, password }),
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then((data: IUserLoginResponse) => {
                setToken(data.jwtToken);
                return true;
            });
        } else {
            toaster.danger('A failure occured during login.');
        }
    });
};

export const userRegister = async (values: IUserRegistration) => {
    return await fetch(`${url}/api/users/register`, {
        method: 'POST',
        body: JSON.stringify({ ...values }),
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during registration.');
        }
    });
};

export const createLike = async (petId: number) => {
    return await fetch(`${url}/api/users/${getTokenDecoded().nameid}/pets/${petId}/likes`, {
        method: 'POST',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during pet like.');
        }
    });
};

export const deleteLike = async (petId: number) => {
    return await fetch(`${url}/api/users/${getTokenDecoded().nameid}/pets/${petId}/likes`, {
        method: 'DELETE',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during pet unlike.');
        }
    });
};

export const getLikedPets = async () => {
    return await fetch(`${url}/api/users/${getTokenDecoded().nameid}/likes`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during liked pets pull from server.');
        }
    });
};

export const getUsers = async () => {
    return await fetch(`${url}/api/users`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during users pull from server.');
        }
    });
};

export const deleteUser = async (userId: number) => {
    console.warn(userId);

    return await fetch(`${url}/api/users/${userId}`, {
        method: 'DELETE',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during user delete.');
        }
    });
};

export const getCategories = async () => {
    return await fetch(`${url}/api/categories`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during categories pull from server.');
        }
    });
};

export const getCategoryById = async (categoryId: number) => {
    return await fetch(`${url}/api/categories/${categoryId}`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during category pull from server.');
        }
    });
};

export const createCategory = async (values: ICategory) => {
    return await fetch(`${url}/api/categories`, {
        method: 'POST',
        body: JSON.stringify(values),
        headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during category create.');
        }
    });
};

export const updateCategory = async (values: ICategory) => {
    return await fetch(`${url}/api/categories/${values.id}`, {
        method: 'PUT',
        body: JSON.stringify(values),
        headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during category update.');
        }
    });
};

export const deleteCategory = async (categoryId: number) => {
    return await fetch(`${url}/api/categories/${categoryId}`, {
        method: 'DELETE',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during category delete.');
        }
    });
};

export const getComments = async () => {
    return await fetch(`${url}/api/comments`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during comments pull from server.');
        }
    });
};

export const deleteComment = async (commentId: number) => {
    return await fetch(`${url}/api/comments/${commentId}`, {
        method: 'DELETE',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during comment delete.');
        }
    });
};

export const getAllUserCreatedPet = async () => {
    return await fetch(`${url}/api/users/pets`, {
        method: 'GET',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            toaster.danger('A failure occured during pets pull from server.');
        }
    });
};

export const deletePet = async (petId: number) => {
    return await fetch(`${url}/api/pets/${petId}`, {
        method: 'DELETE',
        headers: {
            Authorization: `Bearer ${getToken()}`
        }
    }).then(response => {
        if (response.ok) {
            return true;
        } else {
            toaster.danger('A failure occured during pet delete.');
        }
    });
};
