using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Infraestrutura
{
    public abstract class BaseData : IDisposable
    {
        //esse é o delegate, qualquer método que eu criar, que retornar o tipo do delegate (no caso object) e tenha 
        //parametros iguais ao delegate, atende o delegate
        public delegate object Fill(IDataReader reader);
        public static List<T> MontaList<T>(Fill fill, IDataReader reader)
        {
            //aqui ele executa o método fill (ReaderParaObejto) passando o reader pra ele e faz um cast pro tipo List do T que vc 
            //passou, no caso um Cliente
            //na vdd, pra esse uso, é bem inutil msm, vc poderia chamar o ReaderParaObejto(reader) la no RepositorioCliente msm
            //delegates são mt usados pra eventos
            //tem bastante em sistemas de processamento, ou sistemas webforms
            //onde vc poderia setar um evento dinamicamente
            //tipo vc criava um TextBox no windowsforms
            //ae vc data um txtNome.OnTextChange += (colocava um método que atendesse o delegate desse evento)
            //ae qdo ocorre o evento, ele chama o método, ok
            return (List<T>)fill(reader);
        }
        public static T MontaEntidade<T>(Fill fill, IDataReader reader)
        {
            return (T)fill(reader);
        }

        protected BaseData()
        {
            InicializaConexao("BojoFactoryConfig");
        }

        private NpgsqlConnection _connection = null;
        private string _connectionName = string.Empty;
        private int _connectionTimeout = 0;
        private NpgsqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    if (string.IsNullOrEmpty(_connectionName))
                        throw new Exception("Não foi inicializado o nome da conexão");
                    try
                    {
                        var connString =
                            System.Configuration.ConfigurationManager.ConnectionStrings[_connectionName]
                                .ConnectionString;

                        //Não existe timeout no postgress =(
                        //if (connString.IndexOf("Connection Timeout", System.StringComparison.Ordinal) == -1)
                        //    connString += string.Format(";Connection Timeout={0}", _connectionTimeout);
                        _connection = new NpgsqlConnection(connString);
                    }
                    catch (NpgsqlException exception)
                    {
                        throw new Exception(exception.BaseMessage);
                    }
                }
                return _connection;
            }
        }

        protected bool InicializaConexao(string connectionName, int timeout = 15)
        {
            _connectionName = connectionName;
            _connectionTimeout = timeout;
            return Connection != null;
        }

        protected NpgsqlDataReader  ExecutarReader(string comando, List<NpgsqlParameter> parametros = null)
        {
            try
            {
                AbrirConexao();
                NpgsqlCommand comand = CreateCommand(comando);
                if (parametros != null)
                {
                    comand.Parameters.AddRange(parametros.ToArray());
                }
                comand.CommandType = CommandType.Text;
                return comand.ExecuteReader();
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        protected T GetSafeField<T>(object campoReader, object valorNulo)
        {
            if (campoReader is DBNull)
            {
                return (T)valorNulo;
            }
            else
            {
                return (T)campoReader;
            }

        }

        protected NpgsqlCommand CreateCommand(string comandoSql)
        {
            return new NpgsqlCommand(comandoSql, Connection);
        }
        protected void AbrirConexao()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }
        protected void FecharConexao()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
        protected int ExecutarNonQuery(string comandoSql, List<NpgsqlParameter> parametros = null)
        {
            int retorno = 0;
            try
            {
                AbrirConexao();
                var command = CreateCommand(comandoSql);
                if (parametros != null)
                    command.Parameters.AddRange(parametros.ToArray());

                retorno = command.ExecuteNonQuery();
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(ex.BaseMessage);
            }
           
            return retorno;
        }
        protected int ExecutarScalar(string comandoSql, List<NpgsqlParameter> parametros = null)
        {
            int retorno = 0;
            try
            {
                AbrirConexao();
                NpgsqlCommand comando = CreateCommand(comandoSql);
                if (parametros != null)
                    comando.Parameters.AddRange(parametros.ToArray());
                retorno = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return retorno;
        }

        //ah, vc pode usar num using :)
        public void Dispose()
        {
            try
            {
                FecharConexao();
                _connection.Dispose();
            }
            catch { }
        }
        
    }

    public static class ExtensionMethods
    {
        public static void AddWithValue(this List<NpgsqlParameter> parameters, string parameterName, object value)
        {
            parameters.Add(new NpgsqlParameter(parameterName, value));
        }

        public static T Fill<T>(this IDataReader reader, BaseData.Fill fill)
        {
            return BaseData.MontaEntidade<T>(fill, reader);
        }

        //Fill é um delegate que criei la em cima
        public static List<T> FillList<T>(this  IDataReader reader, BaseData.Fill fill)
        {
            //aqui ele chama o montaList passando o reader e o método que ele vai processar
            return BaseData.MontaList<T>(fill, reader);
        }
    }
    
}
