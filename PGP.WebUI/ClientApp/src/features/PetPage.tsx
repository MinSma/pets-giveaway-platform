import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router';
import { getPetById } from '../apiClient';
import * as enums from '../enums';
import * as models from '../models';

interface IPetPageRoute {
    petId: string | undefined;
}

interface IPetProps {
    id: number;
    name: string;
    age: number | null;
    city: string;
    gender: number;
    weight?: number | null;
    height?: number | null;
    isSterilized: boolean | null;
    description: string;
    dateAdded: Date;
    state: number;
    photoCode: string;
    comments: models.IComment[];
}

const PetPage: React.FC = () => {
    const [pet, setPet] = useState<IPetProps>();
    const { petId } = useParams<IPetPageRoute>();

    useEffect(() => {
        const init = async () => {
            const pet = await getPetById(Number(petId));
            setPet(pet);
        };

        init();
    }, [petId]);

    return (
        <>
            {pet ? (
                <div className="container mt-5 mb-5">
                    <div className="row">
                        <div className="col-lg-6 col-md-12 col-xs-12">
                            <img src={pet.photoCode} />
                        </div>
                        <div className="col-lg-6 col-md-12 col-xs-12">
                            {pet.age} {pet.description} {pet.weight} {pet.height} {enums.Gender.parse(pet.gender)} {enums.State.parse(pet.state)}
                        </div>
                    </div>
                    <h3 className="text-center">Comments ({pet.comments.length})</h3>
                    <div className="row">
                        <div className="col-12">
                            {pet.comments.map((c, i) => (
                                <div key={i} className="text-center">
                                    {c.text} {c.createdByUser.email} {c.createdAt}
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            ) : (
                'Loading...'
            )}
        </>
    );
};

export default PetPage;
