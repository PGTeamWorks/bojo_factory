using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class MateriaPrima
    {
        public Int32 Id { get; set; }
        public string Descricao { get; set; }
        public decimal SaldoEstoque { get; set; }
        public decimal PrecoCusto { get; set; }
    }
}
