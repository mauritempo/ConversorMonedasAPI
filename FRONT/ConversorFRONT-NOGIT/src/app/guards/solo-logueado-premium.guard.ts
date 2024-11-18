import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { DataAuthService } from '../services/data-auth.service';


export const soloLogueadoPREMIUMGuard: CanActivateFn = (route, state) => {
  const dataAuthService = inject(DataAuthService);
  const router = inject(Router);

  const subscriptionType = dataAuthService.usuario?.subscriptionType;
  console.log('Tipo de suscripción en guard:', subscriptionType);

  if (subscriptionType === 2 || subscriptionType === 3) {
    console.log('Acceso permitido para Premium');
    return true; // Acceso permitido
  }

  console.error('Acceso denegado, redirigiendo a home-free');
  const url = router.parseUrl('/home-free');
  return new RedirectCommand(url); // Redirige a home-free
};
