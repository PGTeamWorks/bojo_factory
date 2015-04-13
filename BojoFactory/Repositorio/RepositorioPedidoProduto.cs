using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Infraestrutura;
using Dominio.Entidades;
using Npgsql;

namespace Repositorio
{
    public class RepositorioPedidoProduto : BaseData
    {
        public PedidoProduto InsereAltera(PedidoProduto pedidoProduto)
        {
            var operacao = pedidoProduto.Id == 0 ? "I" : "U";

            try
            {
                var parametros = PreparaParamentros(pedidoProduto);

                var query = string.Format("SELECT * FROM fn_pedido_produto( :id_pedido_produto," +
                                          "                                 :id_pedido," +
                                          "                                 :id_produto," +
                                          "                                 :quantidade," +
                                          "                                 :valor_unitario,"+
                                          "                                 '{0}')", operacao);

                var datareader = ExecutarReader(query, parametros);
                return datareader.FillList<PedidoProduto>(ReaderParaObejto).FirstOrDefault();
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

        public PedidoProduto ObterPorId(int id)
        {
            try
            {
                var query = string.Format("SELECT * " +
                                          "FROM tb_pedido_produto " +
                                          "WHERE id_pedido_produto = '{0}'", id);

                var dataReader = ExecutarReader(query);
                return dataReader.FillList<PedidoProduto>(ReaderParaObejto).FirstOrDefault();
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

        public IEnumerable<PedidoProduto> Obter()
        {
            try
            {
                var query = string.Format(" SELECT * " +
                                          " FROM tb_pedido_produto");
                var dataReader = ExecutarReader(query);
                return dataReader.FillList<PedidoProduto>(ReaderParaObejto);
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

        public List<NpgsqlParameter> PreparaParamentros(PedidoProduto pedidoProduto)
        {
            var paramentros = new List<NpgsqlParameter>();

            paramentros.Add(pedidoProduto.Id != 0
                ? new NpgsqlParameter("id_pedido_produto", pedidoProduto.Id)
                : new NpgsqlParameter("id_pedido_produto", null));

            paramentros.Add(new NpgsqlParameter("id_pedido", pedidoProduto.IdPedido));
            paramentros.Add(new NpgsqlParameter("id_produto", pedidoProduto.IdProduto));
            paramentros.Add(new NpgsqlParameter("quantidade", pedidoProduto.Quantidade));
            paramentros.Add(new NpgsqlParameter("valor_unitario", pedidoProduto.Preco));

            return paramentros;
        }

        public List<PedidoProduto> ReaderParaObejto(IDataReader reader)
        {
            var produtosPedidos = new List<PedidoProduto>();

            while (reader.Read())
            {
                var produtoPedido = new PedidoProduto();
                produtoPedido.Id = GetSafeField<int>(reader["id_pedido_produto"], 0);
                produtoPedido.IdPedido = GetSafeField<int>(reader["id_pedido"], 0);
                produtoPedido.IdProduto = GetSafeField<int>(reader["id_produto"], 0);
                produtoPedido.Quantidade = GetSafeField<int>(reader["quantidade"], 0);
                produtoPedido.Preco = GetSafeField<decimal>(reader["valor_unitario"], 0);

                produtosPedidos.Add(produtoPedido);
            }

            return produtosPedidos;
        }
    }
}
