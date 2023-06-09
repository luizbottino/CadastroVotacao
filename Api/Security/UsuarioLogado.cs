using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Security
{
    public class UsuarioLogado
    {
		private readonly IHttpContextAccessor _accessor;

		public UsuarioLogado(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}

		public string Email => _accessor.HttpContext.User.Identity.Name;
		public string Nome => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.GivenName)?.Value;
		public string Documento => GetClaimsIdentity().FirstOrDefault(a => a.Type == Util.CustomClaims.Documento)?.Value;
		public int Id => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Sid) != null ? Convert.ToInt32(GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.Sid).Value) : 0;
		public DateTime DataNascimento => GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.DateOfBirth) != null ? Convert.ToDateTime(GetClaimsIdentity().FirstOrDefault(a => a.Type == ClaimTypes.DateOfBirth).Value) : new DateTime(2000,1,1);
		

		public IEnumerable<Claim> GetClaimsIdentity()
		{
			return _accessor.HttpContext.User.Claims;
		}
	}
}
