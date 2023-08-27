using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Lambda.Entities
{
    internal class Categoria
    {
        public int Id { get; set; } // Identificador Unico
        public string Name { get; set; } // Nome
        public int Tier { get; set; } // Nivel
    }
}
