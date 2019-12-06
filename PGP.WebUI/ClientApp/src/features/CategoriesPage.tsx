import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Table } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { getCategories } from '../apiClient';
import { ICategory } from '../models';

const CategoriesPage: React.FC = () => {
    const [categories, setCategories] = useState<ICategory[]>([]);

    useEffect(() => {
        const init = async () => {
            const response = await getCategories();
            response && setCategories(response);
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
                    <Table.TextHeaderCell>Title</Table.TextHeaderCell>
                    <Table.TextHeaderCell>Actions</Table.TextHeaderCell>
                </Table.Head>
                <Table.Body>
                    {categories.map(u => (
                        <Table.Row key={u.id} border>
                            <Table.TextCell>{u.id}</Table.TextCell>
                            <Table.TextCell>{u.title}</Table.TextCell>
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

export default CategoriesPage;
