using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly Servicos.UsuarioService _srvPart;
        private readonly Servicos.Constantes _configs;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public ContaController(
            Servicos.UsuarioService srvPart
            //Servicos.Constantes configs
            )
        { 
            _srvPart = srvPart; 
            //_configs = configs;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody] ViewModels.LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await _srvPart.Login(model.UserName, Util.Utilitario.CalculateSHA1(model.Senha));

                    if (user == null)
                        return NotFound(new { message = "Usuário ou senha inválidos" });

                    var token = Util.Utilitario.GenerateToken(user, "administrador", _configs.SecretKey);

                    return Ok(new { user, token });
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
