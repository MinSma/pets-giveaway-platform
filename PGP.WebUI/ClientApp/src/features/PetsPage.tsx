import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Spinner, Table, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { deletePet, getAllUserCreatedPet } from '../apiClient';
import * as enums from '../enums';
import { IOption, IPetList } from '../models';

interface IUserCreatedPet extends IPetList {
    weight?: number;
    height?: number;
    isStrelized: boolean;
    description: string;
    dateAdded: Date;
    category: IOption;
}

const PetsPage: React.FC = () => {
    const [userCreatedPets, setUserCreatedPets] = useState<IUserCreatedPet[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getAllUserCreatedPet();
            response && setUserCreatedPets(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleEdit = async () => {};

    const handleDelete = async (petId: number) => {
        const response = await deletePet(petId);

        if (response) {
            setUserCreatedPets(userCreatedPets.filter(p => p.id !== petId));
            toaster.success('Pet was successfully deleted.');
        }
    };
    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner />
            ) : (
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
                        {userCreatedPets.map(ucp => (
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
                                <Table.TextCell>{ucp.category.text}</Table.TextCell>
                                <Table.TextCell flexBasis={200}>
                                    <Button appearance="primary" intent="none" onClick={handleEdit}>
                                        <FontAwesomeIcon icon={faEdit} /> <span className="ml-1">Edit</span>
                                    </Button>
                                    <Button appearance="primary" intent="danger" className="ml-1" onClick={() => handleDelete(ucp.id)}>
                                        <FontAwesomeIcon icon={faTrash} />
                                        <span className="ml-1">Delete</span>
                                    </Button>
                                </Table.TextCell>
                            </Table.Row>
                        ))}
                    </Table.Body>
                </Table>
            )}
        </div>
    );
};

export default PetsPage;
