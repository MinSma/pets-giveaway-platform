import { faBirthdayCake, faCity, faHeart, faHome, faSearch, faVenusMars } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { createLike, getAllPets, getToken, removeLike } from '../../apiClient';
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
    isLiked: boolean;
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

    const handleLikeClick = async (e: any, petId: number, isLiked: boolean) => {
        e.stopPropagation();

        let response = null;

        if (!isLiked) {
            response = await createLike(petId);
        } else {
            response = await removeLike(petId);
        }

        const petIndex = pets.findIndex(p => p.id === petId);

        response && setPets(Object.assign([...pets], { [petIndex]: Object.assign({}, pets[petIndex], { isLiked: !isLiked }) }));
    };

    return (
        <div className="container mt-5 mb-5">
            <div className="row">
                {pets.map((p, i) => (
                    <div key={i} className="col-lg-3 col-md-6 col-xs-12 mt-2 cursor-pointer" onClick={() => history.push(routes.PET_PAGE(p.id))}>
                        <div className="card">
                            <div className="card-block text-center">
                                <img src={p.photoCode} alt="Pet image" className="card-img-top card-img p-3" />
                                <div className="card-body">
                                    <div className="card-title">{p.name}</div>
                                    <div className="card-text">
                                        <div>
                                            <FontAwesomeIcon icon={faBirthdayCake} /> Age: {p.age}
                                        </div>
                                        <div>
                                            <FontAwesomeIcon icon={faVenusMars} /> Gender: {enums.Gender.parse(p.gender)}
                                        </div>
                                        <div>
                                            <FontAwesomeIcon icon={faCity} /> City: {p.city}
                                        </div>
                                        <div>
                                            {p.state === enums.State.NotGivenAway && (
                                                <>
                                                    <FontAwesomeIcon icon={faSearch} /> {enums.State.parse(p.state)}
                                                </>
                                            )}
                                            {p.state === enums.State.GivenAway && (
                                                <>
                                                    <FontAwesomeIcon icon={faHome} /> {enums.State.parse(p.state)}
                                                </>
                                            )}
                                        </div>
                                    </div>
                                    {getToken() && (
                                        <div className="mt-2">
                                            {p.isLiked ? (
                                                <Button onClick={(e: any) => handleLikeClick(e, p.id, p.isLiked)}>
                                                    <FontAwesomeIcon icon={faHeart} color="red" className="mr-1" /> Liked
                                                </Button>
                                            ) : (
                                                <Button onClick={(e: any) => handleLikeClick(e, p.id, p.isLiked)}>
                                                    <FontAwesomeIcon icon={faHeart} className="mr-1" /> Like
                                                </Button>
                                            )}
                                        </div>
                                    )}
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
