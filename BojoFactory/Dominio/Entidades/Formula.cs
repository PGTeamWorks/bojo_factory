using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Formula
    {
        public Int32 Id { get; set; }
        public Produto ProdutoId { get; set; }
        public MateriaPrima MateriaPrimaId { get; set; }
        public decimal Quantida { get; set; }

    }
}
