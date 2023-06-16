using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string UF { get; set; }
        public string Role { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<Poema> Poemas { get; set; }
        public List<Voto> Votos { get; set; }

    }
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Property(x => x.UserName).HasColumnType("varchar(100)");
            modelBuilder.Property(c => c.Nome).HasColumnType("varchar(255)");
            modelBuilder.Property(c => c.Email).HasColumnType("varchar(255)");
            modelBuilder.Property(c => c.CPF).HasColumnType("varchar(14)");
            modelBuilder.Property(c => c.UF).HasColumnType("varchar(2)");
            modelBuilder.Property(x => x.Senha).HasColumnType("varchar(100)");
            modelBuilder.Property(x => x.Role).HasColumnType("varchar(100)");
            modelBuilder.Property(x => x.DataCadastro).HasColumnType("datetime");

            modelBuilder.HasMany(c => c.Poemas).WithOne(c => c.Usuario).HasForeignKey(c => c.IdUsuario).OnDelete(DeleteBehavior.Cascade);
        }
    }
}