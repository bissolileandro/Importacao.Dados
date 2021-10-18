using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacao.Dados.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
    }
}
