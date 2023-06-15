using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;

namespace Entidades
{
    public class EntidadesContext : DbContext
    {
        public EntidadesContext(DbContextOptions<EntidadesContext> opt) : base(opt)
        {
            //this.Database.SetCommandTimeout(300);
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Poema> Poemas { get; set; }
        public DbSet<Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.ApplyConfiguration(new VotoConfig());
            modelBuilder.ApplyConfiguration(new PoemaConfig());
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            

        }
    }

    public static class ModelBuilderExtensions
    {
        public static void RemovePluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetAnnotation("Relational:TableName", entity.DisplayName());

                foreach (var prop in entity.GetProperties().Where(c => c.PropertyInfo != null && (c.PropertyInfo.PropertyType == typeof(DateTime) || c.PropertyInfo.PropertyType == typeof(DateTime?))))
                {
                    modelBuilder.Entity(entity.Name).Property(prop.Name).HasColumnType("datetime");
                }
            }
        }
    }
}
