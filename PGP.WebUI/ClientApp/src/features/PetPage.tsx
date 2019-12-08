import { faBirthdayCake, faHome, faSearch, faSignature, faTextHeight, faTrash, faVenusMars, faWeight } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Avatar, Button, Spinner, TextInput, toaster } from 'evergreen-ui';
import { ErrorMessage, Form, Formik } from 'formik';
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router';
import * as Yup from 'yup';
import { createComment, deleteComment, getPetById, getToken, getTokenDecoded } from '../apiClient';
import { useDeleteConfirmation } from '../components/DeleteConfirmationService';
import * as enums from '../enums';
import { dateToFormattedString } from '../utils';

interface IPetPageRoute {
    petId: string | undefined;
}

interface ICommentProps {
    id: number;
    text: string;
    createdAt: Date;
    createdByUser: {
        id: number;
        email: string;
        phoneNumber: string;
        firstName: string;
        lastName: string;
    };
}

interface IPetProps {
    id: number;
    name: string;
    age: number | null;
    city: string;
    gender: number;
    weight?: number | null;
    height?: number | null;
    isSterilized: boolean | null;
    description: string;
    dateAdded: Date;
    state: number;
    photoCode: string;
    comments: ICommentProps[];
}

interface ICreateCommentFormProps {
    text: string;
}

const formValidationSchema = Yup.object<ICreateCommentFormProps>().shape({
    text: Yup.string().required('Required')
});

const PetPage: React.FC = () => {
    const [pet, setPet] = useState<IPetProps>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const { petId } = useParams<IPetPageRoute>();
    const confirm = useDeleteConfirmation();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            const pet = await getPetById(Number(petId));
            setPet(pet);
            setIsLoading(false);
        };

        init();
    }, [petId]);

    const onSubmit = async (values: ICreateCommentFormProps) => {
        const response = await createComment(values.text, Number(petId));

        if (response && pet) {
            setPet({
                ...pet,
                comments: [...pet.comments, response]
            });
            toaster.success('Comment was successfully created.');
        }
    };

    const handleDelete = async (commentId: number) => {
        confirm().then(async () => {
            const response = await deleteComment(commentId);

            if (response && pet) {
                setPet({
                    ...pet,
                    comments: pet.comments.filter(c => c.id !== commentId)
                });
                toaster.success('Comment was successfully deleted.');
            }
        });
    };

    return (
        <div className="container mt-5 mb-5">
            {!isLoading && pet ? (
                <>
                    <div className="row">
                        <div className="col-lg-6 col-md-12 col-xs-12 text-center">
                            <img id="pet_page_photo" src={pet.photoCode} />
                        </div>
                        <div className="col-lg-6 col-md-12 col-xs-12 text-center">
                            <h1>{pet.name}</h1>
                            <div>
                                <FontAwesomeIcon icon={faBirthdayCake} /> Age: {pet.age}
                            </div>
                            <div>
                                <FontAwesomeIcon icon={faSignature} /> Description: {pet.description}
                            </div>
                            <div>
                                <FontAwesomeIcon icon={faWeight} /> Weight: {pet.weight}
                            </div>
                            <div>
                                <FontAwesomeIcon icon={faTextHeight} /> Height: {pet.height}
                            </div>
                            <div>
                                <FontAwesomeIcon icon={faVenusMars} /> Gender: {enums.Gender.parse(pet.gender)}
                            </div>
                            <div>
                                {pet.state === enums.State.NotGivenAway && (
                                    <>
                                        <FontAwesomeIcon icon={faSearch} /> {enums.State.parse(pet.state)}
                                    </>
                                )}
                                {pet.state === enums.State.GivenAway && (
                                    <>
                                        <FontAwesomeIcon icon={faHome} /> {enums.State.parse(pet.state)}
                                    </>
                                )}
                            </div>
                        </div>
                    </div>
                    <h3 className="text-center mt-2">Comments ({pet.comments.length})</h3>
                    <div>
                        {pet.comments.map((c, i) => (
                            <div key={i} className="row border mt-1 p-2 text-center">
                                <div className="col-lg-2 col-md-4 col-xs-12 align-vertical-center">
                                    {getTokenDecoded().role === 'Admin' && (
                                        <FontAwesomeIcon icon={faTrash} color="red" className="mr-2" onClick={() => handleDelete(c.id)} />
                                    )}
                                    <Avatar name={`${c.createdByUser.firstName} ${c.createdByUser.lastName}`} />
                                    <span className="ml-2">
                                        {c.createdByUser.firstName} {c.createdByUser.lastName}
                                    </span>
                                </div>
                                <div className="col-lg-2 col-md-4 col-xs-12 align-vertical-center">
                                    <span className="ml-2">{c.createdByUser.email}</span>
                                </div>
                                <div className="col-lg-2 col-md-4 col-xs-12 align-vertical-center">{dateToFormattedString(c.createdAt)}</div>
                                <div className="col-lg-6 col-md-12 col-xs-12 align-vertical-center">{c.text}</div>
                            </div>
                        ))}
                    </div>
                    {getToken() && (
                        <>
                            <h3 className="text-center mt-2">Create new comment</h3>
                            <Formik validationSchema={formValidationSchema} initialValues={{ text: '' }} onSubmit={onSubmit}>
                                {props => {
                                    const { values, errors, handleChange, submitCount } = props;
                                    return (
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
                                    );
                                }}
                            </Formik>
                        </>
                    )}
                </>
            ) : (
                <Spinner className="mx-auto" />
            )}
        </div>
    );
};

export default PetPage;
