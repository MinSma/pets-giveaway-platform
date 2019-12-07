import { Spinner, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { createLike, deleteLike, getAllPets } from '../apiClient';
import { PetCard } from '../components';
import { IPetList } from '../models';
import { routes } from '../utils/routes';

const HomePage: React.FC = () => {
    const [pets, setPets] = useState<IPetList[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getAllPets();
            setPets(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleLikeClick = async (e: any, petId: number) => {
        e.stopPropagation();

        let response = null;
        const petIndex = pets.findIndex(p => p.id === petId);

        if (!pets[petIndex].isLiked) {
            response = await createLike(petId);
            toaster.success('Successfully liked pet.');
        } else {
            response = await deleteLike(petId);
            toaster.success('Successfully unliked pet.');
        }

        response && setPets(Object.assign([...pets], { [petIndex]: Object.assign({}, pets[petIndex], { isLiked: !pets[petIndex].isLiked }) }));
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner />
            ) : (
                <div className="row">
                    {pets.map((p, i) => (
                        <div key={i} className="col-lg-3 col-md-6 col-xs-12 mt-2 cursor-pointer" onClick={() => history.push(routes.PET_PAGE(p.id))}>
                            <PetCard pet={p} handleLikeClick={handleLikeClick} />
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default HomePage;
