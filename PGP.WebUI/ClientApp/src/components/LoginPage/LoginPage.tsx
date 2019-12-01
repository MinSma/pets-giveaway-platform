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
                    <Form>
                        <div className="form-row">
                            <TextInput
                                name="email"
                                placeholder="Email"
                                isInvalid={submitCount > 0 && errors.email ? true : false}
                                onChange={handleChange}
                            />
                            <ErrorMessage name="email" className="text-danger" component="small" />
                        </div>
                        <div className="form-row">
                            <TextInput
                                name="password"
                                placeholder="Password"
                                isInvalid={submitCount > 0 && errors.password ? true : false}
                                onChange={handleChange}
                            />
                            <ErrorMessage name="password" className="text-danger" component="small" />
                        </div>
                        <div className="form-row">
                            <Button type="submit">Submit</Button>
                        </div>
                    </Form>
                );
            }}
        </Formik>
    );
};

export default LoginPage;
