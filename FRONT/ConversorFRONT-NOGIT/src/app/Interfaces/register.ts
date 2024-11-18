export interface Register {
    username: string;
    password: string;
    email: string;
    subscriptionId: number; // Tipo de suscripción (por ejemplo, 'Free', 'Trial', 'Pro')
}

export interface ResRegister {
    message: string;
    userid: number;
}