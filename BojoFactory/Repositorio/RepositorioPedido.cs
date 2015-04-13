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
    public class RepositorioPedido : BaseData, IRepositorioPedido
    {
        public Pedido InsereAltera(Pedido pedido)
        {
            var operacao = pedido.Id == 0 ? "I" : "U";

            try
            {
                var parametros = PreparaParamentros(pedido);

                var query = string.Format("SELECT * FROM fn_pedido( :id_pedido," +
                                          "                  :data_pedido," +
                                          "                  :valor_total," +
                                          "                  :id_cliente," +
                                          "                  '{0}')", operacao);

                var datareader = ExecutarReader(query, parametros);
                return datareader.FillList<Pedido>(ReaderParaObejto).FirstOrDefault();
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

        public Pedido ObterPorId(int id)
        {
            try
            {
                var query = string.Format("SELECT * " +
                                          "FROM tb_pedido " +
                                          "WHERE id_pedido = '{0}'", id);

                var dataReader = ExecutarReader(query);
                return dataReader.FillList<Pedido>(ReaderParaObejto).FirstOrDefault();
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

        public IEnumerable<Pedido> Obter()
        {
            try
            {
                var query = string.Format(" SELECT * " +
                                          " FROM tb_pedido");
                var dataReader = ExecutarReader(query);
                return dataReader.FillList<Pedido>(ReaderParaObejto);
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

        public List<NpgsqlParameter> PreparaParamentros(Pedido pedido)
        {
            var paramentros = new List<NpgsqlParameter>();

            paramentros.Add(pedido.Id != 0
                ? new NpgsqlParameter("id_pedido", pedido.Id)
                : new NpgsqlParameter("id_pedido", null));

            paramentros.Add(new NpgsqlParameter("data_pedido", pedido.DataPedido));
            paramentros.Add(new NpgsqlParameter("valor_total", pedido.ValorTotal));
            paramentros.Add(new NpgsqlParameter("id_cliente", pedido.IdCliente));

            return paramentros;
        }

        public List<Pedido> ReaderParaObejto(IDataReader reader)
        {
            var produtos = new List<Pedido>();

            while (reader.Read())
            {
                var produto = new Pedido();
                produto.Id = GetSafeField<int>(reader["id_pedido"], 0);
                produto.DataPedido = GetSafeField<DateTime>(reader["data_pedido"], DateTime.MinValue); //Convert.ToDateTime(GetSafeField<string>(reader["data_pedido"].ToString(), DateTime.MinValue.ToString()), CultureInfo.CurrentCulture.DateTimeFormat);
                produto.ValorTotal = GetSafeField<decimal>(reader["valor_total"], 0);
                produto.IdCliente = GetSafeField<int>(reader["id_cliente"], 0);

                produtos.Add(produto);
            }

            return produtos;
        }
    }
}
