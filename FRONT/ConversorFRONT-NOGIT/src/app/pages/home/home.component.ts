import { Component, inject, OnInit } from '@angular/core';
import { Conversion, ConversionRsp } from '../../Interfaces/conversion';
import { Currency } from '../../Interfaces/currency';
import { DataConversionService } from '../../services/data-conversion.service';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup } from '@angular/forms'; 
import { FormsModule, NgForm } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { User, UserFree } from '../../Interfaces/user';
import { DataAuthService } from '../../services/data-auth.service';
import Swal from 'sweetalert2';



@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, RouterModule,CommonModule ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit  {
  remainingConversions: number = 100;
  currencies: Currency[] = [];
  public userfree: UserFree | undefined;
  conversionResult: ConversionRsp | undefined;
  dataAuthService = inject(DataAuthService)
  currencyForm: any;
  dataConversionService = inject(DataConversionService)
  
  constructor() {
    console.log('HomeComponent cargado');
    this.dataAuthService.usuario;
  }
  ngOnInit() {
    this.initializeConversions();
    this.loadCurrencies();
  }

  // Método para cargar las monedas desde el servicio
  async loadCurrencies() {
    await this.dataConversionService.loadData();
    this.currencies = this.dataConversionService.currencies;
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

  // Método para realizar la conversión de monedas
  async convertCurrency(ConversionForm: NgForm) {
    if (this.currencyForm != null ){
      Swal.fire("Changes are not saved", "", "info");
      return
    }
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
    }}}
