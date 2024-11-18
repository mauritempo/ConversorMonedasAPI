import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { routes } from '../app.routes';

@Injectable({
  providedIn: 'root'
})
export class DataSubscriptionServiceService {
  router = inject(Router)
  constructor() {}

  redirectBasedOnSubscription(subscriptionType: number | undefined) {
    console.log('Redirigiendo según la suscripción:', subscriptionType);

    if (subscriptionType === 1) {
      this.router.navigate(['/home-free']); // Free
    } else if (subscriptionType === 2 || subscriptionType === 3) {
      this.router.navigate(['/home']); // Trial o Pro
    } else {
      console.error(
        `Error: Tipo de suscripción desconocido (${subscriptionType}), redirigiendo a login`
      );
      this.router.navigate(['/login']); // Redirige a login en caso de error
    }
  }
}