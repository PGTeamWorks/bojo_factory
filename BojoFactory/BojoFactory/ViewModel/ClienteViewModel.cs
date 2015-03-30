using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BojoFactory.ViewModel
{
    public class ClienteViewModel
    {
        public Int32 Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}