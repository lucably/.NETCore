using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace dotNet
{ 
	public class Relatorio
	{
		public Relatorio(Catalogo catalogo)
        {
			this.catalogo = catalogo;
        }

		private readonly Catalogo catalogo;

		//Substitui o void pelo Task.
		public async Task Imprimir(HttpContext context)
        {
			foreach (var livro in catalogo.GetLivros())
			{
				//Faz a requisição mandando para o endereço, o -10,-40,10 => Significa permite o num de caracteres, o"-" é para alinhar a esquerda, e o toString("C") formata o texto em Moeda
				await context.Response.WriteAsync($"{livro.Codigo,-10}{livro.Nome,-40}{livro.Preco.ToString("C"),10}\r\n");
				//Esse context.Response.WriteAsync era chamado no Startup.cs, serve para renderizar no HTTP
			}
		}
	}
}