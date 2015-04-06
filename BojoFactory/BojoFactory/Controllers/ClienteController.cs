using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BojoFactory.ViewModel;
using Dominio.Entidades;
using Newtonsoft.Json.Linq;
using Repositorio;

namespace BojoFactory.Controllers
{
    public class ClienteController : BaseController
    {
        readonly RepositorioCliente _repositorio = new RepositorioCliente();

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inserir()   
        {
            return PartialView("Modal/_Inserir");
        }   

        [HttpPost]
        public ActionResult Inserir(ClienteViewModel cliente)
        {
           var obj = Mapper.Map<ClienteViewModel, Cliente>(cliente);
            try
            {
                var objInserido = _repositorio.InsereAltera(obj);
                return Json(new { objInserido }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
               return Json(new {erro = true, exception.Message}, JsonRequestBehavior.AllowGet);
            }
           
            
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var cliente = _repositorio.ObterPorId(id);
            return PartialView("Modal/_Editar",cliente);
        }

        public ActionResult Listar()
        {
            var clientes = _repositorio.Obter();
            return Json(new {clientes}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalhar(int id)
        {
            var cliente = _repositorio.ObterPorId(id);
            return PartialView("Modal/_Detalhes", cliente);
        }

        public ActionResult Excluir(int id)
        {
            var cliente = _repositorio.ObterPorId(id);
            return PartialView("Modal/_Excluir",cliente);
        }

        [HttpPost]
        public ActionResult ExcluirPorId(int id)
        {
            try
            {
                var cliente = _repositorio.Deleta(id);
                return Json(new {cliente}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                
                throw new Exception(exception.Message);
            }
           
        }
    }
}