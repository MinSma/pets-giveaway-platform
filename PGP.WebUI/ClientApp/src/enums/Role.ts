interface IEnum {
    id: number;
    text: string;
}

const User: number = 1;
const Moderator: number = 2;
const Admin: number = 3;

const dataSource: IEnum[] = [
    { id: 1, text: 'User' },
    { id: 2, text: 'Moderator' },
    { id: 3, text: 'Admin' }
];

const parse = (id: number): string => {
    const role = dataSource.find(x => x.id === id);

    if (role) {
        return role.text;
    }

    throw new Error(`Role with specified id = ${id} was not found.`);
};

export const Role = {
    User,
    Moderator,
    Admin,
    parse,
    dataSource() {
        return dataSource;
    }
};
