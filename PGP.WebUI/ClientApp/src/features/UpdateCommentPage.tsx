import { Button, Spinner, TextInput, toaster } from 'evergreen-ui';
import { ErrorMessage, Form, Formik } from 'formik';
import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router';
import * as Yup from 'yup';
import { getCommentById, updateComment } from '../apiClient';
import { IComment } from '../models';
import { routes } from '../utils/routes';

interface IUpdateCommentPageRoute {
    commentId: string | undefined;
}

interface IUpdateCommentPageFormProps {
    id: number;
    text: string;
    createdAt: Date;
    userId: number;
    petId: number;
}

const formValidationSchema = Yup.object<IUpdateCommentPageFormProps>().shape({
    text: Yup.string().required('Required')
});

const UpdateCommentPage: React.FC = () => {
    const [comment, setComment] = useState<IComment>();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const { commentId } = useParams<IUpdateCommentPageRoute>();
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            if (commentId) {
                const response = await getCommentById(Number(commentId));
                setComment(response);
            }
            setIsLoading(false);
        };

        init();
    }, [commentId]);

    const onSubmit = async (values: IUpdateCommentPageFormProps) => {
        const response = await updateComment(values);

        if (response) {
            toaster.success('Comment was successfully updated.');
            history.push(routes.COMMENTS_PAGE());
        }
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner className="mx-auto" />
            ) : (
                <Formik
                    validationSchema={formValidationSchema}
                    initialValues={{
                        id: comment ? comment.id : 0,
                        text: comment ? comment.text : '',
                        createdAt: comment ? comment.createdAt : new Date(),
                        userId: comment ? comment.userId : 0,
                        petId: comment ? comment.petId : 0
                    }}
                    onSubmit={onSubmit}
                >
                    {props => {
                        const { values, errors, handleChange, submitCount } = props;
                        return (
                            <div className="container main-background-color mt-5 mb-5">
                                <Form className="p-3">
                                    <div className="form-row">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.text}
                                                type="text"
                                                className="w-100"
                                                name="text"
                                                placeholder="Text"
                                                isInvalid={submitCount > 0 && errors.text ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="text" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <Button type="submit" className="mx-auto">
                                            Submit
                                        </Button>
                                    </div>
                                </Form>
                            </div>
                        );
                    }}
                </Formik>
            )}
        </div>
    );
};

export default UpdateCommentPage;
