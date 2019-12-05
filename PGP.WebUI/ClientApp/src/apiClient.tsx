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

export const getAllPets = async () => {
    return await fetch(`${url}/api/pets`, {
        method: 'GET'
    }).then(response => {
        if (response.ok) {
            return response.json().then(data => {
                return data;
            });
        } else {
            console.warn('getAllPets failed');
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
            console.warn('getPetById failed');
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
                localStorage.setItem('jwtToken', data.jwtToken);
                return true;
            });
        } else {
            console.warn('userLogin failed');
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
            console.warn('userRegister failed');
        }
    });
};
