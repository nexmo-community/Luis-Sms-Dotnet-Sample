using Microsoft.AspNetCore.Mvc;
using Nexmo.Api;

namespace LuisVonageDemo.Controllers
{
    [ApiController]
    public class SmsController : ControllerBase
    {
        [HttpGet("webhooks/inbound")]
        public IActionResult Get([FromQuery]SMS.SMSInbound inbound)
        {
            Dispatcher.ExecuteQuery(inbound);
            return NoContent();
        }
    }
}