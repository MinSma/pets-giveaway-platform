import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Avatar, Button, Spinner, Table, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { deleteComment, getComments } from '../apiClient';
import { IComment } from '../models';
import { dateToFormattedString } from '../utils';

const CommentsPage: React.FC = () => {
    const [comments, setComments] = useState<IComment[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const response = await getComments();
            response && setComments(response);
            setIsLoading(false);
        };

        init();
    }, []);

    const handleEdit = async () => {};

    const handleDelete = async (commentId: number) => {
        const response = await deleteComment(commentId);

        if (response) {
            setComments(comments.filter(c => c.id !== commentId));
            toaster.success('Comment was successfully deleted.');
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
                        <Table.TextHeaderCell>Text</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Date</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Author</Table.TextHeaderCell>
                        <Table.TextHeaderCell>Actions</Table.TextHeaderCell>
                    </Table.Head>
                    <Table.Body>
                        {comments.map(c => (
                            <Table.Row key={c.id} border>
                                <Table.TextCell>{c.id}</Table.TextCell>
                                <Table.TextCell>{c.text}</Table.TextCell>
                                <Table.TextCell>{dateToFormattedString(c.createdAt)}</Table.TextCell>
                                <Table.TextCell>
                                    <Avatar name={c.userFullName} /> <span className="ml-1">{c.userEmail}</span>
                                </Table.TextCell>
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

export default CommentsPage;
