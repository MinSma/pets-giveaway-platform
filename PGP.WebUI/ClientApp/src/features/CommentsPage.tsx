import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Avatar, Button, Spinner, Table, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router';
import { deleteComment, getComments } from '../apiClient';
import { ThereIsNoResultsToShow } from '../components';
import { useDeleteConfirmation } from '../components/DeleteConfirmationService';
import { ICommentList } from '../models';
import { dateToFormattedString } from '../utils';
import { routes } from '../utils/routes';

const CommentsPage: React.FC = () => {
    const [comments, setComments] = useState<ICommentList[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const history = useHistory();
    const confirm = useDeleteConfirmation();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getComments();
            response && setComments(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleDelete = async (commentId: number) => {
        confirm().then(async () => {
            const response = await deleteComment(commentId);

            if (response) {
                setComments(comments.filter(c => c.id !== commentId));
                toaster.success('Comment was successfully deleted.');
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
                        <Table.TextHeaderCell>Text</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Date</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Author</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Actions</Table.TextHeaderCell>
                    </Table.Head>
                    <Table.Body>
                        {comments.length > 0 ? (
                            comments.map(c => (
                                <Table.Row key={c.id} border>
                                    <Table.TextCell>{c.id}</Table.TextCell>
                                    <Table.TextCell>{c.text}</Table.TextCell>
                                    <Table.TextCell>{dateToFormattedString(c.createdAt)}</Table.TextCell>
                                    <Table.TextCell>
                                        <Avatar name={c.userFullName} /> <span className="ml-1">{c.userEmail}</span>
                                    </Table.TextCell>
                                    <Table.TextCell>
                                        <Button appearance="primary" intent="none" onClick={() => history.push(routes.UPDATE_COMMENT_PAGE(c.id))}>
                                            <FontAwesomeIcon icon={faEdit} /> <span className="ml-1">Edit</span>
                                        </Button>
                                        <Button appearance="primary" intent="danger" className="ml-1" onClick={() => handleDelete(c.id)}>
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
            )}
        </div>
    );
};

export default CommentsPage;
