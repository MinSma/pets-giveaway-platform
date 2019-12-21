import { Spinner, toaster } from 'evergreen-ui';
import React, { useEffect, useState } from 'react';
import { useHistory, useParams } from 'react-router';
import { createLike, deleteLike, getAllPetsByCategoryId, getCategories } from '../apiClient';
import { PetCard, ThereIsNoResultsToShow } from '../components';
import { IOption, IPetList } from '../models';
import { routes } from '../utils';

interface ICategoryPetsPageRoute {
    categoryId: string | undefined;
}

const CategoryPetsPage: React.FC = () => {
    const [pets, setPets] = useState<IPetList[]>([]);
    const [categories, setCategories] = useState<IOption[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const history = useHistory();
    const { categoryId } = useParams<ICategoryPetsPageRoute>();

    useEffect(() => {
        const init = async () => {
            setIsLoading(true);

            const response = await getAllPetsByCategoryId(Number(categoryId));
            setPets(response);

            const categoriesResponse = await getCategories();
            setCategories(categoriesResponse);

            setIsLoading(false);
        };

        init();
    }, [categoryId]);

    const handleLikeClick = async (e: any, petId: number) => {
        e.stopPropagation();

        let response = null;
        const petIndex = pets.findIndex(p => p.id === petId);

        if (!pets[petIndex].isLiked) {
            response = await createLike(petId);
            toaster.success('Successfully liked pet.');
        } else {
            response = await deleteLike(petId);
            toaster.success('Successfully unliked pet.');
        }

        response && setPets(Object.assign([...pets], { [petIndex]: Object.assign({}, pets[petIndex], { isLiked: !pets[petIndex].isLiked }) }));
    };

    return (
        <div className="container mt-5 mb-5">
            {isLoading ? (
                <Spinner className="mx-auto" />
            ) : (
                <>
                    <div className="row">
                        {categories.map(c => (
                            <div key={c.id} className="col-lg-2 col-md-2 col-xs-6 p-3 w-100 mx-auto">
                                <button
                                    className="btn text-center bg-purple-color w-100"
                                    onClick={() => history.push(routes.CATEGORY_PETS_PAGE(c.id))}
                                >
                                    {c.text}
                                </button>
                            </div>
                        ))}
                        <div className="col-lg-2 col-md-2 col-xs-6 p-3 w-100 mx-auto">
                            <button className="btn text-center bg-purple-color w-100" onClick={() => history.push(routes.HOME())}>
                                All
                            </button>
                        </div>
                    </div>
                    {pets.length > 0 ? (
                        <div className="row">
                            {pets.map((p, i) => (
                                <div
                                    key={i}
                                    className="col-lg-3 col-md-6 col-xs-12 mt-2 cursor-pointer"
                                    onClick={() => history.push(routes.PET_PAGE(p.id))}
                                >
                                    <PetCard pet={p} handleLikeClick={handleLikeClick} />
                                </div>
                            ))}
                        </div>
                    ) : (
                        <ThereIsNoResultsToShow />
                    )}
                </>
            )}
        </div>
    );
};

export default CategoryPetsPage;
