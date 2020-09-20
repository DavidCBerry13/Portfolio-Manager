export class Client {
    clientId: number;
    firstName: string;
    lastName: string;
    streetAddress: string;
    city: string;
    stateCode: string;
    zipCode: string;
    email: string;
    phone: string;
    dateOfBirth: Date;
    accounts: ClientAccount[];
}


export class ClientAccount {
    accountNumber: string;
    accountStatus: string;
    openDate: string;
    closeDate: string;
    accountBalance: number;
}
