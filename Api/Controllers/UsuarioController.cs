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

        }
    }
