//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Reflection;
//using Npgsql;

//namespace Infraestrutura.Contexto
//{
//    public class ContextoDb
//    {
//        private NpgsqlConnection _connection;

//        public ContextoDb()
//        {
//            _connection = _connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["BojoFactoryConfig"].ConnectionString);
//        }

//        public void AbrirConexao()
//        {
//            if (_connection.State != ConnectionState.Open)
//                _connection.Open();
//        }

//        public void FecharConexao()
//        {
//            _connection.Close();
//        }

//        public NpgsqlDataReader ExecutaFunctionComRetorno(string query, List<NpgsqlParameter> parametros)
//        {
//            var command = new NpgsqlCommand(query, _connection);

//            try
//            {
//                AbrirConexao();

//                foreach (var parametro in parametros)
//                {
//                    command.Parameters.Add(parametro);
//                }

//                return command.ExecuteReader();

//            }
//            catch (Exception exception)
//            {
//                throw new Exception(exception.Message);
//            }
//            finally
//            {
//                FecharConexao();
//            }

//        }

//        public NpgsqlDataReader ExecutaFunctionComRetorno(string query)
//        {
//            var command = new NpgsqlCommand(query, _connection);

//            try
//            {
//                AbrirConexao();
//                //command.Connection = _connection;
//                return command.ExecuteReader();

//            }
//            catch (Exception exception)
//            {
//                throw new Exception(exception.Message);
//            }
//            finally
//            {
//                //FecharConexao();
//            }

//        }

//    }
//}
