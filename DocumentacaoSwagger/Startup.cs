using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using DocumentacaoSwagger.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentacaoSwagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<Contexto>(conexao => conexao.UseSqlServer(Configuration.GetConnectionString("ConexaoBD")));

            services.AddControllers();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API de aviões",
                    Description = "API para a manipulação de dados referentes a aviões",
                    TermsOfService = new Uri("https://abcxyz.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Thiago",
                        Email = "thiago@email.com",
                        Url = new Uri("https://thiago.com")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Open SOurce",
                        Url = new Uri("https://opensource.com")
                    }
                });

                var arquivoSwaggerXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var diretorioArquivoXML = Path.Combine(AppContext.BaseDirectory, arquivoSwaggerXML);
                swagger.IncludeXmlComments(diretorioArquivoXML);
            });

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(ui =>
            {
                ui.SwaggerEndpoint("./v1/swagger.json", "API de Aviões V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
