import { faBirthdayCake, faCity, faHeart, faHome, faSearch, faVenusMars } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button } from 'evergreen-ui';
import React from 'react';
import { getToken } from '../apiClient';
import * as enums from '../enums';
import { IPetList } from '../models';

interface IPetCardProps {
    pet: IPetList;
    handleLikeClick: (e: any, petId: number) => void;
}

const PetCard: React.FC<IPetCardProps> = ({ pet, handleLikeClick }) => {
    return (
        <div className="card">
            <div className="card-block text-center">
                <img src={pet.photoCode} alt="Pet image" className="card-img-top card-img p-3" />
                <div className="card-body">
                    <div className="card-title">{pet.name}</div>
                    <div className="card-text">
                        <div>
                            <FontAwesomeIcon icon={faBirthdayCake} /> Age: {pet.age}
                        </div>
                        <div>
                            <FontAwesomeIcon icon={faVenusMars} /> Gender: {enums.Gender.parse(pet.gender)}
                        </div>
                        <div>
                            <FontAwesomeIcon icon={faCity} /> City: {pet.city}
                        </div>
                        <div>
                            {pet.state === enums.State.NotGivenAway && (
                                <>
                                    <FontAwesomeIcon icon={faSearch} /> {enums.State.parse(pet.state)}
                                </>
                            )}
                            {pet.state === enums.State.GivenAway && (
                                <>
                                    <FontAwesomeIcon icon={faHome} /> {enums.State.parse(pet.state)}
                                </>
                            )}
                        </div>
                    </div>
                    {getToken() && (
                        <div className="mt-2">
                            {pet.isLiked ? (
                                <Button onClick={(e: any) => handleLikeClick(e, pet.id)}>
                                    <FontAwesomeIcon icon={faHeart} color="red" className="mr-1" /> Liked
                                </Button>
                            ) : (
                                <Button onClick={(e: any) => handleLikeClick(e, pet.id)}>
                                    <FontAwesomeIcon icon={faHeart} className="mr-1" /> Like
                                </Button>
                            )}
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default PetCard;
