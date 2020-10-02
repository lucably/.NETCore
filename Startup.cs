using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dotNet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Adicionando uma lista da class Livros que criei
            var livros = new List<Livro>();
            livros.Add(new Livro("001","Livro 1", 12.99m)); //sufixo "m" indica que é decimal 
            livros.Add(new Livro("002", "Livro 2", 23m));
            livros.Add(new Livro("003", "Livro 3", 43.23m));


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    foreach(var livro in livros) {
                        //Faz a requisição mandando para o endereço, o -10,-40,10 => Significa permite o num de caracteres, o"-" é para alinhar a esquerda, e o toString("C") formata o texto em Moeda
                        await context.Response.WriteAsync($"{livro.Codigo,-10}{livro.Nome,-40}{livro.Preco.ToString("C"),10}\r\n");
                    }
                });
            });
        }
    }
}
