using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linq_Lambda.Entities;

namespace Linq_Lambda.Entities
{
    internal class Produto
    {
        public int Id { get; set; }// identificador unico do produto
        public string Name { get; set; } // Nome do produto
        public double Price { get; set; }// Valor do produto
        public Categoria Categoria  { get; set; } // categoria objeto (associação com classe produto)
        public override string ToString()
        {
            return Id
                + " , "
                + Name
                + ", "
                + Price.ToString("F2", CultureInfo.InvariantCulture)
                + ", "
                + Categoria.Name
                + ", "
                + Categoria.Tier;
        }
    }
}
