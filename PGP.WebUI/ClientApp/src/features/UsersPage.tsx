import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Avatar, Button, Spinner, Table, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { deleteUser, getUsers } from '../apiClient';
import { useDeleteConfirmation } from '../components/DeleteConfirmationService';
import * as enums from '../enums';
import { IUser } from '../models';
import { routes } from '../utils/routes';

const UsersPage: React.FC = () => {
    const [users, setUsers] = useState<IUser[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const history = useHistory();
    const confirm = useDeleteConfirmation();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getUsers();
            response && setUsers(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleDelete = async (userId: number) => {
        confirm().then(async () => {
            const response = await deleteUser(userId);

            if (response) {
                setUsers(users.filter(u => u.id !== userId));
                toaster.success('User was successfully deleted.');
            }
        });
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner className="mx-auto" />
            ) : (
                <Table>
                    <Table.Head>
                        <Table.TextHeaderCell>Id</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Name</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Phone number</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Email address</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Role</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Actions</Table.TextHeaderCell>
                    </Table.Head>
                    <Table.Body>
                        {users.map(u => (
                            <Table.Row key={u.id} border>
                                <Table.TextCell>{u.id}</Table.TextCell>
                                <Table.TextCell display="flex" alignItems="center">
                                    <Avatar name={u.firstName + ' ' + u.lastName} />
                                    <span className="ml-1">{u.firstName + ' ' + u.lastName}</span>
                                </Table.TextCell>
                                <Table.TextCell>{u.phoneNumber}</Table.TextCell>
                                <Table.TextCell>{u.email}</Table.TextCell>
                                <Table.TextCell>{enums.Role.parse(u.roleId)}</Table.TextCell>
                                <Table.TextCell>
                                    <Button appearance="primary" intent="none" onClick={() => history.push(routes.UPDATE_USER_PAGE(u.id))}>
                                        <FontAwesomeIcon icon={faEdit} /> <span className="ml-1">Edit</span>
                                    </Button>
                                    <Button appearance="primary" intent="danger" className="ml-1" onClick={() => handleDelete(u.id)}>
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

export default UsersPage;
