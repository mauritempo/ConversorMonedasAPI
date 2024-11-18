import { Component, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { DataAuthService } from '../../services/data-auth.service';
import Swal from 'sweetalert2';
import { Register } from '../../Interfaces/register';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  errorRegister: boolean = false;
  successRegister: boolean = false;
  router = inject(Router)
  authService = inject(DataAuthService)

  constructor() {}

  async onRegister(registerForm: NgForm) {
    if (!registerForm.valid) {
      this.errorRegister = true;
      this.successRegister = false;
      return;
    }

    const { username, password, email, subscriptionId } = registerForm.value;
    
    const registerData: Register = { username, password, email, subscriptionId };
    const success = await this.authService.register(registerData);

    if (success) {
      console.log('Redirigiendo al login...');
      this.successRegister = true;
      this.errorRegister = false;
      this.router.navigate(['/login']).then(() => {
        Swal.fire("Registro exitoso", "", "success");
      });
      registerForm.reset();
    } else {
      console.log('Error en el registro.');
      this.errorRegister = true;
      this.successRegister = false;
    }

 }}
