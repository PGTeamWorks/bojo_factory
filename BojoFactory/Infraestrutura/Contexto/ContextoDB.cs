using System.Configuration;
using Npgsql;

namespace Infraestrutura.Contexto
{
    public class ContextoDb
    {
        private NpgsqlConnection _connection;

        public void AbrirConexao()
        {
            _connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BojoFactoryConfig"].ConnectionString);
           _connection.Open();

        }
    }
}
