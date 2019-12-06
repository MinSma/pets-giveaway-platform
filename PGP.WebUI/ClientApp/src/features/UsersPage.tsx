import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Avatar, Button, Table } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { getUsers } from '../apiClient';
import * as enums from '../enums';
import { IUser } from '../models';

const UsersPage: React.FC = () => {
    const [users, setUsers] = useState<IUser[]>([]);

    useEffect(() => {
        const init = async () => {
            const response = await getUsers();
            response && setUsers(response);
        };

        init();
    }, []);

    const handleEdit = async () => {};

    const handleDelete = async () => {};

    return (
        <div className="container mt-5 mb-5">
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
                                <Button appearance="primary" intent="none" onClick={handleEdit}>
                                    <FontAwesomeIcon icon={faEdit} /> <span className="ml-1">Edit</span>
                                </Button>
                                <Button appearance="primary" intent="danger" className="ml-1" onClick={handleDelete}>
                                    <FontAwesomeIcon icon={faTrash} />
                                    <span className="ml-1">Delete</span>
                                </Button>
                            </Table.TextCell>
                        </Table.Row>
                    ))}
                </Table.Body>
            </Table>
        </div>
    );
};

export default UsersPage;
