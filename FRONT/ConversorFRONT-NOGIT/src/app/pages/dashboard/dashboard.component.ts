import { Component, inject } from '@angular/core';
import { RouterOutlet, RouterModule, Router } from '@angular/router';
import { DataAuthService } from '../../services/data-auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterOutlet,RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  subscriptionTypeLabel: string = '';
  router = inject(Router);
  authService = inject(DataAuthService);

  constructor() {
    this.setSubscriptionTypeLabel(); // Inicializa el label de la suscripción
  }

  // Método para cerrar sesión
  logout(){
    this.authService.logout(); // Cierra la sesión y elimina los datos del localStorage
    this.router.navigate(['/login']); // Redirige al login después de cerrar sesión
  }

  // Método para determinar el tipo de suscripción y establecer el texto correspondiente
  private setSubscriptionTypeLabel(): void {
    const subscriptionType = this.authService.usuario?.subscriptionType;

    // Mapeo de números a nombres de suscripción
    const subscriptionTypeMap: { [key: string]: string } = {
      "1": 'Free',
      "2": 'Trial',
      "3": 'Premium'
    };

    // Obtén el nombre basado en el número o muestra "Desconocido"
    this.subscriptionTypeLabel = subscriptionTypeMap[subscriptionType!] || 'Desconocido';
  }
}

