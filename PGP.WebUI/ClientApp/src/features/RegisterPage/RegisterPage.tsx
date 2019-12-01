import { Button, FilePicker, TextInput } from 'evergreen-ui';
import { ErrorMessage, Form, Formik } from 'formik';
import React from 'react';
import * as Yup from 'yup';

interface IRegisterFormProps {
    email: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    photoCode: string;
}

const formValidationSchema = Yup.object<IRegisterFormProps>().shape({
    email: Yup.string()
        .email('Should be email')
        .required('Required'),
    password: Yup.string().required('Required'),
    confirmPassword: Yup.string().required('Required'),
    firstName: Yup.string().required('Required'),
    lastName: Yup.string().required('Required'),
    phoneNumber: Yup.string().required('Required'),
    photoCode: Yup.string()
});

const initialValues: IRegisterFormProps = {
    email: '',
    password: '',
    confirmPassword: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    photoCode: ''
};

const RegisterPage: React.FC = () => {
    const onSubmit = async (values: IRegisterFormProps) => {
        console.warn(values);
    };

    const toBase64 = (file: File) =>
        new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
        });

    return (
        <Formik validationSchema={formValidationSchema} initialValues={initialValues} onSubmit={onSubmit}>
            {props => {
                const { errors, handleChange, submitCount, setFieldValue } = props;
                return (
                    <div className="container main-background-color">
                        <Form>
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
                            <div className="form-row">
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
                            <div className="form-row">
                                <div className="col-6 mx-auto">
                                    <TextInput
                                        type="password"
                                        className="w-100"
                                        name="confirmPassword"
                                        placeholder="Confirm Password"
                                        isInvalid={submitCount > 0 && errors.confirmPassword ? true : false}
                                        onChange={handleChange}
                                    />
                                    <ErrorMessage name="confirmPassword" className="text-danger" component="small" />
                                </div>
                            </div>
                            <div className="form-row">
                                <div className="col-6 mx-auto">
                                    <TextInput
                                        type="text"
                                        className="w-100"
                                        name="firstName"
                                        placeholder="First Name"
                                        isInvalid={submitCount > 0 && errors.firstName ? true : false}
                                        onChange={handleChange}
                                    />
                                    <ErrorMessage name="firstName" className="text-danger" component="small" />
                                </div>
                            </div>
                            <div className="form-row">
                                <div className="col-6 mx-auto">
                                    <TextInput
                                        type="text"
                                        className="w-100"
                                        name="lastName"
                                        placeholder="Last Name"
                                        isInvalid={submitCount > 0 && errors.lastName ? true : false}
                                        onChange={handleChange}
                                    />
                                    <ErrorMessage name="lastName" className="text-danger" component="small" />
                                </div>
                            </div>
                            <div className="form-row">
                                <div className="col-6 mx-auto">
                                    <TextInput
                                        type="phone"
                                        className="w-100"
                                        name="phoneNumber"
                                        placeholder="Phone Number"
                                        isInvalid={submitCount > 0 && errors.phoneNumber ? true : false}
                                        onChange={handleChange}
                                    />
                                    <ErrorMessage name="phoneNumber" className="text-danger" component="small" />
                                </div>
                            </div>
                            <div className="form-row">
                                <div className="col-6 mx-auto">
                                    <FilePicker
                                        multiple={false}
                                        onChange={async (files: File[]) => {
                                            const fileInBase64 = await toBase64(files[0]);
                                            setFieldValue('photoCode', fileInBase64);
                                        }}
                                        placeholder="Select the photo here!"
                                    />
                                    <ErrorMessage name="photoCode" className="text-danger" component="small" />
                                </div>
                            </div>
                            <div className="form-row">
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

export default RegisterPage;
