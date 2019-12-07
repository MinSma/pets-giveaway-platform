import { Button, Checkbox, FilePicker, Select, Spinner, TextInput, toaster } from 'evergreen-ui';
import { ErrorMessage, Form, Formik } from 'formik';
import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router';
import * as Yup from 'yup';
import { createPet, getCategories, getPetById, updatePet } from '../apiClient';
import * as enums from '../enums';
import { IOption, IUserCreatedPet } from '../models';
import { fileToBase64, routes } from '../utils';

interface ICreateUpdatePetPageRoute {
    petId: string | undefined;
}

export interface ICreateUpdatePetFormProps {
    id: number;
    name: string;
    age?: number | null;
    city: string;
    gender: number;
    state: number;
    photoCode: string;
    weight?: number;
    height?: number;
    isSterilized: boolean;
    description: string;
    categoryId: number;
}

const formValidationSchema = Yup.object<ICreateUpdatePetFormProps>().shape({
    name: Yup.string().required('Required'),
    age: Yup.number(),
    city: Yup.string().required('Required'),
    gender: Yup.number().required('Required'),
    state: Yup.number().required('Required'),
    photoCode: Yup.string().required('Required'),
    weight: Yup.number(),
    height: Yup.number(),
    isSterilized: Yup.boolean(),
    description: Yup.string(),
    categoryId: Yup.number().required('Required')
});

const CreateUpdatePetPage: React.FC = () => {
    const [pet, setPet] = useState<IUserCreatedPet>();
    const [categories, setCategories] = useState<IOption[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const { petId } = useParams<ICreateUpdatePetPageRoute>();
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);

            if (petId) {
                const response = await getPetById(Number(petId));
                setPet(response);
            }

            const response = await getCategories();
            setCategories(response);

            setIsLoading(false);
        };

        init();
    }, [petId]);

    const onSubmit = async (values: ICreateUpdatePetFormProps) => {
        if (petId) {
            const response = await updatePet(values);

            if (response) {
                toaster.success('Pet was successfully updated.');
                history.push(routes.PETS_PAGE());
            }
        } else {
            const response = await createPet(values);

            if (response) {
                toaster.success('Pet was successfully created.');
                history.push(routes.PETS_PAGE());
            }
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
                        id: pet ? pet.id : 0,
                        name: pet ? pet.name : '',
                        city: pet ? pet.city : '',
                        age: pet ? pet.age : undefined,
                        gender: pet ? pet.gender : 0,
                        weight: pet ? pet.weight : undefined,
                        height: pet ? pet.height : undefined,
                        isSterilized: pet ? pet.isSterilized : false,
                        description: pet ? pet.description : '',
                        state: pet ? pet.state : 0,
                        photoCode: pet ? pet.photoCode : '',
                        categoryId: pet ? pet.categoryId : 0
                    }}
                    onSubmit={onSubmit}
                >
                    {props => {
                        const { values, errors, handleChange, submitCount, setFieldValue } = props;
                        return (
                            <div className="container main-background-color mt-5 mb-5">
                                <Form className="p-3">
                                    <div className="form-row">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.name}
                                                type="text"
                                                className="w-100"
                                                name="name"
                                                placeholder="Name"
                                                isInvalid={submitCount > 0 && errors.name ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="name" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.city}
                                                type="text"
                                                className="w-100"
                                                name="city"
                                                placeholder="City"
                                                isInvalid={submitCount > 0 && errors.city ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="city" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.age}
                                                type="number"
                                                className="w-100"
                                                name="age"
                                                placeholder="Age"
                                                isInvalid={submitCount > 0 && errors.age ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="age" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <Select width="100%" value={values.gender} name="gender" onChange={handleChange}>
                                                {enums.Gender.dataSource().map(r => (
                                                    <option key={r.id} value={r.id}>
                                                        {r.text}
                                                    </option>
                                                ))}
                                            </Select>
                                            <ErrorMessage name="gender" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.weight}
                                                type="number"
                                                className="w-100"
                                                name="weight"
                                                placeholder="Weight"
                                                isInvalid={submitCount > 0 && errors.weight ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="weight" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.height}
                                                type="number"
                                                className="w-100"
                                                name="height"
                                                placeholder="Height"
                                                isInvalid={submitCount > 0 && errors.height ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="height" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <Checkbox label="Sterilized" name="isSterilized" checked={values.isSterilized} onChange={handleChange} />
                                            <ErrorMessage name="height" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <TextInput
                                                value={values.description}
                                                type="text"
                                                className="w-100"
                                                name="description"
                                                placeholder="Description"
                                                isInvalid={submitCount > 0 && errors.description ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="description" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <Select width="100%" value={values.state} name="state" onChange={handleChange}>
                                                {enums.State.dataSource().map(r => (
                                                    <option key={r.id} value={r.id}>
                                                        {r.text}
                                                    </option>
                                                ))}
                                            </Select>
                                            <ErrorMessage name="state" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <FilePicker
                                                value={values.photoCode}
                                                multiple={false}
                                                onChange={async (files: File[]) => {
                                                    const fileInBase64 = await fileToBase64(files[0]);
                                                    setFieldValue('photoCode', fileInBase64);
                                                }}
                                                placeholder="Select the photo here!"
                                            />
                                            <ErrorMessage name="photoCode" className="text-danger" component="small" />
                                        </div>
                                    </div>
                                    <div className="form-row mt-2">
                                        <div className="col-6 mx-auto">
                                            <Select width="100%" value={values.categoryId} name="categoryId" onChange={handleChange}>
                                                {categories.map(c => (
                                                    <option key={c.id} value={c.id}>
                                                        {c.text}
                                                    </option>
                                                ))}
                                            </Select>
                                            <ErrorMessage name="category" className="text-danger" component="small" />
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

export default CreateUpdatePetPage;
