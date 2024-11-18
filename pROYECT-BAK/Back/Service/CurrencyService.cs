using Data.entidades;
using Data.Models;
using Data.repository;
using DTO.CURRENCY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CurrencyService : ICurrencyServices
    {
        private readonly CurrencyRepository _currencyRepository;

        public CurrencyService(CurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public List<CurrencyDTO> GetAllCurrencies()
        {
            // Obtener monedas globales (UserId = null)
            var globalCurrencies = _currencyRepository.GetGlobalCurrencies()
                .Select(c => new CurrencyDTO
                {
                    Id = c.Id,
                    Code = c.Code,
                    Legend = c.Legend,
                    Symbol = c.Symbol,
                    ConvertibilityIndex = c.ConvertibilityIndex,
                    Status = c.Status
                }).ToList();

            
            // Combinar ambas listas y retornar
            return globalCurrencies;
        }

        public CurrencyDTO GetCurrencyById(int id)
        {
            var currency = _currencyRepository.GetCurrencyById(id);
            
            return new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Legend = currency.Legend,
                Symbol = currency.Symbol,
                ConvertibilityIndex = currency.ConvertibilityIndex,
                Status = currency.Status
            };
        }
        public int AddCurrency(GlobalCurrencyDTO currencyDto)
        {
            var currency = new CurrencyConversion    
            {
             Code = currencyDto.Code,
             Legend = currencyDto.Legend,      
             Symbol = currencyDto.Symbol,      
             ConvertibilityIndex = currencyDto.ConvertibilityIndex,
             Status = CurrencyStatus.Alta        
            };

            return _currencyRepository.AddCurrency(currency);
        }
        public bool UpdateCurrency(int id, GlobalCurrencyDTO currencyDto)
        {
            var existingCurrency = _currencyRepository.GetCurrencyById(id);
            if (existingCurrency == null)
            {
                return false; 
            }

            existingCurrency.Code = currencyDto.Code;
            existingCurrency.Legend = currencyDto.Legend;
            existingCurrency.Symbol = currencyDto.Symbol;
            existingCurrency.ConvertibilityIndex = currencyDto.ConvertibilityIndex;
            existingCurrency.Status = CurrencyStatus.Modificacion;

            _currencyRepository.UpdateCurrency(existingCurrency);
            return true; 
        }

        

        public bool DeleteCurrency(int id)
        {
            var existingCurrency = _currencyRepository.GetCurrencyById(id);
            if (existingCurrency == null)
            {
                return false; // No se encontró la moneda para eliminar
            }

            existingCurrency.Status = CurrencyStatus.Baja; // Cambiar estado a Baja en lugar de eliminar físicamente
            _currencyRepository.UpdateCurrency(existingCurrency); // Guardar el cambio de estado
            return true; // Eliminación (cambio de estado) exitoso
        }
        public ConversionResultDTO ConvertCurrency(ConversionRequestDTO request)
        {
            var fromCurrency = _currencyRepository.GetCurrencyByCode(request.FromCurrencyCode);
            var toCurrency = _currencyRepository.GetCurrencyByCode(request.ToCurrencyCode);

            if (fromCurrency == null || toCurrency == null)
            {
                return null;
            }

            decimal convertedAmount = request.Amount * (fromCurrency.ConvertibilityIndex / toCurrency.ConvertibilityIndex);

            return new ConversionResultDTO
            {
                FromCurrencyCode = request.FromCurrencyCode,
                ToCurrencyCode = request.ToCurrencyCode,
                OriginalAmount = request.Amount,
                ConvertedAmount = convertedAmount
            };
        }


    }
}
