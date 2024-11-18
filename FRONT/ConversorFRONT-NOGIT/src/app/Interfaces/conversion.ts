export interface Conversion {
    fromCurrencyCode: string;
    toCurrencyCode: string;
    amount: number;
  }
  export interface ConversionRsp {
    fromCurrencyCode: string;
    toCurrencyCode: string;
    originalAmount: number;
    convertedAmount: number;
  }