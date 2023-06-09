using Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] AccessCredentials credenciais,
            [FromServices] AccessManager accessManager,
            [FromServices] Servicos.UsuarioService srvUsuario)
        {
            try
            {

                var admin = await accessManager.ValidateCredentialsAdmin(credenciais);
                if (admin != null)
                {
                    //await srvUsuario.RegistrarLogin(Request.Headers["User-Agent"], admin.Id);
                    return Ok(accessManager.GenerateTokenAdmin(admin));
                }
                else
                {
                    return BadRequest("Falha ao autenticar");
                }
                //if (credenciais.ClientId != "Hck2dkWtZecy4eAq")
                //{
                //    var user = await accessManager.ValidateCredentials(credenciais);
                //    if (user != null)
                //    {
                //        //var result = srvParticipante.RegistrarLogin(Request.Headers["User-Agent"], user.Id);
                //        return Ok(accessManager.GenerateToken(user));
                //    }
                //    else
                //    {
                //        return BadRequest("Dados incorretos.");
                //    }
                //}
                //else
                //{
                //    var admin = await accessManager.ValidateCredentialsAdmin(credenciais);
                //    if (admin != null)
                //    {
                //        //await srvUsuario.RegistrarLogin(Request.Headers["User-Agent"], admin.Id);
                //        return Ok(accessManager.GenerateTokenAdmin(admin));
                //    }
                //    else
                //    {
                //        return BadRequest("Falha ao autenticar");
                //    }
                //}
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
