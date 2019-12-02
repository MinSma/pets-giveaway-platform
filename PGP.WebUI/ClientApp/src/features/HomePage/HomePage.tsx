import React, { useEffect, useState } from 'react';
import { getAllPets } from '../../apiClient';
import * as enums from '../../enums';

interface IPet {
    id: number;
    name: string;
    age?: number | null;
    gender: number;
    weight?: number | null;
    height?: number | null;
    isSterilized: boolean;
    description: string | null;
    dateAdded: Date;
    state: number;
    photoCode: string;
    categoryId: number;
    userId: number;
}

const HomePage: React.FC = () => {
    const [pets, setPets] = useState<IPet[]>([]);

    useEffect(() => {
        const init = async () => {
            const pets = await getAllPets();
            setPets(pets);
        };

        init();
    }, []);

    return (
        <div className="container">
            <div className="row">
                {pets.map((p, i) => (
                    <div key={i} className="col-lg-3 col-md-6 col-xs-12 mt-2">
                        <div className="card">
                            <div className="card-block text-center">
                                <img src={p.photoCode} alt="Pet image" className="card-img-top card-img p-3" />
                                <div className="card-body">
                                    <div className="card-title">{p.name}</div>
                                    <div className="card-text">
                                        <div>Age: {p.age}</div>
                                        <div>Gender: {enums.Gender.parse(p.gender)}</div>
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
