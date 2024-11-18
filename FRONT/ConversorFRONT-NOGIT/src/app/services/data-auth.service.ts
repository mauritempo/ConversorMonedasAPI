import { inject, Injectable, resolveForwardRef } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { User } from '../Interfaces/user';
import { Login, ResLogin } from '../Interfaces/login';
import { Register } from '../Interfaces/register';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class DataAuthService {

  router = inject(Router)

  constructor() {
    const token = this.getToken();
    const subscriptionType = localStorage.getItem("subscriptionType");
    const username = localStorage.getItem("username");

    
  
    if (token && subscriptionType && username) {
      const parsedSubscriptionType = parseInt(subscriptionType, 10);
  
      // Validar si el subscriptionType es un número válido
      if (!isNaN(parsedSubscriptionType)) {
        this.usuario = {
          username,
          token,
          subscriptionType: parsedSubscriptionType,
        };
        console.log("Usuario recuperado desde localStorage:", this.usuario);
        
          
        
      } else {
        console.error("subscriptionType no es válido:", subscriptionType);
        this.usuario = undefined;
      }
    } else {
      this.usuario = undefined;
    }
    
  }

public usuario: User | undefined;

async loginn(loginData: Login): Promise<boolean> {
  const res = await fetch('https://localhost:7228/api/User/login', {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json'
      },
      body: JSON.stringify(loginData)
  });

  if (res.status !== 200) return false;
  
  

  const resJson: ResLogin & { sub?: number } = await res.json();

  console.log(resJson)

  if (!resJson.token) return false;

 
  const subscriptionType = resJson.sub ?? 3;
  this.usuario = {
      username: loginData.username,
      token: resJson.token,
      subscriptionType,
  };
    console.log("resJson.sub antes de convertir:", resJson.sub);
    console.log("subscriptionType guardado en localStorage:", localStorage.getItem("subscriptionType"));
    console.log("Usuario configurado:", this.usuario);
      
    localStorage.setItem('authToken', resJson.token);
    localStorage.setItem('username', loginData.username);
    localStorage.setItem('subscriptionType', subscriptionType.toString());
 


  const userDetailsRes = await fetch(`https://localhost:7228/api/User/usuarios/${encodeURIComponent(loginData.username)}`, {
      method: 'GET',
      headers: {
          'Authorization': `Bearer ${resJson.token}`,
          'Content-Type': 'application/json'
      }
  });

  if (userDetailsRes.status !== 200) return true;

  const userDetailsResJson = await userDetailsRes.json();

  console.log(userDetailsResJson);

  const esPremium = false;

  this.usuario.subscriptionType = userDetailsResJson.sub;

  return true;

}


async register(registerData: Register){
  const res = await fetch(`https://localhost:7228/api/User/register`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(registerData)
  });

  if (res.status === 200|| res.status === 201) {
    console.log('Registro exitoso');
    return true;
  } else {
    console.error('Error al registrar:', res.status);
    return false;
  }
}



getToken() {
  return localStorage.getItem("authToken");
}

clearToken() {
  localStorage.removeItem('authToken');
}

// Cerrar sesión
logout() {
  this.clearToken();
  localStorage.removeItem("subscriptionType");
  localStorage.removeItem('username');
  this.usuario = undefined;
  this.router.navigate(['/login']);
}


}
