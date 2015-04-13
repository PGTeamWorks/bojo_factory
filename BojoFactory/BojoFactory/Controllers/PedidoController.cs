using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BojoFactory.Models;
using Dominio.Entidades;
using Repositorio;

namespace BojoFactory.Controllers
{
    public class PedidoController : Controller
    {
        private readonly RepositorioPedido  _repositorioPedido = new RepositorioPedido();
        private  readonly RepositorioPedidoProduto _repositorioPedidoProduto = new RepositorioPedidoProduto();

        // GET: Pedido
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult ConfirmarPedido(List<PedidoModel> model)
        {
            if (model == null || !model.Any())
                return Content("Nenhum item no pedido.");


            var valorTotal = model.Sum(m => m.Valor * m.Quantidade);
            var pedido = new Pedido()
            {
                DataPedido = DateTime.Now,
                ValorTotal = valorTotal,
                IdCliente = model.FirstOrDefault().IdCliente
            };

          
            pedido = _repositorioPedido.InsereAltera(pedido);
            
            
            foreach (var pedidoModel in model)
            {

                var pedidoProduto = new PedidoProduto{
                        IdPedido = pedidoModel.Id,
                        IdProduto = pedidoModel.Id,
                        Quantidade = pedidoModel.Quantidade,
                        Preco = pedidoModel.Valor
                    };

                _repositorioPedidoProduto.InsereAltera(pedidoProduto);

            }

            return View();
        }
    }
}