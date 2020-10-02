using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNet
{
    public class Catalogo
    {
        //Adicionando uma lista da class Livros que criei
        public List<Livro> GetLivros()
        {
            var livros = new List<Livro>();
            livros.Add(new Livro("001", "Livro 1", 12.99m)); //sufixo "m" indica que é decimal 
            livros.Add(new Livro("002", "Livro 2", 23m));
            livros.Add(new Livro("003", "Livro 3", 43.23m));

            return livros;
        }
    }
} 
