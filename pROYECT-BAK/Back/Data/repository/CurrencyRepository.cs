using Data.entidades;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.repository
{
    public class CurrencyRepository
    {
        private readonly MonedasContext _context;
        public CurrencyRepository(MonedasContext context)
        {
            _context = context;
        }

        public IEnumerable<CurrencyConversion> GetGlobalCurrencies()
        {
            return _context.currencyConversions
            .Where(c => c.Status == CurrencyStatus.Alta || c.Status == CurrencyStatus.Consulta)
            .ToList();
        }
        public IEnumerable<CurrencyConversion> GetCurrenciesByUserId(int userId)
        {
            return _context.currencyConversions
                .Where(c => c.UserId == userId && c.Status == CurrencyStatus.Alta)
                .ToList();
        }
        public CurrencyConversion GetCurrencyById(int id)
        {
            var currency = _context.currencyConversions.Find(id);
            return (currency.Status != CurrencyStatus.Baja) ? currency : null;
        }

        public int GetConversionsCountByUserId(int userId) // Método para contar las conversiones de un usuario específico
        {
            return _context.currencyConversions.Count(c => c.UserId == userId);
        }

        public int AddCurrency(CurrencyConversion currency)
        {
            _context.currencyConversions.Add(currency);
            _context.SaveChanges();
            return currency.Id;
        }
        public void UpdateCurrency(CurrencyConversion currency)
        {
            var existingCurrency = _context.currencyConversions.Find(currency.Id);
           
                existingCurrency.Code = currency.Code;
                existingCurrency.Legend = currency.Legend;
                existingCurrency.Symbol = currency.Symbol;
                existingCurrency.ConvertibilityIndex = currency.ConvertibilityIndex;
                existingCurrency.Status = CurrencyStatus.Modificacion;

                _context.SaveChanges();
            
        }
        public void DeleteCurrency(int id)
        {
            var currency = _context.currencyConversions.Find(id);
            if (currency != null)
            {
                currency.Status = CurrencyStatus.Baja;
                _context.SaveChanges();
            }

        }
        public CurrencyConversion GetCurrencyByCode(string code)
        {
            
            return _context.currencyConversions
                    .AsEnumerable()
                    .FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

        }
    }
}
