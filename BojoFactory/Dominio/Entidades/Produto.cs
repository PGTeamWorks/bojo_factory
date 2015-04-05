using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Tamanho { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
        public decimal SaldoEstoque { get; set; }
    }
}
