import { faEdit, faTrash } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Avatar, Button, Table } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { getComments } from '../apiClient';
import { IComment } from '../models';
import { dateToFormattedString } from '../utils';

const CommentsPage: React.FC = () => {
    const [comments, setComments] = useState<IComment[]>([]);

    useEffect(() => {
        const init = async () => {
            const response = await getComments();
            response && setComments(response);
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

export default CommentsPage;
