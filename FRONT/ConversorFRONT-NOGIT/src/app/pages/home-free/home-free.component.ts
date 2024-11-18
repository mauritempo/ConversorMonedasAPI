import { Component, inject, OnInit } from '@angular/core';
import { DataConversionService } from '../../services/data-conversion.service';
import { Conversion, ConversionRsp } from '../../Interfaces/conversion';
import { Currency } from '../../Interfaces/currency';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DataAuthService } from '../../services/data-auth.service';
import { UserFree } from '../../Interfaces/user';

@Component({
  selector: 'app-home-free',
  standalone: true,
  imports: [FormsModule, RouterModule,CommonModule],
  templateUrl: './home-free.component.html',
  styleUrl: './home-free.component.scss'
})
export class HomeFreeComponent implements OnInit  {
  remainingConversions: number = 10;
  currencies: Currency[] = [];
  fromCurrencyCode: string = '';
  toCurrencyCode: string = '';
  amount: number = 0;
  conversionResult: ConversionRsp | undefined;
  public userfree: UserFree | undefined;
  dataConversionService = inject(DataConversionService)
  authService = inject(DataAuthService)
  constructor() {}

  ngOnInit(){
    this.initializeConversions();
    this.loadCurrencies();
    
  }
  private initializeConversions(){
    const userId = this.userfree?.userid;
    if (userId) {
      this.dataConversionService.getRemainingConversions(userId)
        .then((remaining) => {
          this.remainingConversions = remaining;
        })
        .catch((err) => {
          console.error('Error al obtener conversiones restantes:', err);
        });
    }
  }

  // Método para cargar las monedas desde el servicio
  async loadCurrencies() {
    await this.dataConversionService.loadData();
    await this.dataConversionService.getRemainingConversions;
    this.currencies = this.dataConversionService.currencies;
  }

  // Método para realizar la conversión de monedas

    async convertCurrency(ConversionForm: NgForm) {
      const {fromCurrencyCode, toCurrencyCode,amount} = ConversionForm.value;
      
      if (this.remainingConversions == 0) {
        alert('Ya no tienes conversiones disponibles.');
        return;
      }
      
      const request: Conversion = {fromCurrencyCode, toCurrencyCode,amount}
      try {
      const result = await this.dataConversionService.convertCurrency(request);
      if (result) {
        this.conversionResult = result;
        this.remainingConversions--; // Reduce el contador localmente
      }
    } catch (error) {
      console.error('Error al realizar la conversión:', error);
    }}
}
