using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BojoFactory.ViewModel
{
    public class ProdutoMPViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Tamanho { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
        public decimal SaldoEstoque { get; set; }
        public int IdMateriaPrima { get; set; }
        public int QntMateriaPrima { get; set; }

    }
}