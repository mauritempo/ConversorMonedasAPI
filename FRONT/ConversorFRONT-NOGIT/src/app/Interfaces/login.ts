export interface Login {
    username: string;
    password: string;

}

export interface ResLogin {
    status: string;
    message: string;
    token?: string;
    sub: number;
}
