import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { getLikedPets, removeLike } from '../../apiClient';
import { PetCard } from '../../components';
import { IPetList } from '../../models';
import { routes } from '../../utils';

const LikedPetsPage: React.FC = () => {
    const [likedPets, setLikedPets] = useState<IPetList[]>([]);
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            const response = await getLikedPets();
            setLikedPets(response);
        };

        init();
    }, []);

    const handleLikeClick = async (e: any, petId: number) => {
        e.stopPropagation();

        const response = await removeLike(petId);
        response && setLikedPets(likedPets.filter(lp => lp.id !== petId));
    };

    return (
        <div className="container mt-5 mb-5">
            <div className="row">
                {likedPets.map((p, i) => (
                    <div key={i} className="col-lg-3 col-md-6 col-xs-12 mt-2 cursor-pointer" onClick={() => history.push(routes.PET_PAGE(p.id))}>
                        <PetCard pet={p} handleLikeClick={handleLikeClick} />
                    </div>
                ))}
            </div>
        </div>
    );
};

export default LikedPetsPage;
