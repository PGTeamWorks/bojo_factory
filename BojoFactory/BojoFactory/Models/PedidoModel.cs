using System;

namespace BojoFactory.Models
{
    public class PedidoModel
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public int Quantidade  { get; set; }
        public decimal Valor { get; set; }
        public int IdCliente { get; set; }
    }
}
