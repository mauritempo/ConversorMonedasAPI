export interface Subscription {
    type: number; // 1: Free, 2: Trial, 3: Pro
    maxConversions: number; // Máximo permitido según el tipo de suscripción
  }
  