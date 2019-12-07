import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Button, Spinner, Table, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { deleteCategory, getCategories } from '../apiClient';
import { useDeleteConfirmation } from '../components/DeleteConfirmationService';
import { IOption } from '../models';
import { routes } from '../utils/routes';

const CategoriesPage: React.FC = () => {
    const [categories, setCategories] = useState<IOption[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const history = useHistory();
    const confirm = useDeleteConfirmation();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getCategories();
            response && setCategories(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleDelete = async (categoryId: number) => {
        confirm().then(async () => {
            const response = await deleteCategory(categoryId);

            if (response) {
                setCategories(categories.filter(c => c.id !== categoryId));
                toaster.success('Category was successfully deleted.');
            }
        });
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner />
            ) : (
                <>
                    <Button appearance="primary" intent="success" onClick={() => history.push(routes.CREATE_CATEGORY_PAGE())}>
                        Create new category
                    </Button>
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
                                        <Button appearance="primary" intent="none" onClick={() => history.push(routes.UPDATE_CATEGORY_PAGE(c.id))}>
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
                </>
            )}
        </div>
    );
};

export default CategoriesPage;
