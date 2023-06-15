using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Poema
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int TotalVotos { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public Usuario Usuario { get; set; }
        public List<Voto> Votos { get; set; }
    }

    public class PoemaConfig : IEntityTypeConfiguration<Poema>
    {
        public void Configure(EntityTypeBuilder<Poema> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);

            modelBuilder.Property(c => c.Titulo).HasColumnType("varchar(255)");
            modelBuilder.Property(c => c.Descricao).HasColumnType("varchar(max)");

        }
    }
}
