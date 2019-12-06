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

export interface IPetList {
    id: number;
    name: string;
    age?: number | null;
    city: string;
    gender: number;
    state: number;
    photoCode: string;
    isLiked: boolean;
}
