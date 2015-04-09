using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio.Entidades;
using Repositorio;

namespace BojoFactory.Controllers
{
    public class MateriaPrimaController : Controller
    {
        private readonly RepositorioMateriaPrima _repositorioMateriaPrima = new RepositorioMateriaPrima();

        // GET: MateriaPrima
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inserir()
        {
            return PartialView("Modal/_Inserir");
        }

        [HttpPost]
        public ActionResult Inserir(MateriaPrima materiaPrima)
        {
            try
            {
                var objInserido = _repositorioMateriaPrima.InsereAltera(materiaPrima);
                return Json(new { erro = false, objInserido }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { erro = true, exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var materiaPrima = _repositorioMateriaPrima.ObterPorId(id);
            return PartialView("Modal/_Editar", materiaPrima);
        }

        public ActionResult Listar()
        {
            var materiasPrima = _repositorioMateriaPrima.Obter();
            return Json(new { materiasPrima }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalhar(int id)
        {
            var materiaPrima = _repositorioMateriaPrima.ObterPorId(id);
            return PartialView("Modal/_Detalhes", materiaPrima);
        }

        public ActionResult Excluir(int id)
        {
            var materiaPrima = _repositorioMateriaPrima.ObterPorId(id);
            return PartialView("Modal/_Excluir", materiaPrima);
        }

        [HttpPost]
        public ActionResult ExcluirPorId(int id)
        {
            try
            {
                var materiaPrima = _repositorioMateriaPrima.Deleta(id);

                return Json(new { materiaPrima }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { erro = true, exception.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}