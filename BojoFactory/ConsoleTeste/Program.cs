using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestrutura.Contexto;
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
                _connection.Open();
                Console.WriteLine("ABRIU");

                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM tb_cliente",_connection);

               var retorno = command.ExecuteReader();

                Console.WriteLine(retorno.ToString());

                _connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
              
            }

        }
    }
}
