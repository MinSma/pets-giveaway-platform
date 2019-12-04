export interface IComment {
    id: number;
    text: string;
    createdAt: Date;
    createdByUser: IUser;
}

export interface IUser {
    id: number;
    email: string;
    phoneNumber: string;
    firstName: string;
    lastName: string;
    photoCode: string;
}
