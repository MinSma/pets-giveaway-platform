import { Button, Spinner, TextInput, toaster } from 'evergreen-ui';
import { ErrorMessage, Form, Formik } from 'formik';
import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router';
import * as Yup from 'yup';
import { createCategory, getCategoryById, updateCategory } from '../apiClient';
import { ICategory } from '../models';
import { routes } from '../utils/routes';

interface ICreateUpdateCategoryPageRoute {
    categoryId: string | undefined;
}

interface ICreateUpdateCategoryFormProps {
    id: number;
    title: string;
}

const formValidationSchema = Yup.object<ICreateUpdateCategoryFormProps>().shape({
    title: Yup.string().required('Required')
});

const CreateUpdateCategoryPage: React.FC = () => {
    const [category, setCategory] = useState<ICategory>();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const { categoryId } = useParams<ICreateUpdateCategoryPageRoute>();
    const history = useHistory();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);
            if (categoryId) {
                const response = await getCategoryById(Number(categoryId));
                setCategory(response);
            }
            setIsLoading(false);
        };

        init();
    }, [categoryId]);

    const onSubmit = async (values: ICreateUpdateCategoryFormProps) => {
        if (categoryId) {
            const response = await updateCategory(values);

            if (response) {
                toaster.success('Category was successfully updated.');
                history.push(routes.CATEGORIES_PAGE());
            }
        } else {
            const response = await createCategory(values);

            if (response) {
                toaster.success('Category was successfully created.');
                history.push(routes.CATEGORIES_PAGE());
            }
        }
    };

    return (
        <>
            {isLoading ? (
                <Spinner />
            ) : (
                <Formik
                    validationSchema={formValidationSchema}
                    initialValues={{ id: category ? category.id : 0, title: category ? category.title : '' }}
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
                                                value={values.title}
                                                type="text"
                                                className="w-100"
                                                name="title"
                                                placeholder="Title"
                                                isInvalid={submitCount > 0 && errors.title ? true : false}
                                                onChange={handleChange}
                                            />
                                            <ErrorMessage name="title" className="text-danger" component="small" />
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
        </>
    );
};

export default CreateUpdateCategoryPage;
