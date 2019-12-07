import { Spinner, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { deleteLike, getLikedPets } from '../apiClient';
import { PetCard, ThereIsNoResultsToShow } from '../components';
import { IPetList } from '../models';
import { routes } from '../utils';

const LikedPetsPage: React.FC = () => {
    const [likedPets, setLikedPets] = useState<IPetList[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getLikedPets();
            setLikedPets(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleLikeClick = async (e: any, petId: number) => {
        e.stopPropagation();

        const response = await deleteLike(petId);

        if (response) {
            setLikedPets(likedPets.filter(lp => lp.id !== petId));
            toaster.success('Successfully unliked pet.');
        }
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner className="mx-auto" />
            ) : (
                <div className="row">
                    {likedPets.length > 0 ? (
                        likedPets.map((p, i) => (
                            <div
                                key={i}
                                className="col-lg-3 col-md-6 col-xs-12 mt-2 cursor-pointer"
                                onClick={() => history.push(routes.PET_PAGE(p.id))}
                            >
                                <PetCard pet={p} handleLikeClick={handleLikeClick} />
                            </div>
                        ))
                    ) : (
                        <ThereIsNoResultsToShow />
                    )}
                </div>
            )}
        </div>
    );
};

export default LikedPetsPage;
