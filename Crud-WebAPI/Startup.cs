using Crud_WebAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Crud_WebAPI
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

            /*
            Adicionando o comando do banco de Dados

            Precisamos agorar criar nossa conexão que sera o DefaultConn que ficara dentro do arquivo appsettings.Development.json
            adicionando o obj "ConnectionStrings":{
                "DefaultConn": "Data Source=CrudWebAPI.db"
            }

            Depois coloque o msm dado no appsettings.json.

            Depois disso execute o dotnet ef migrations add initial

            Agora execute dotnet ef database update => para rodar o migration dentro do banco de dados. Se n der erro ele ira criar um arquivo chamado "CrudWebAPI.db"
            Basta agora rolar o arquivo para dentro do DB Browser (exe)
            */
            services.AddDbContext<DataContext>(
                typeDB => typeDB.UseSqlite(Configuration.GetConnectionString("DefaultConn"))
            );

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crud_WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud_WebAPI v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
