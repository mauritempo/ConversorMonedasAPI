import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { DataAuthService } from '../services/data-auth.service';

export const soloPublicoGuard: CanActivateFn = (route, state) => {
  const dataAuthService = inject(DataAuthService);
  const router = inject(Router);

  const token = dataAuthService.usuario?.token;
  const subscriptionType = dataAuthService.usuario?.subscriptionType;
  console.log(token, subscriptionType)

  if (!token) return true; // Permite acceso si no está autenticado

  // Redirige basado en el tipo de suscripción
  const url = subscriptionType === 1 ? router.parseUrl('/home-free') : router.parseUrl('/home');
  console.log(url);
  return new RedirectCommand(url);
};
