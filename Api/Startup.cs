using Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddApplicationInsightsTelemetry();

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Configuracoes:SecretKey").Value);
            services.AddSingleton(Configuration.GetSection("Configuracoes").Get<Servicos.Constantes>());

            services.AddDbContext<Entidades.EntidadesContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr"), x => x.MigrationsAssembly("Entidades")));
            services.AddScoped<Entidades.EntidadesContext, Entidades.EntidadesContext>();

            services.AddCors();
            services.AddControllers();
            services.AddScoped<Servicos.PoemaService>();
            services.AddScoped<Servicos.UsuarioService>();
            services.AddScoped<AccessManager>();
            services.AddScoped<UsuarioLogado>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CadastroVotacao", Version = "v1" });
            });

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddJwtSecurity(signingConfigurations, tokenConfigurations);


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            IdentityModelEventSource.ShowPII = true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CadastroVotacao v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Entidades.EntidadesContext>
    {
        public Entidades.EntidadesContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<Entidades.EntidadesContext>();
            var connectionString = configuration.GetConnectionString("ConnStr");
            builder.UseSqlServer(connectionString);
            return new Entidades.EntidadesContext(builder.Options);
        }
    }
}
