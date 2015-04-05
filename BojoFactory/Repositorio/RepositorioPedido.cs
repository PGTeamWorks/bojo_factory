using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura;
using Npgsql;

namespace Repositorio
{
    public class RepositorioPedido : BaseData, IRepositorioPedido
    {


        public Pedido ManipulaObjeto(Pedido obj)
        {
            var parametros = PreparParametros(obj);

            try
            {
                var tipoOperacao = "";

                if (obj.Id == 0)
                    tipoOperacao = "I";
                if (obj.Id != 0)
                    tipoOperacao = "U";

                var query =
                    string.Format("SELECT * " +
                                  "FROM fn_pedido " +
                                  "(NULL, " +
                                  " :data_pedido, " +
                                  " :valor_total, " +
                                  " :id_cliente " +
                                  " '{0}')", tipoOperacao);

                var dataReader = ExecutarReader(query, parametros);

                return dataReader.FillList<Pedido>(ReaderParaObjeto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public Pedido ObterPorId(int id)
        {
            try
            {

                var query = string.Format(" SELECT id_pedido,    " +
                                          "        data_pedido,  " +
                                          "        valor_total," +
                                          "        id_cliente  " +
                                          " FROM vs_pedido       " +
                                          " WHERE {0}", id);

                var dataReader = ExecutarReader(query);

                return dataReader.FillList<Pedido>(ReaderParaObjeto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        public IEnumerable<Pedido> Obter()
        {
            try
            {
                var query = string.Format(" SELECT id_pedido,   " +
                                          "        data_pedido, " +
                                          "        valor_total," +
                                          "        id_cliente " +
                                          " FROM vs_produto      ");

                var dataReader = ExecutarReader(query);

                return dataReader.FillList<Pedido>(ReaderParaObjeto);
            }
            catch (NpgsqlException exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<NpgsqlParameter> PreparParametros(Pedido pedido)
        {
            var parametros = new List<NpgsqlParameter>();

            parametros.Add(new NpgsqlParameter("data_pedido", pedido.DataPedido));
            parametros.Add(new NpgsqlParameter("valor_total", pedido.ValorTotal));
            parametros.Add(new NpgsqlParameter("id_cliente", pedido.Cliente.Id));

            return parametros;
        }

        public List<Pedido> ReaderParaObjeto(IDataReader reader)
        {
            var listaPedidos = new List<Pedido>();

            while (reader.Read())
            {
                var pedido = new Pedido();

                pedido.Id = GetSafeField<int>(reader["id_pedido"], 0);
                pedido.ValorTotal = GetSafeField<decimal>(reader["valor_total"], string.Empty);
                pedido.DataPedido =
                    Convert.ToDateTime(
                        GetSafeField<string>(reader["data_pedido"].ToString(), DateTime.MinValue.ToString()), 
                           CultureInfo.CurrentCulture.DateTimeFormat);
                pedido.Cliente.Id = GetSafeField<int>(reader["id_cliente"], 0);

                listaPedidos.Add(pedido);
            }

            return listaPedidos;


        }
    }
}
