using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura;
using Npgsql;

namespace Repositorio
{
    public class RepositorioCliente : BaseData, IRepositorioCliente
    {
        public Cliente InsereAltera(Cliente cliente)
        {
            var paramretos = PreparaParamentros(cliente);

            var operacao = cliente.Id == 0 ? "I" : "U";

            try
            {
                var query =
                    string.Format("SELECT *                             " +
                                  "FROM fn_cliente (:id_cliente,        " +
                                  "                 :nome,              " +
                                  "                 :cpf,               " +
                                  "                 :email,             " +
                                  "                 :telefone,          " +
                                  "                 :data_nascimento,   " +
                                  "                 '{0}')"             ,operacao);

                var dataReader = ExecutarReader(query, paramretos);

                return dataReader.FillList<Cliente>(ReaderParaObejto).FirstOrDefault();

            }
            catch (NpgsqlException exception)
            {

                throw new Exception(exception.BaseMessage);
            }
            finally
            {
                base.FecharConexao();
            }
        }

        public Cliente Deleta(int id)
        {
            try
            {
                var query = string.Format(" SELECT fn_cliente ('{0}', " +
                                          "                     NULL, " +
                                          "                     NULL, " +
                                          "                     NULL, " +
                                          "                     NULL, " +
                                          "                     NULL, " +
                                          "                     'D')",id);

                var dataReader = ExecutarReader(query);

                return dataReader.FillList<Cliente>(ReaderParaObejto).FirstOrDefault();

            }
            catch (NpgsqlException exception)
            {
                
                throw new Exception(exception.BaseMessage);
            }
        }

        public Cliente ObterPorId(int id)
        {
            try
            {
                var query = string.Format("SELECT id_cliente,       " +
                                          "       nome,             " +
                                          "       cpf,              " +
                                          "       email,            " +
                                          "       telefone,         " +
                                          "       data_nascimento   " +
                                          "FROM vs_cliente          " +
                                          "WHERE id_cliente = '{0}' ",id);

                var dataReader = ExecutarReader(query);

                return dataReader.FillList<Cliente>(ReaderParaObejto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {
                
                throw new Exception(exception.BaseMessage);
            }
        }

        public IEnumerable<Cliente> Obter()
        {
            try
            {
                var query = string.Format("SELECT * FROM vs_cliente");

                var dataReader = ExecutarReader(query);

               
                return dataReader.FillList<Cliente>(ReaderParaObejto);


            }
            catch (NpgsqlException exception)
            {

                throw new Exception(exception.BaseMessage);
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<NpgsqlParameter> PreparaParamentros(Cliente cliente)
        {
            var paramentros = new List<NpgsqlParameter>();

            paramentros.Add(cliente.Id != 0
                ? new NpgsqlParameter("id_cliente", cliente.Id)
                : new NpgsqlParameter("id_cliente", null));

            paramentros.Add(new NpgsqlParameter("nome", cliente.Nome));
            paramentros.Add(new NpgsqlParameter("cpf", cliente.Cpf));
            paramentros.Add(new NpgsqlParameter("email", cliente.Email));
            paramentros.Add(new NpgsqlParameter("telefone", cliente.Telefone));
            paramentros.Add(new NpgsqlParameter("data_nascimento", cliente.DataNascimento));

            return paramentros;
        }

        //no caso aqui, o metodo retorna um List<Cliente> (que é um object (tudo é object em C# (nao tenho cts dos tipos primitivoss))
        // e ele tem um parametro IDataReader que é o mesmo que o delegate
        //Entendeu a parte de criar o delegate e um método que atende o delegate?
        // acho que sim
        //blz..
        public List<Cliente> ReaderParaObejto(IDataReader reader)
        {
            var clientes = new List<Cliente>();

            while (reader.Read())
            {
                var cliente = new Cliente();
                cliente.Id = GetSafeField<int>(reader["id_cliente"], 0);
                cliente.Nome = GetSafeField<string>(reader["nome"], string.Empty);
                cliente.Cpf = GetSafeField<string>(reader["cpf"], string.Empty);
                cliente.Email = GetSafeField<string>(reader["email"], string.Empty);
                cliente.Telefone = GetSafeField<string>(reader["telefone"], string.Empty);
                cliente.DataNascimento =
                    Convert.ToDateTime(
                        GetSafeField<string>(reader["data_nascimento"].ToString(), DateTime.MinValue.ToString()),
                        CultureInfo.CurrentCulture.DateTimeFormat);

                clientes.Add(cliente);
            }

            return clientes;
        }

    }
}
