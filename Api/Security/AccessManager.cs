using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace Api.Security
{
    public class AccessManager
    {
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private IMemoryCache _cache;
        private readonly Servicos.UsuarioService _srvPart;
        private readonly Servicos.UsuarioService _srvUsuario;
        private readonly Security.UsuarioLogado _usuarioLogado;

        public AccessManager(
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IMemoryCache cache,
            Servicos.UsuarioService srvPart,
            Security.UsuarioLogado usuario, Servicos.UsuarioService srvUsuario)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _cache = cache;
            _srvPart = srvPart;
            _usuarioLogado = usuario;
            _srvUsuario = srvUsuario;
        }
        public async Task<ViewModels.UsuarioViewModel> ValidateCredentialsAdmin(AccessCredentials credenciais)
        {
            if (credenciais != null && !String.IsNullOrWhiteSpace(credenciais.UserID))
            {
                if (credenciais.GrantType == "password")
                {
                    // Verifica a existência do usuário nas tabelas do
                    // ASP.NET Core Identity                    
                    return await _srvUsuario.Login(credenciais.UserID, Util.Utilitario.CalculateSHA1(credenciais.Password));
                }
                else if (credenciais.GrantType == "refresh_token")
                {
                    if (!String.IsNullOrWhiteSpace(credenciais.RefreshToken))
                    {
                        RefreshTokenData refreshTokenBase = null;

                        string strTokenArmazenado =
                            _cache.Get(credenciais.RefreshToken).ToString();
                        if (!String.IsNullOrWhiteSpace(strTokenArmazenado))
                        {
                            refreshTokenBase = JsonConvert
                                .DeserializeObject<RefreshTokenData>(strTokenArmazenado);
                        }

                        bool credenciaisValidas = (refreshTokenBase != null && credenciais.UserID == refreshTokenBase.UserID && credenciais.RefreshToken == refreshTokenBase.RefreshToken);

                        // Elimina o token de refresh já que um novo será gerado
                        if (credenciaisValidas)
                        {
                            _cache.Remove(credenciais.RefreshToken);
                        }
                    }
                }
            }

            return null;
        }

        public Token GenerateTokenAdmin(UsuarioViewModel admin)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity("admin@admin.com.br", "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, admin.UserName),
                        new Claim(ClaimTypes.GivenName, admin.UserName),
                        new Claim(ClaimTypes.Role, admin.Role),
                        new Claim(ClaimTypes.Sid, admin.Id.ToString())
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = DateTime.UtcNow.AddMinutes(60);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            var resultado = new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK",
                Id = admin.Id,
                Nome = admin.UserName,
                Role = admin.Role

            };
            return resultado;
        }

        public Token GenerateToken(ViewModels.UsuarioViewModel credenciais)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(credenciais.Email, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, credenciais.Email),
                        new Claim(ClaimTypes.GivenName, credenciais.Nome),
                        new Claim(ClaimTypes.Sid, credenciais.Id.ToString())
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromMinutes(_tokenConfigurations.Minutes);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            var resultado = new Token()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                RefreshToken = Guid.NewGuid().ToString().Replace("-", String.Empty),
                Message = "OK",
                Id = credenciais.Id,
                Nome = credenciais.Nome
            };

            // Armazena o refresh token em cache através do Redis 
            var refreshTokenData = new RefreshTokenData();
            refreshTokenData.RefreshToken = resultado.RefreshToken;
            refreshTokenData.UserID = credenciais.Email;


            // Calcula o tempo máximo de validade do refresh token
            // (o mesmo será invalidado automaticamente pelo Redis)
            TimeSpan finalExpiration =
                TimeSpan.FromMinutes(_tokenConfigurations.FinalExpiration);

            MemoryCacheEntryOptions opcoesCache =
                new MemoryCacheEntryOptions();
            opcoesCache.SetAbsoluteExpiration(finalExpiration);
            _cache.Set(resultado.RefreshToken,
                JsonConvert.SerializeObject(refreshTokenData),
                opcoesCache);

            return resultado;
        }
        public async Task<ViewModels.UsuarioViewModel> ValidateCredentials(AccessCredentials credenciais)
        {
            if (credenciais != null && (!String.IsNullOrWhiteSpace(credenciais.UserID) || !String.IsNullOrWhiteSpace(_usuarioLogado.Documento)))
            {
                if (credenciais.GrantType == "password")
                {
                    return await _srvPart.Login(credenciais.UserID, Util.Utilitario.CalculateSHA1(credenciais.Password));
                }
                else if (credenciais.GrantType == "token")
                {
                    return await _srvPart.RecuperarPorDocumento(_usuarioLogado.Documento);
                }
                else if (credenciais.GrantType == "refresh_token")
                {
                    if (!String.IsNullOrWhiteSpace(credenciais.RefreshToken))
                    {
                        RefreshTokenData refreshTokenBase = null;

                        string strTokenArmazenado =
                            _cache.Get(credenciais.RefreshToken).ToString();
                        if (!String.IsNullOrWhiteSpace(strTokenArmazenado))
                        {
                            refreshTokenBase = JsonConvert
                                .DeserializeObject<RefreshTokenData>(strTokenArmazenado);
                        }

                        bool credenciaisValidas = (refreshTokenBase != null && credenciais.UserID == refreshTokenBase.UserID && credenciais.RefreshToken == refreshTokenBase.RefreshToken);

                        // Elimina o token de refresh já que um novo será gerado
                        if (credenciaisValidas)
                        {
                            _cache.Remove(credenciais.RefreshToken);
                            return await _srvPart.RecuperarPorDocumento(credenciais.UserID);
                        }
                    }
                }
            }

            return null;
        }
     
    }

    public class UsuarioAdminViewModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
