using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Linq_Lambda.Entities;

namespace Linq_Lambda
{
    internal class Program
    {

        static void Print<T>(string mensagem, IEnumerable<T> Colecao)
        {
            Console.WriteLine(mensagem);
            foreach (T Obj in Colecao)
            {
                Console.WriteLine(Obj);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {//Produtos possuem 4 categoras (sendo categoria de 1ª linha ate 4ª linha
            Categoria C1 = new Categoria()
            {  
                Id = 1,
                Name = "Ferramentas",
                Tier = 2};
            Categoria C2 = new Categoria()
            {
                Id = 2,
                Name = "Computadores",
                Tier = 1
            };
            Categoria C3 = new Categoria()
            {
                Id = 1,
                Name = "Eletronicos",
                Tier = 1};

            List<Produto> produtos = new List<Produto>()
            {//PRODUTOS DA MINHA LISTA
                new Produto{ Id = 1,Name = "RazerGX7",Price = 2299.00, Categoria= C2},
                new Produto{ Id = 2,Name = "Pendrive",Price = 25.00, Categoria= C2},
                new Produto{ Id = 3,Name = "Hommer",Price = 999.00, Categoria= C3},
                new Produto{ Id = 4,Name = "Televisão",Price = 4999.00, Categoria= C3},
                new Produto{ Id = 5,Name = "NotebookIntel",Price = 13299.00, Categoria= C2},
                new Produto{ Id = 6,Name = "IntelI9",Price = 1299.00, Categoria= C2},
                new Produto{ Id = 7,Name = "Pentium 3",Price = 959.00, Categoria= C2},
                new Produto{ Id = 8,Name = "Furadeira",Price = 299.00, Categoria= C1},
                new Produto{ Id = 9,Name = "Barbeador",Price = 180.00, Categoria= C1},
                new Produto{ Id = 10,Name = "Dvd",Price = 350.00, Categoria= C3},
                new Produto{ Id = 11,Name = "Radio",Price = 120.00, Categoria= C3},
                new Produto{ Id = 12,Name = "Alexia",Price = 198.99, Categoria= C2}



            };

            //IRA IMPRIMIR SOMENTE OS PRODUTOS DO TIER 1 (PRIMEIRA CLASSE) E QUE TENHAM VALORES ABAIXO DE 900 REAIS
            // var R1 = produtos.Where(p => p.Categoria.Tier == 1 && p.Price < 900.00);
            var R1 = from p in produtos where p.Categoria.Tier == 1 && p.Price < 900.0
                     select p;
            Print("Tier 1 AND PRICE < 900",R1);

            //mostrar somente os produtos da categoria COMPUTADORES
            // var R2 = produtos.Where(p => p.Categoria.Name == "Computadores").Select(p => p.Name);
            var R2 = from p in produtos where p.Categoria.Name == "Ferramentas" select p.Name;
            Print("Names of Products From Computadores", R2);

            // var R3 = produtos.Where(p => p.Name[0] == 'R').Select(p => new { p.Name, p.Price, NomeCategoria = p.Categoria.Name });
            var R3 = from p in produtos where p.Name[0] == 'R' 
                     select new { p.Name, p.Price, categoriaNome = p.Categoria.Name };

            Print("Produtos começando com letra R ", R3);

            // var R4 = produtos.Where(p => p.Categoria.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);

            var R4 = from p in produtos
                     where p.Categoria.Tier == 1
                     orderby p.Name
                     orderby p.Price
                     select p;
            Print("Protudos do Tier 1 + Ordem crescente pelo preço + nome: ", R4);

            // var R5 = R4.Skip(2).Take(4);//pula 2 elementos e pega o 4 proximos

            var R5 = (from p in R4 select p).Skip(2).Take(4);
            Print("Pulando 2 primeiros elementos e capturando os 4 elementos ", R5);

            var R6 = produtos.First();
            Console.WriteLine("Primeiro elemento: " + R6);
            //podemos usar o FirstOrDefault = caso não encontre o elemento , retorna nullo e evita excessão
            var R7 = produtos.Where(p => p.Price > 5999.00).FirstOrDefault();
            // Console.WriteLine("Teste First elemento vazio  excessão e gerada: " + R7);

            Console.WriteLine("Teste FirstOrDefault retorna nullo,\n caso não encontre elemento: " + R7);

            var R8 = produtos.Where(p => p.Id == 10).SingleOrDefault();
            Console.WriteLine("Single Or Default teste 1 :" + R8);

            var R9 = produtos.Max(p => p.Price);//Posso definir o ID , Name, Price.
            Console.WriteLine("O Produto com Maior preço : " + R9.ToString("F2",CultureInfo.InvariantCulture));

            var R10 = produtos.Min(p => p.Price);//Posso definir o ID , Name, Price.
            Console.WriteLine("O Produto com Menor preço : " + R10.ToString("F2", CultureInfo.InvariantCulture));

            var R11 = produtos.Where(p => p.Categoria.Tier == 1).Sum(p => p.Price);
            Console.WriteLine("Ira somar todos os valores dos produtos: \n presente na Tier 1: " +"R$:"+ R11);

            var R12 = produtos.Where(p => p.Categoria.Tier == 1).Average(p => p.Price);
            Console.WriteLine("Ira retornar a media dos valores TIER 1 : " + R12);
            //caso não existe a sequencia de elementos ( previnir um possivel erro de buscar)
            var R13 = produtos.Where(p=> p.Categoria.Tier == 5).Select(p => p.Price).DefaultIfEmpty(0).Average();
            Console.WriteLine("Função Default If Empty: vazio / valor inexistente: " + R13);
            //utlizando funçoes aggregate (reduce) //UTILIZANDO O TRATAMENTO VAZIO 0.0
            var R14 = produtos.Where(p => p.Categoria.Tier ==1).Select(p => p.Price).Aggregate(0.0,(x,y)=> x + y);
            Console.WriteLine("Categoria 1 utilizando o Aggregate soma : " + R14);
            //OPERAÇÃO DE AGRUPAMENTO GroupBy


            //var R15 = produtos.GroupBy(p => p.Categoria);

            var R15 = from p in produtos group p by p.Categoria;
            Console.WriteLine();
            foreach (IGrouping<Categoria,Produto> grupo in R15)
            {
                Console.WriteLine("Categoria Chave/Key : " + grupo.Key.Name + " : ");
                foreach(Produto p in grupo)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
          
        }
    }
}
