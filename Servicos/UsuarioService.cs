using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Servicos
{
    public class UsuarioService
    {
        private readonly Entidades.EntidadesContext _db;

        public UsuarioService(Entidades.EntidadesContext db)
        {
            _db = db;
        }

        public async Task<ViewModels.UsuarioViewModel> Login(string username, string senha)
        {
            return await _db.Usuarios.Where(c => c.UserName == username && c.Senha == senha).Select(c => new ViewModels.UsuarioViewModel
            {
                Id = c.Id,
                UserName = c.UserName,
                Role = c.Role,
                Nome = c.Nome,
                Email = c.Email,
            }).FirstOrDefaultAsync();
        }

        public async Task<ViewModels.UsuarioViewModel> RecuperarPorDocumento(string documento)
        {
            return await _db.Usuarios.Select(c => new ViewModels.UsuarioViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Email = c.Email,
            }).FirstOrDefaultAsync(c => c.Email == documento);
        }


        //public async Task AlterarSenha(int idUsuario, string senha, Guid token)
        //{
        //    var usuario = await _db.Usuarios.Where(x => x.Id.Equals(idUsuario)).FirstOrDefaultAsync();
        //    var logSenha = await _db.LogAlteracaoSenhas.Where(x => x.Id.Equals(token)).FirstOrDefaultAsync();

        //    usuario.Senha = senha;
        //    logSenha.Hash = senha;
        //    logSenha.DataUtilizacao = DateTime.Now;
        //    await _db.SaveChangesAsync();

        //    var tokens = await _db.LogAdminAlteracaoSenhas.Where(x => x.IdUsuario.Equals(idUsuario) && !x.DataUtilizacao.HasValue).ToListAsync();
        //    tokens.ForEach(x => { x.Removido = true; x.DataUtilizacao = DateTime.Now; });
        //    await _db.SaveChangesAsync();
        //}

        //public async Task<UsuarioViewModel> Cadastrar(ViewModels.UsuarioCadastroViewModel model)
        //{
        //    if (!await _db.Usuarios.AnyAsync(c => c.UserName == model.UserName))
        //    {
        //        var part = model.ToEntidade();

        //        _db.Usuarios.Add(part);
        //        await _db.SaveChangesAsync();

        //        return new UsuarioViewModel
        //        {
        //            Id = part.Id,
        //            UserName = part.UserName,
        //            Role = part.Role
        //        };
        //    }
        //    else
        //        throw new ArgumentException("Username ou Email já cadastrado");
        //}
    }
}
