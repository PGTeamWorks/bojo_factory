using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Formula
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdMateriaPrima { get; set; }
        public decimal Quantidade { get; set; }

    }
}
