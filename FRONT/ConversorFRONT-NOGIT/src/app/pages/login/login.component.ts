import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { DataAuthService } from '../../services/data-auth.service';
import { Login } from '../../Interfaces/login';
import { DataSubscriptionServiceService } from '../../services/data-subscription-service.service';
import Swal from 'sweetalert2';
import { disableDebugTools } from '@angular/platform-browser';
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  
  subscriptionService = inject(DataSubscriptionServiceService)
  router = inject(Router);
  authService = inject(DataAuthService);


  errorLogin = false;   
  showPassword: boolean = false; 

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
  async login(loginFrom: NgForm){
    const{username,password} = loginFrom.value;
    const loginData = {username, password}

    const res = await this.authService.loginn(loginData)
    console.log(res)
    if (res && this.authService.usuario?.subscriptionType !== undefined) {
      const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
          toast.onmouseenter = Swal.stopTimer;
          toast.onmouseleave = Swal.resumeTimer;
        }
      });
      Toast.fire({
        icon: "success",
        title: "Signed in successfully"
      });
      this.subscriptionService.redirectBasedOnSubscription(this.authService.usuario.subscriptionType)
   }
    else {
      Swal.fire("Usuario/contraseña incorrectos.", "", "info");
    }

  }
}

