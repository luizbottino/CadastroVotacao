using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

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

    }
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.UserName).HasColumnType("varchar(100)");
            builder.Property(c => c.Nome).HasColumnType("varchar(255)");
            builder.Property(c => c.Email).HasColumnType("varchar(255)");
            builder.Property(c => c.CPF).HasColumnType("varchar(14)");
            builder.Property(c => c.UF).HasColumnType("varchar(2)");
            builder.Property(x => x.Senha).HasColumnType("varchar(100)");
            builder.Property(x => x.Role).HasColumnType("varchar(100)");
            builder.Property(x => x.DataCadastro).HasColumnType("datetime");
        }
    }
}