import { Spinner, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router';
import { createLike, deleteLike, getAllUserCreatedPet, getUserById } from '../apiClient';
import { PetCard, ThereIsNoResultsToShow } from '../components';
import { IUser, IUserCreatedPet } from '../models';
import { routes } from '../utils';

interface IUserPageRoute {
    userId: string | undefined;
}

const UserPage: React.FC = () => {
    const [user, setUser] = useState<IUser>();
    const [userCreatedPets, setUserCreatedPets] = useState<IUserCreatedPet[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const { userId } = useParams<IUserPageRoute>();
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);

            const userResponse = await getUserById(Number(userId));
            setUser(userResponse);

            const response = await getAllUserCreatedPet(Number(userId));
            response && setUserCreatedPets(response);

            setIsLoading(false);
        };

        init();
    }, []);

    const handleLikeClick = async (e: any, petId: number) => {
        e.stopPropagation();

        let response = null;
        const petIndex = userCreatedPets.findIndex(p => p.id === petId);

        if (!userCreatedPets[petIndex].isLiked) {
            response = await createLike(petId);
            toaster.success('Successfully liked pet.');
        } else {
            response = await deleteLike(petId);
            toaster.success('Successfully unliked pet.');
        }

        response &&
            setUserCreatedPets(
                Object.assign([...userCreatedPets], {
                    [petIndex]: Object.assign({}, userCreatedPets[petIndex], { isLiked: !userCreatedPets[petIndex].isLiked })
                })
            );
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner className="mx-auto" />
            ) : (
                <>
                    {user && (
                        <h1 className="col-12 text-center">
                            {user.firstName} {user.lastName} profile
                        </h1>
                    )}
                    {userCreatedPets.length > 0 ? (
                        <div className="row">
                            {userCreatedPets.map((p, i) => (
                                <div
                                    key={i}
                                    className="col-lg-3 col-md-6 col-xs-12 mt-2 cursor-pointer"
                                    onClick={() => history.push(routes.PET_PAGE(p.id))}
                                >
                                    <PetCard pet={p} handleLikeClick={handleLikeClick} />
                                </div>
                            ))}
                        </div>
                    ) : (
                        <ThereIsNoResultsToShow />
                    )}
                </>
            )}
        </div>
    );
};

export default UserPage;
