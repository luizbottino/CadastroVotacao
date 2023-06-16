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
                var rec = await _srvPoem.Recuperar(_usuario.Id);

                if (rec == null)
                    return NotFound();

                //rec.Foto = $@"https://stgseachegue.blob.core.windows.net/fotos/{rec.Foto}";
                //rec.Video = $@"https://stgseachegue.blob.core.windows.net/videos/{rec.Video}";

                return Ok(rec);
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

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("top20")]
        //public async Task<IActionResult> GetTop20()
        //{
        //    try
        //    {
        //        var lista = await _srvPoem.Listar(true, false, _usuario.Id);

        //        lista.ForEach(x =>
        //        {
        //            x.Foto = $@"https://stgseachegue.blob.core.windows.net/fotos/{x.Foto}";
        //            x.Video = $@"https://stgseachegue.blob.core.windows.net/videos/{x.Video}";
        //        });


        //        return Ok(lista);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, new { mensagem = "Ocorreu um erro na aplicação", exeption = e.Message });
        //    }
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("top5")]
        //public async Task<IActionResult> GetTop5()
        //{
        //    try
        //    {
        //        var lista = await _srvPoem.Listar(false, true, _usuario.Id);

        //        lista.ForEach(x =>
        //        {
        //            x.Foto = $@"https://stgseachegue.blob.core.windows.net/fotos/{x.Foto}";
        //            x.Video = $@"https://stgseachegue.blob.core.windows.net/videos/{x.Video}";
        //        });


        //        return Ok(lista);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, new { mensagem = "Ocorreu um erro na aplicação", exeption = e.Message });
        //    }
        //}

        [HttpPost]
        [Route("votar/{idReceita}")]
        public async Task<IActionResult> Votar([FromRoute] int idReceita)
        {
            try
            {
                if (DateTime.Now < new DateTime(2023, 6, 12))
                    return BadRequest("O período para votar ainda não se iniciou, aguarde.");

                await _srvPoem.Votar(_usuario.Id, idReceita);

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

        //[HttpDelete]
        //[AllowAnonymous]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[Route("voto/{matricula}")]
        //public async Task<IActionResult> DeleteVoto([FromRoute] string matricula)
        //{
        //    try
        //    {
        //        await _srvPoem.ZerarVoto(matricula);

        //        return Ok();
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, new { mensagem = "Ocorreu um erro na aplicação", exeption = e.Message });
        //    }
        //}


        //[HttpDelete]
        //[AllowAnonymous]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[Route("{matricula}")]
        //public async Task<IActionResult> Delete([FromRoute] string matricula)
        //{
        //    try
        //    {
        //        await _srvPoem.Zerar(matricula);

        //        return Ok();
        //    }
        //    catch (ArgumentException e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, new { mensagem = "Ocorreu um erro na aplicação", exeption = e.Message });
        //    }
        //}

        //[HttpPost]
        //[Route("video")]
        //[RequestSizeLimit(524288000)]
        //public async Task<IActionResult> UploadVideo([FromForm] IFormFile anexo)
        //{
        //    int linha = 1;
        //    try
        //    {
        //        MemoryStream contents = new MemoryStream();
        //        if (anexo != null && anexo.Length > 0)
        //        {
        //            string nomeVideo = $"{DateTime.Now.Ticks}.mp4";
        //            anexo.CopyTo(contents);

        //            await Util.Utilitario.UpLoadBlobAsync(contents, nomeVideo, "videos", anexo.ContentType, _configs.StorageConn);

        //            return Ok($@"https://stgseachegue.blob.core.windows.net/videos/{nomeVideo}");
        //        }
        //        return BadRequest("Arquivo não enviado");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erro na linha {linha}: {ex.Message}");
        //    }

        //}


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
