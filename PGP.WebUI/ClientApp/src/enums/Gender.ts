import { IOption } from '../models';

const Male: number = 1;
const Female: number = 2;

const dataSource: IOption[] = [
    { id: 1, text: 'Male' },
    { id: 2, text: 'Female' }
];

const parse = (id: number): string => {
    const gender = dataSource.find(x => x.id === id);

    if (gender) {
        return gender.text;
    }

    throw new Error(`Gender with specified id = ${id} was not found.`);
};

export const Gender = {
    Male,
    Female,
    parse,
    dataSource() {
        return dataSource;
    }
};
