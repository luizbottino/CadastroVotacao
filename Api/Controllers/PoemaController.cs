using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Servicos;
using ViewModels;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class PoemaController : ControllerBase
    {

        private readonly PoemaService _srvPoem;
        private readonly Security.UsuarioLogado _usuario;
        private readonly Servicos.Constantes _configs;
        private readonly AccessManager _accessManager;

        public PoemaController(PoemaService srvPoem,
            Servicos.Constantes configs,
            Security.UsuarioLogado usuario,
            AccessManager accessManager)
        {
            _configs = configs;
            _srvPoem = srvPoem;
            _usuario = usuario;
            _accessManager = accessManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var poema = await _srvPoem.Recuperar(_usuario.Id);

                if (poema == null)
                    return NotFound();


                return Ok(poema);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro na aplicação", exeption = e.Message });
            }
        }


        [HttpPost]
        [Route("votar/{idPoema},{Nota}")]
        public async Task<IActionResult> Votar([FromRoute] int idPoema, int nota)
        {
            try
            {
                if (nota >= 1 && nota <= 5)
                {
                    await _srvPoem.Votar(_usuario.Id, idPoema, nota);

                    return Ok();
                }
                else
                    return BadRequest("A nota deve ser entre 1 e 5!");

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro na aplicação", exeption = e.Message });
            }
        }


        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Post([FromBody] ViewModels.PoemaCadastroViewModel model)
        {
            try
            {

                model.IdUsuario = _usuario.Id;

                await _srvPoem.Cadastrar(model);

                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { mensagem = "Ocorreu um erro na aplicação", exeption = e.Message });
            }
        }

    }
}
