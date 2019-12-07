import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Spinner, Table, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { deleteCategory, getCategories } from '../apiClient';
import { IOption } from '../models';

const CategoriesPage: React.FC = () => {
    const [categories, setCategories] = useState<IOption[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getCategories();
            response && setCategories(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleEdit = async () => {};

    const handleDelete = async (categoryId: number) => {
        const response = await deleteCategory(categoryId);

        if (response) {
            setCategories(categories.filter(c => c.id !== categoryId));
            toaster.success('Category was successfully deleted.');
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
                        <Table.TextHeaderCell>Title</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Actions</Table.TextHeaderCell>
                    </Table.Head>
                    <Table.Body>
                        {categories.map(c => (
                            <Table.Row key={c.id} border>
                                <Table.TextCell>{c.id}</Table.TextCell>
                                <Table.TextCell>{c.text}</Table.TextCell>
                                <Table.TextCell>
                                    <Button appearance="primary" intent="none" onClick={handleEdit}>
                                        <FontAwesomeIcon icon={faEdit} /> <span className="ml-1">Edit</span>
                                    </Button>
                                    <Button appearance="primary" intent="danger" className="ml-1" onClick={() => handleDelete(c.id)}>
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

export default CategoriesPage;
