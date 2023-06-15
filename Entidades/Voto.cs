using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Voto
    {
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int IdPoema { get; set; }
        public Poema Poema{ get; set; }
        public DateTime Data { get; set; }


    }
    public class VotoConfig : IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> modelBuilder)
        {
            modelBuilder.HasKey(c => new { c.IdUsuario, c.IdPoema });

            modelBuilder.HasOne(c => c.Usuario).WithMany(c => c.Votos).HasForeignKey(c => c.IdUsuario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.HasOne(c => c.Poema).WithMany(c => c.Votos).HasForeignKey(c => c.IdPoema).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
