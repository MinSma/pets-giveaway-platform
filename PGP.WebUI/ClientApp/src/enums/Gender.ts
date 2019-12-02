interface IEnum {
    id: number;
    text: string;
}

const dataSource: IEnum[] = [
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
    parse,
    dataSource() {
        return dataSource;
    }
};
