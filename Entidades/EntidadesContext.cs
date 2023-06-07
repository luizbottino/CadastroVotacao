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
        //public DbSet<Participante> Participantes { get; set; }
        //public DbSet<LogAlteracaoSenha> LogAlteracaoSenhas { get; set; }
        //public DbSet<LogAcesso> LogAcessos { get; set; }
        //public DbSet<Cupom> Cupons { get; set; }
        //public DbSet<CupomTipo> CupomTipos { get; set; }
        //public DbSet<Raspadinha> Raspadinhas { get; set; }
        //public DbSet<RaspadinhaPremio> RaspadinhaPremios { get; set; }
        //public DbSet<ArquivoTransporte> ArquivoTransportes { get; set; }
        //public DbSet<ArquivoImpedidoTransporte> ArquivoImpedidoTransportes { get; set; }
        //public DbSet<Sorteio> Sorteios { get; set; }
        //public DbSet<Premio> Premios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        //public DbSet<LogAdminAlteracaoSenha> LogAdminAlteracaoSenhas { get; set; }
        //public DbSet<LogAdminAcesso> LogAdminAcessos { get; set; }
        //public DbSet<QrCode> QrCodes { get; set; }
        //public DbSet<Unidade> Unidades { get; set; }
        //public DbSet<UsuarioUnidade> UsuarioUnidades { get; set; }
        //public DbSet<AcaoDigitalRealizada> AcoesDigitaisRealizadas { get; set; }
        //public DbSet<PesquisaPergunta> PesquisaPerguntas { get; set; }
        //public DbSet<PesquisaResposta> PesquisaRespostas { get; set; }
        //public DbSet<AvaliacaoSemanal> AvaliacoesSemanais { get; set; }
        //public DbSet<AvaliacaoSemanalNovo> AvaliacoesSemanaisNovo { get; set; }
        //public DbSet<Acao> Acaos { get; set; }
        //public DbSet<Brinde> Brindes { get; set; }
        //public DbSet<Estoque> Estoques { get; set; }


        #region Tipos Complexos
        //public DbSet<ExtratoConsolidado> ExtratoConsolidados { get; set; }
        //public DbSet<ExtratoDetalhado> ExtratoDetalhados { get; set; }
        //public DbSet<NumeroSecap> NumeroSecaps { get; set; }
        //public DbSet<ParticipanteArquivo> ParticipanteArquivos { get; set; }
        //public DbSet<GanhadorRaspadinha> GanhadorRaspadinhas { get; set; }
        //public DbSet<ParticipanteRaspadinhaSemUso> ParticipanteRaspadinhaSemUsos { get; set; }
        //public DbSet<ParticipantesBaseGeral> ParticipantesBaseGerals { get; set; }
        //public DbSet<BrindeUnidade> BrindeUnidades { get; set; }
        //public DbSet<AvaliacoesDash> AvaliacoesDashs { get; set; }
        //public DbSet<AvaliacoesNovoDash> AvaliacoesNovoDashs { get; set; }
        //public DbSet<EstoqueDash> EstoqueDashs { get; set; }
        //public DbSet<CupomDash> CupomDashs { get; set; }
        //public DbSet<CadastrosDash> CadastrosDashs { get; set; }
        //public DbSet<AcoesDiariasDash> AcoesDiariasDashs { get; set; }
        //public DbSet<ParticipanteCadastroBase> ParticipanteCadastroBases { get; set; }
        //public DbSet<PesquisaDash> PesquisaDashs { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Tipos Complexos
            //É para não criar essas tabelas no banco de dados
            //builder.Entity<ParticipanteArquivo>().HasNoKey().ToView(null);
            //builder.Entity<ExtratoConsolidado>().HasNoKey().ToView(null);
            //builder.Entity<ExtratoDetalhado>().HasNoKey().ToView(null);
            //builder.Entity<NumeroSecap>().HasNoKey().ToView(null);
            //builder.Entity<GanhadorRaspadinha>().HasNoKey().ToView(null);
            //builder.Entity<ParticipanteRaspadinhaSemUso>().HasNoKey().ToView(null);
            //builder.Entity<ParticipantesBaseGeral>().HasNoKey().ToView(null);
            //builder.Entity<BrindeUnidade>().HasNoKey().ToView(null);
            //builder.Entity<AvaliacoesDash>().HasNoKey().ToView(null);
            //builder.Entity<AvaliacoesNovoDash>().HasNoKey().ToView(null);
            //builder.Entity<EstoqueDash>().HasNoKey().ToView(null);
            //builder.Entity<CupomDash>().HasNoKey().ToView(null);
            //builder.Entity<CadastrosDash>().HasNoKey().ToView(null);
            //builder.Entity<AcoesDiariasDash>().HasNoKey().ToView(null);
            //builder.Entity<ParticipanteCadastroBase>().HasNoKey().ToView(null);
            //builder.Entity<PesquisaDash>().HasNoKey().ToView(null);
            #endregion

            builder.RemovePluralizingTableNameConvention();

            //builder.ApplyConfiguration(new ParticipanteConfig());
            //builder.ApplyConfiguration(new LogAlteracaoSenhaConfig());
            //builder.ApplyConfiguration(new CupomConfig());
            //builder.ApplyConfiguration(new CupomTipoConfig());
            //builder.ApplyConfiguration(new RaspadinhaConfig());
            //builder.ApplyConfiguration(new RaspadinhaPremioConfig());
            //builder.ApplyConfiguration(new LogAcessoConfig());
            //builder.ApplyConfiguration(new ArquivoTransporteConfig());
            //builder.ApplyConfiguration(new ArquivoImpedidoTransporteConfig());
            //builder.ApplyConfiguration(new SorteioConfig());
            //builder.ApplyConfiguration(new PremioConfig());
            //builder.ApplyConfiguration(new UsuarioConfig());
            //builder.ApplyConfiguration(new LogAdminAcessoConfig());
            //builder.ApplyConfiguration(new LogAdminAlteracaoSenhaConfig());
            //builder.ApplyConfiguration(new QrCodeConfig());
            //builder.ApplyConfiguration(new UnidadeConfig());
            //builder.ApplyConfiguration(new UsuarioUnidadeConfig());
            //builder.ApplyConfiguration(new AcaoDigitalRealizadaConfig());
            //builder.ApplyConfiguration(new PesquisaPerguntaConfig());
            //builder.ApplyConfiguration(new PesquisaRespostaConfig());
            //builder.ApplyConfiguration(new AvaliacaoSemanalConfig());
            //builder.ApplyConfiguration(new AvaliacaoSemanalNovoConfig());
            //builder.ApplyConfiguration(new AcaoConfig());
            //builder.ApplyConfiguration(new BrindeConfig());
            //builder.ApplyConfiguration(new EstoqueConfig());
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
