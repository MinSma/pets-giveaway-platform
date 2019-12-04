interface IEnum {
    id: number;
    text: string;
}

const dataSource: IEnum[] = [
    { id: 1, text: 'Found new home' },
    { id: 2, text: 'Still looking for new home' }
];

const parse = (id: number): string => {
    const state = dataSource.find(x => x.id === id);

    if (state) {
        return state.text;
    }

    throw new Error(`State with specified id = ${id} was not found.`);
};

export const State = {
    parse,
    dataSource() {
        return dataSource;
    }
};