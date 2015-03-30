using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Microsoft.SqlServer.Server;
using Npgsql;

namespace ConsoleTeste
{
    class Program 
    {
        
        static void Main(string[] args)
        {
             NpgsqlConnection _connection;

             string conexao = string.Format("Server=localhost; Port=5432;User Id={0}; Password={1}; Database=bojo_factory;", "bojofactory", "bojofactory");
            _connection = new NpgsqlConnection(conexao);

            try
            {
                Cliente cliente =  new Cliente();
                cliente.Nome = "Fulano";
                cliente.Cpf = "1234567891011";
                cliente.DataNascimento = DateTime.Now;
                cliente.Email = "fulano@bojo.com.br";
                cliente.Telefone = "9999-9999";

                _connection.Open();
                
                var query = string.Format("SELECT * FROM fn_cliente (NULL,'{0}','{1}','{2}','{3}','{4}','I')"
                    , cliente.Nome, cliente.Cpf, cliente.Email, cliente.Telefone,cliente.DataNascimento);

                NpgsqlCommand command = new NpgsqlCommand(query, _connection);

                Object result = command.ExecuteReader();

                Console.WriteLine(result);

               // NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM tb_cliente",_connection);

               //var retorno = command.ExecuteReader();

               // Console.WriteLine(retorno);

                _connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
              
            }

          }

        public void FormataData(DateTime data)
        {
           
        }
        
    }
}
