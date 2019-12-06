import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { getAllPets } from '../../apiClient';
import * as enums from '../../enums';
import { routes } from '../../utils/routes';

interface IPet {
    id: number;
    name: string;
    age?: number | null;
    city: string;
    gender: number;
    state: number;
    photoCode: string;
}

const HomePage: React.FC = () => {
    const [pets, setPets] = useState<IPet[]>([]);
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            const pets = await getAllPets();
            setPets(pets);
        };

        init();
    }, []);

    return (
        <div className="container mt-5 mb-5">
            <div className="row">
                {pets.map((p, i) => (
                    <div key={i} className="col-lg-3 col-md-6 col-xs-12 mt-2" onClick={() => history.push(routes.PET_PAGE(p.id))}>
                        <div className="card">
                            <div className="card-block text-center">
                                <img src={p.photoCode} alt="Pet image" className="card-img-top card-img p-3" />
                                <div className="card-body">
                                    <div className="card-title">{p.name}</div>
                                    <div className="card-text">
                                        <div>Age: {p.age}</div>
                                        <div>Gender: {enums.Gender.parse(p.gender)}</div>
                                        <div>City: {p.city}</div>
                                        <div>{enums.State.parse(p.state)}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default HomePage;
