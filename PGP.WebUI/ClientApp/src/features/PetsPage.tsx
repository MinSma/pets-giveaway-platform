import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Spinner, Table, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { deletePet, getAllUserCreatedPet, getCategories } from '../apiClient';
import { ThereIsNoResultsToShow } from '../components';
import { useDeleteConfirmation } from '../components/DeleteConfirmationService';
import * as enums from '../enums';
import { IOption, IUserCreatedPet } from '../models';
import { routes } from '../utils/routes';

const PetsPage: React.FC = () => {
    const [userCreatedPets, setUserCreatedPets] = useState<IUserCreatedPet[]>([]);
    const [categories, setCategories] = useState<IOption[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const history = useHistory();
    const confirm = useDeleteConfirmation();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);

            const response = await getAllUserCreatedPet();
            response && setUserCreatedPets(response);

            const categoriesResponse = await getCategories();
            setCategories(categoriesResponse);

            setIsLoading(false);
        };

        init();
    }, []);

    const handleDelete = async (petId: number) => {
        confirm().then(async () => {
            const response = await deletePet(petId);

            if (response) {
                setUserCreatedPets(userCreatedPets.filter(p => p.id !== petId));
                toaster.success('Pet was successfully deleted.');
            }
        });
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner className="mx-auto" />
            ) : (
                <>
                    <Button appearance="primary" intent="success" onClick={() => history.push(routes.CREATE_PET_PAGE())}>
                        Create new pet
                    </Button>
                    <Table>
                        <Table.Head>
                            <Table.TextHeaderCell>Id</Table.TextHeaderCell>
                            <Table.TextHeaderCell>Name</Table.TextHeaderCell>
                            <Table.TextHeaderCell>Age</Table.TextHeaderCell>
                            <Table.TextHeaderCell>City</Table.TextHeaderCell>
                            <Table.TextHeaderCell>Gender</Table.TextHeaderCell>
                            <Table.TextHeaderCell>State</Table.TextHeaderCell>
                            <Table.TextHeaderCell>Photo</Table.TextHeaderCell>
                            <Table.TextHeaderCell>Weight</Table.TextHeaderCell>
                            <Table.TextHeaderCell>Height</Table.TextHeaderCell>
                            <Table.TextHeaderCell>Category</Table.TextHeaderCell>
                            <Table.TextHeaderCell flexBasis={200}>Actions</Table.TextHeaderCell>
                        </Table.Head>
                        <Table.Body>
                            {userCreatedPets.length > 0 ? (
                                userCreatedPets.map(ucp => (
                                    <Table.Row key={ucp.id} border>
                                        <Table.TextCell>{ucp.id}</Table.TextCell>
                                        <Table.TextCell>{ucp.name}</Table.TextCell>
                                        <Table.TextCell>{ucp.age}</Table.TextCell>
                                        <Table.TextCell>{ucp.city}</Table.TextCell>
                                        <Table.TextCell>{enums.Gender.parse(ucp.gender)}</Table.TextCell>
                                        <Table.TextCell>{enums.State.parse(ucp.state)}</Table.TextCell>
                                        <Table.TextCell>
                                            <img width="40%" height="40%" src={ucp.photoCode} />
                                        </Table.TextCell>
                                        <Table.TextCell>{ucp.weight}</Table.TextCell>
                                        <Table.TextCell>{ucp.height}</Table.TextCell>
                                        <Table.TextCell>{categories.find(c => c.id === ucp.categoryId)!.text}</Table.TextCell>
                                        <Table.TextCell flexBasis={200}>
                                            <Button appearance="primary" intent="none" onClick={() => history.push(routes.UPDATE_PET_PAGE(ucp.id))}>
                                                <FontAwesomeIcon icon={faEdit} /> <span className="ml-1">Edit</span>
                                            </Button>
                                            <Button appearance="primary" intent="danger" className="ml-1" onClick={() => handleDelete(ucp.id)}>
                                                <FontAwesomeIcon icon={faTrash} />
                                                <span className="ml-1">Delete</span>
                                            </Button>
                                        </Table.TextCell>
                                    </Table.Row>
                                ))
                            ) : (
                                <ThereIsNoResultsToShow />
                            )}
                        </Table.Body>
                    </Table>
                </>
            )}
        </div>
    );
};

export default PetsPage;
