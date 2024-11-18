using DTO.CURRENCY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace ConversorMonedasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ConversionController : ControllerBase
    {
        private readonly ICurrencyServices _conversionService;
        private readonly ISubscriptionService _subscriptionService;

        public ConversionController(ICurrencyServices conversionService, ISubscriptionService subscriptionService)
        {
            _conversionService = conversionService;
            _subscriptionService = subscriptionService;
        }

        // Método auxiliar para verificar si el usuario tiene conversiones restantes
        private bool HasConversionsRemaining(int userId)
        {
            return _subscriptionService.HasConversionsLeft(userId);
        }

        // Endpoint para obtener todas las monedas
        [HttpGet("currencies")]
        public IActionResult GetAllCurrencies()
        {
            

            var currencies = _conversionService.GetAllCurrencies();
            return Ok(currencies);
        }
        // Endpoint para obtener una moneda específica por su ID
        [HttpGet("currency/{id}")]
        public IActionResult GetCurrencyById(int userId, int id)
        {
            if (!HasConversionsRemaining(userId))
            {
                return Forbid("No tienes conversiones restantes para tu suscripción.");
            }

            var currency = _conversionService.GetCurrencyById(id);
            if (currency == null)
            {
                return NotFound("Moneda no encontrada.");
            }
            return Ok(currency);
        }

        // Endpoint para crear una nueva moneda
        [HttpPost("currency")]
        public IActionResult AddCurrency(int userId, [FromBody] GlobalCurrencyDTO currencyDto)
        {
            if (!HasConversionsRemaining(userId))
            {
                return Forbid("No tienes conversiones restantes para tu suscripción.");
            }

            var currencyId = _conversionService.AddCurrency(currencyDto);
            return CreatedAtAction(nameof(GetCurrencyById), new { id = currencyId, userId = userId }, currencyDto);
        }

        // Endpoint para actualizar una moneda existente
        [HttpPut("currency/{id}")]
        public IActionResult UpdateCurrency(int userId, int id, [FromBody] GlobalCurrencyDTO currencyDto)
        {
            if (!HasConversionsRemaining(userId))
            {
                return Forbid("No tienes conversiones restantes para tu suscripción.");
            }

            bool result = _conversionService.UpdateCurrency(id, currencyDto);
            if (!result)
            {
                return NotFound("No se pudo actualizar la moneda, puede que no exista.");
            }
            return NoContent();
        }


        // Endpoint para eliminar una moneda (cambio de estado a Baja)
        [HttpDelete("currency/{id}")]
        public IActionResult DeleteCurrency(int userId, int id)
        {
            if (!HasConversionsRemaining(userId))
            {
                return Forbid("No tienes conversiones restantes para tu suscripción.");
            }

            bool result = _conversionService.DeleteCurrency(id);
            if (!result)
            {
                return NotFound("No se pudo eliminar la moneda, puede que no exista.");
            }
            return NoContent();
        }

        [HttpPost("convert")]
        public IActionResult ConvertCurrency(int userId, [FromBody] ConversionRequestDTO request)
        {
            var conversionResult = _conversionService.ConvertCurrency(request);
            if (conversionResult == null)
            {
                return BadRequest("No se pudo realizar la conversión. Verifica los códigos de moneda.");
            }

            return Ok(conversionResult);
        }
        



    }
}