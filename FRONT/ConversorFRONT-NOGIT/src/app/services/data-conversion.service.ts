import { inject, Injectable } from '@angular/core';
import { DataAuthService } from './data-auth.service';
import { Conversion, ConversionRsp } from '../Interfaces/conversion';
import { Currency } from '../Interfaces/currency';
import { UserFree } from '../Interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class DataConversionService {
  currencies: Currency[] = [];
  
  
  authToken = localStorage.getItem("authToken");
  public userfree: UserFree | undefined;
  constructor() { 
    this.loadData();
    
    
  }

  async loadData() {
    await this.getAllCurrencies();
   
  }

  // Método para obtener todas las monedas
  async getAllCurrencies() {
    const res = await fetch('https://localhost:7228/api/Conversion/currencies', {
      method: 'GET',
      headers: {
        authorization: 'Bearer ' + this.authToken,
      }
    });

    if (res.ok) { // Verificamos si el código de respuesta está en el rango 200-299
      const resJson: Currency[] = await res.json();
      this.currencies = resJson;
      return resJson; // Retornamos la lista de monedas obtenida
    } else {
      console.error(`Error al obtener monedas: ${res.status} - ${res.statusText}`);
      return null; // Retornamos null en caso de error
    }
  }

  // Método para realizar la conversión de moneda
  async convertCurrency(request: Conversion){
    const res = await fetch('https://localhost:7228/api/Conversion/convert', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        authorization: 'Bearer ' + this.authToken,
      },
      body: JSON.stringify(request),
    });

    if (res.status !== 200) return;

    const result: ConversionRsp = await res.json();
    return result;
  }

  async getRemainingConversions(userId: number) {
    const res = await fetch(`https://localhost:7228/api/Conversion/${userId}/remaining-conversions`, {
      method: 'GET',
      headers: {
        authorization: 'Bearer ' + localStorage.getItem('authToken'),
      },
    });
  
    if (res.status !== 200) {
      throw new Error('No se pudo obtener las conversiones restantes');
    }
  
    const resJson = await res.json();
    console.log(resJson);
    return resJson.RemainingConversions;
  }

  // Método para recargar los datos
  async refreshCurrencies() {
    await this.loadData();
    
  }
}