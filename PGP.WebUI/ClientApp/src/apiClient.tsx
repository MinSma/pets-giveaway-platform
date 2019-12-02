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
