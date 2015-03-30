using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BojoFactory.ViewModel;
using Newtonsoft.Json.Linq;
using Repositorio;

namespace BojoFactory.Controllers
{
    public class ClienteController : BaseController
    {
        //é, nesse caso nao da pra por num using :) mas é mais bonito assiim, na vdd seria se vc injetasse no contrutor :P
        // eu tentei usar um construtor aqui no ClienteController passando um RepositorioCliente e não deu certo :/
        //tem q colocar algum framewrok de injeção de dependencia.. no ml tava usando o ninject

        // eu vi comecei a usar aqui mas como não me adiantaria de muita coisa, deixei assim
        // entendo :) é nao adianta fazer um canhão pra matar uma formiga rs
        readonly RepositorioCliente _repositorio = new RepositorioCliente();

        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Inserir(ClienteViewModel cliente)
        {
            return null;
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var cliente = _repositorio.ObterPorId(id);
            return Json(new {cliente}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Listar()
        {
            var clientes = _repositorio.Obter();
            return Json(new {clientes}, JsonRequestBehavior.AllowGet);
        }
    }
}