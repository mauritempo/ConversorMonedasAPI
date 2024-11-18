import { DataAuthService } from '../services/data-auth.service';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { inject, Injectable } from '@angular/core';
import { Token } from '@angular/compiler';



export const soloLogueadoGuard: CanActivateFn = (route, state) => {
  const dataAuthService = inject(DataAuthService);
  const router = inject(Router);
  console.log(dataAuthService)
  // Verificar si el usuario está logueado
  if (dataAuthService.usuario?.token) {
    return true; // Permitir acceso
  }
  
  // Redirigir al login si no está autenticado
  router.navigate(["/login"]);
  return false;
};



