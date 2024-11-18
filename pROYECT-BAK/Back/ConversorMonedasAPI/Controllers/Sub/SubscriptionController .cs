using DTO.SUBS;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace ConversorMonedasAPI.Controllers.Sub
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }


        // Endpoint para asignar una suscripción a un usuario
        [HttpPost("assign")]
        public IActionResult AssignSubscriptionToUser([FromBody] UserSubscriptionDTO userSubscriptionDto)
        {
            var status = _subscriptionService.AssignSubscriptionToUser(userSubscriptionDto);

            if (status == null)
            {
                return BadRequest("No se pudo asignar la suscripción. Verifique los datos.");
            }

            return Ok(status);
        }

        // Endpoint para verificar si el usuario tiene conversiones restantes
        [HttpGet("{userId}/remaining-conversions")]
        public IActionResult HasConversionsLeft(int userId)
        {
            bool hasConversions = _subscriptionService.HasConversionsLeft(userId);

            return Ok(new { HasConversionsLeft = hasConversions });
        }
    }
}
