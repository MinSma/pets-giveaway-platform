import { Button, Select, Spinner, TextInput, toaster } from 'evergreen-ui';
import { ErrorMessage, Form, Formik } from 'formik';
import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router';
import * as Yup from 'yup';
import { getUserById, updateUser } from '../apiClient';
import * as enums from '../enums';
import { IUser } from '../models';
import { routes } from '../utils/routes';

interface IUpdateUserPageRoute {
    userId: string | undefined;
}

interface IUpdateUserPageFormProps {
    id: number;
    email: string;
    phoneNumber: string;
    firstName: string;
    lastName: string;
    roleId: number;
}

const formValidationSchema = Yup.object<IUpdateUserPageFormProps>().shape({
    email: Yup.string()
        .email('Should be email')
        .required('Required'),
    phoneNumber: Yup.string().required('Required'),
    firstName: Yup.string().required('Required'),
    lastName: Yup.string().required('Required'),
    roleId: Yup.number().required('Required')
});

const UpdateUserPage: React.FC = () => {
    const [user, setUser] = useState<IUser>();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const { userId } = useParams<IUpdateUserPageRoute>();
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            if (userId) {
                const response = await getUserById(Number(userId));
                setUser(response);
            }
            setIsLoading(false);
        };

        init();
    }, [userId]);

    const onSubmit = async (values: IUpdateUserPageFormProps) => {
        const response = await updateUser(values);

        if (response) {
            toaster.success('User was successfully updated.');
            history.push(routes.USERS_PAGE());
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
                        id: user ? user.id : 0,
                        email: user ? user.email : '',
                        phoneNumber: user ? user.phoneNumber : '',
                        firstName: user ? user.firstName : '',
                        lastName: user ? user.lastName : '',
                        roleId: user ? user.roleId : 0
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
                                                value={values.email}
                                                type="email"
                                                className="w-100"
                                                name="email"
                                                placeholder="Email"
                                                isInvalid={submitCount > 0 && errors.email ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="email" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.phoneNumber}
                                                type="text"
                                                className="w-100"
                                                name="phoneNumber"
                                                placeholder="Phone number"
                                                isInvalid={submitCount > 0 && errors.phoneNumber ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="phoneNumber" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.firstName}
                                                type="text"
                                                className="w-100"
                                                name="firstName"
                                                placeholder="First name"
                                                isInvalid={submitCount > 0 && errors.firstName ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="firstName" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.lastName}
                                                type="text"
                                                className="w-100"
                                                name="lastName"
                                                placeholder="Last name"
                                                isInvalid={submitCount > 0 && errors.lastName ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="lastName" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <Select width="100%" value={values.roleId} name="roleId" onChange={handleChange}>
                                                {enums.Role.dataSource().map(r => (
                                                    <option key={r.id} value={r.id}>
                                                        {r.text}
                                                    </option>
                                                ))}
                                            </Select>
                                            <ErrorMessage name="roleId" className="text-danger" component="small" />
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

export default UpdateUserPage;
