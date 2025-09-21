using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class SecureController : ControllerBase
    {
        [HttpGet("check")]
        public IActionResult CheckAuthorization()
        {
            return Ok("Authorized access only");
        }
    }
}
