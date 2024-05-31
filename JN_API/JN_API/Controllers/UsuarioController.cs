using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("ConsultarUsuarios")]
        public IActionResult ConsultarUsuarios()
        {
            return Ok("todo bien");
        }

    }
}
