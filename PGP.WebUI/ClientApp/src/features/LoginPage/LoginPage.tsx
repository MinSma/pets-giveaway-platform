import { Button, TextInput } from 'evergreen-ui';
import { ErrorMessage, Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

interface ILoginFormProps {
    email: string;
    password: string;
}

const formValidationSchema = Yup.object<ILoginFormProps>().shape({
    email: Yup.string()
        .email('Should be email')
        .required('Required'),
    password: Yup.string().required('Required')
});

const initialValues: ILoginFormProps = {
    email: '',
    password: ''
};

const LoginPage: React.FC = () => {
    const onSubmit = async (values: ILoginFormProps) => {
        console.warn(values);
    };

    return (
        <Formik validationSchema={formValidationSchema} initialValues={initialValues} onSubmit={onSubmit}>
            {props => {
                const { errors, handleChange, submitCount } = props;
                return (
                    <div className="container main-background-color mt-5 mb-5">
                        <Form className="p-3">
                            <div className="form-row">
                                <div className="col-6 mx-auto">
                                    <TextInput
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
                                        type="password"
                                        className="w-100"
                                        name="password"
                                        placeholder="Password"
                                        isInvalid={submitCount > 0 && errors.password ? true : false}
                                        onChange={handleChange}
                                    />
                                    <ErrorMessage name="password" className="text-danger" component="small" />
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
    );
};

export default LoginPage;
