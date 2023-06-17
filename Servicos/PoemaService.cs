using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Servicos
{
    public class PoemaService
    {
        private readonly Entidades.EntidadesContext _db;

        public PoemaService(Entidades.EntidadesContext db)
        {
            _db = db;
            _db.Database.SetCommandTimeout(600);
        }

        public async Task<PoemaCadastroViewModel> Recuperar(int idUsuario)
        {
            var poem = await _db.Poemas.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);


            if (poem != null)
            {
                return new PoemaCadastroViewModel
                {
                    IdUsuario = idUsuario,
                    Titulo = poem.Titulo,
                    Descricao = poem.Descricao,
                };
            }

            return null;
        }

        public async Task Votar(int idUsuario, int idPoema, int nota)
        {
            var query = $@"
                DECLARE @idPoema INT = {idPoema}, @idUsuario INT = {idUsuario}, @Nota INT = {nota}, @qtd INT

                IF NOT EXISTS (SELECT 1 FROM Voto v WHERE v.IdUsuario = @idUsuario)
                BEGIN
	                INSERT INTO Voto (IdUsuario, IdPoema, Data, Nota)
	                VALUES (@idUsuario, @idPoema, GETDATE(), @Nota);


	                SELECT @qtd = COUNT(1) FROM Voto v WHERE v.IdPoema = @idPoema


	                UPDATE Poema SET TotalVotos = @qtd WHERE Id = @idPoema
                END
                ELSE
                BEGIN
                    UPDATE Voto
                    SET IdUsuario = @idUsuario, IdPoema = @idPoema, Data = GETDATE(), Nota = @Nota
                    WHERE IdUsuario = @idUsuario
                END

            ";

            await _db.Database.ExecuteSqlRawAsync(query);
        }

        public async Task Cadastrar(PoemaCadastroViewModel model)
        {
            var user = await _db.Usuarios.Include(c => c.Poemas).FirstOrDefaultAsync(c => c.Id == model.IdUsuario);

            if (user == null)
                throw new ArgumentException("Refaça o login.");

            if (user.Poemas != null)
            {
                    _db.Poemas.Add(new Poema
                    {
                        Titulo = model.Titulo,
                        Descricao = model.Descricao,
                        IdUsuario = model.IdUsuario,
                        DataCadastro = DateTime.Now,
                    });
                }
                

            await _db.SaveChangesAsync();
        }
    }
}
