using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Servicos;
using ViewModels;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioService _srvPart;
        private readonly Security.UsuarioLogado _usuario;
        private readonly Servicos.Constantes _configs;
        private readonly AccessManager _accessManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IWebHostEnvironment _env;

        public UsuarioController(UsuarioService srvPart,
            Servicos.Constantes configs,
            Security.UsuarioLogado usuario,
            AccessManager accessManager,
            IHttpContextAccessor accessor,
            IWebHostEnvironment hostingEnvironment)
        {
            _configs = configs;
            _srvPart = srvPart;
            _usuario = usuario;
            _accessManager = accessManager;
            _accessor = accessor;
            _env = hostingEnvironment;
        }



        //[HttpGet]
        //[Route("participantes")]
        //[Authorize(Roles = "administrador,participantes", Policy = "Bearer")]
        //public async Task<ActionResult<PaginacaoViewModel<ParticipanteViewModel>>> GetParticipantes(int pagina, int itensPorPagina, string nome, string cpf)
        //{
        //    cpf = Util.Utilitario.LimparMascara(cpf);
        //    var response = await _srvPart.ListarParticipantes(nome, cpf, pagina, itensPorPagina);
        //    // não estava retornando o objeto pelo response
        //    // tive que criar um anonimo para retornar os dados
        //    object objeto = new
        //    {
        //        Pagina = response.Pagina,
        //        ItensPorPagina = itensPorPagina,
        //        Total = response.Total,
        //        Itens = response.Itens
        //    };
        //    return Ok(objeto);
        //}


        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> Post([FromBody] ViewModels.UsuarioCadastroViewModel model)
        //{
        //    try
        //    {
        //        var part = await _srvPart.Cadastrar(model);

        //        return Ok(new { id = part.Id});
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }

            //}
            //private async Task EnviarEmail(ViewModels.ParticipanteCadastroViewModel model)
            //{
            //    string html = "";
            //    string perfil = "";

            //    if (model.Resposta1 == "A")
            //    {
            //        html = Directory.GetCurrentDirectory() + "\\Emkt\\perfil\\explorador.html";
            //        perfil = "Explorador";
            //    }
            //    else if (model.Resposta1 == "B")
            //    {
            //        html = Directory.GetCurrentDirectory() + "\\Emkt\\perfil\\futuro-empreendedor.html";
            //        perfil = "Futuro Empreendedor Digital";
            //    }
            //    else if (model.Resposta1 == "C")
            //    {
            //        html = Directory.GetCurrentDirectory() + "\\Emkt\\perfil\\vendedor-digital.html";
            //        perfil = "Vendedor Digital";
            //    }
            //    else if (model.Resposta1 == "D")
            //    {
            //        html = Directory.GetCurrentDirectory() + "\\Emkt\\perfil\\expert-vendas.html";
            //        perfil = "Expert em Vendas na internet";
            //    }

            //    string layout_html = System.IO.File.ReadAllText(html, System.Text.Encoding.Default);
            //    layout_html = layout_html.Replace("[NOME]", model.Nome);
            //    layout_html = layout_html.Replace("[PERFIL]", perfil);
            //    layout_html = layout_html.Replace("[RESPOSTA1]", model.Resposta1);//NAO E NECESSARIO ALTERAR ESTE VALOR
            //    layout_html = layout_html.Replace("[RESPOSTA2]", model.Resposta2);
            //    layout_html = layout_html.Replace("[RESPOSTA3]", model.Resposta3);
            //    layout_html = layout_html.Replace("[RESPOSTA4]", model.Resposta4);
            //    layout_html = layout_html.Replace("[RESPOSTA5]", model.Resposta5);
            //    layout_html = layout_html.Replace("[RESPOSTA6]", model.Resposta6);

            //    await Util.Utilitario.EnviarEmailAsync(model.Email, "Quiz", layout_html, layout_html);
            //}

        }
    }
