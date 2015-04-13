using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class PedidoProduto
    {
        public int Id { get; set; } //isso poderia ser removido ja q a chava seria produto e pedido mas seria uma chave composta, se fosse usar nhibernate is pegar uashua
        public int IdProduto { get; set; }
        public int IdPedido { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
