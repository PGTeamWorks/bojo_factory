using System;
using System.Web.Mvc;
using BojoFactory.ViewModel;
using Dominio.Entidades;
using Repositorio;

namespace BojoFactory.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly RepositorioProduto _repositorioProduto = new RepositorioProduto();
        private readonly  RepositorioFormula _repositorioFormula = new RepositorioFormula();

        // GET: Produto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inserir()
        {
            return PartialView("Modal/_Inserir");
        }

        [HttpPost]
        public ActionResult Inserir(ProdutoMPViewModel produtoView)
        {
            try
            {
                var produto = new Produto
                {
                    Descricao = produtoView.Descricao,
                    Cor = produtoView.Cor,
                    Preco = produtoView.Preco,
                    SaldoEstoque = produtoView.SaldoEstoque,
                    Tamanho = produtoView.Tamanho
                };

                var objInserido = _repositorioProduto.InsereAltera(produto);

                var formula = new Formula
                {
                    IdMateriaPrima = produtoView.IdMateriaPrima,
                    IdProduto = objInserido.Id,
                    Quantidade = produtoView.QntMateriaPrima
                };

                var formulaInserida = _repositorioFormula.InsereAltera(formula);
                
                return Json(new { objInserido }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { erro = true, exception.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var cliente = _repositorioProduto.ObterPorId(id);
            return PartialView("Modal/_Editar", cliente);
        }

        public ActionResult Listar()
        {
            var produtos = _repositorioProduto.Obter();
            return Json(new { produtos }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalhar(int id)
        {
            var produto = _repositorioProduto.ObterPorId(id);
            return PartialView("Modal/_Detalhes", produto);
        }

        public ActionResult Excluir(int id)
        {
            var cliente = _repositorioProduto.ObterPorId(id);
            return PartialView("Modal/_Excluir", cliente);
        }

        [HttpPost]
        public ActionResult ExcluirPorId(int id)
        {
            try
            {
                var produto = _repositorioProduto.Deleta(id);

                return Json(new { produto }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { erro = true, exception.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
    
}