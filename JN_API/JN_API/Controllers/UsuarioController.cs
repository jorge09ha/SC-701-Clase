using Dapper;
using JN_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JN_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration iConfiguration) : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(Usuario ent)
        {
            using (var contex = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                Respuesta resp = new Respuesta();

                var resut = await contex.ExecuteAsync("RegistrarUsuario", new
                {

                    ent.Identificacion,
                    ent.Nombre,
                    ent.Correo,
                    ent.Contrasenna
                },
                    commandType: System.Data.CommandType.StoredProcedure);

                if (resut > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "Registro Exitoso, puede inicar sesión";
                    resp.Contenido = true;

                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No se registro el usuario ya existe";
                    resp.Contenido = false;

                    return Ok(resp);
                }
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(Usuario ent)
        {
            using (var contex = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                //throw new Exception("SE PRESENTO UN ERROR");
                Respuesta resp = new Respuesta();

                var resut = await contex.QueryFirstAsync<Usuario>("IniciarSesion",
                    new { ent.Correo, ent.Contrasenna },
                    commandType: System.Data.CommandType.StoredProcedure);

                if (resut != null)
                {
                    resut.Token = GenerarToken(resut.Consecutivo);

                    resp.Codigo = 1;
                    resp.Mensaje = "Inicio exitoso";
                    resp.Contenido = resut;

                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "No se pudo iniciar";
                    resp.Contenido = false;

                    return Ok(resp);
                }
            }
        }

        [Authorize]
        [HttpGet]
        [Route("prueba")]
        public IActionResult prueba()
        {
            return Ok("PRUEBA");
        }

        private string GenerarToken(int Consecutivo)
        {
            string SecretKey = iConfiguration.GetSection("Keys:SecretKey").Value!;
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, Consecutivo.ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}