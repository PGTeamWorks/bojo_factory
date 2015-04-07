using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura;
using Npgsql;

namespace Repositorio
{
    public class RepositorioProduto : BaseData, IRepositorioProduto
    {
        public Produto InsereAltera(Produto produto)
        {
            var operacao = produto.Id == 0 ? "I" : "U";

            try
            {
                var parametros = PreparaParamentros(produto);

                var query = string.Format("SELECT fn_produto( :id_produto, " +
                                          "                   :descricao, " +
                                          "                   :tamanho, " +
                                          "                   :cor, " +
                                          "                   :preco, " +
                                          "                   :saldo_estoque, " +
                                          "                   :'{0}')", operacao);

                var datareader = ExecutarReader(query, parametros);
                return datareader.FillList<Produto>(ReaderParaObejto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {

                throw new Exception(exception.BaseMessage);
            }
        }

        public Produto ObterPorId(int id)
        {
            try
            {
                var query = string.Format("SELECT * " +
                                          "FROM vs_produto " +
                                          "WHERE id_produto = '{0}'", id);

                var dataReader = ExecutarReader(query);
                return dataReader.FillList<Produto>(ReaderParaObejto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {
                throw new Exception(exception.BaseMessage);
            }
        }

        public IEnumerable<Produto> Obter()
        {
            try
            {
                var query = string.Format("SELECT * " +
                                      "FROM vs_produto");

                var dataReader = ExecutarReader(query);
                return dataReader.FillList<Produto>(ReaderParaObejto);
            }
            catch (NpgsqlException exception)
            {

                throw new Exception(exception.BaseMessage);
            }
        }

        public List<NpgsqlParameter> PreparaParamentros(Produto produto)
        {
            var paramentros = new List<NpgsqlParameter>();

            paramentros.Add(produto.Id != 0
                ? new NpgsqlParameter("id_produto", produto.Id)
                : new NpgsqlParameter("id_produto", null));

            paramentros.Add(new NpgsqlParameter("descricao", produto.Descricao));
            paramentros.Add(new NpgsqlParameter("tamanho", produto.Tamanho));
            paramentros.Add(new NpgsqlParameter("cor",produto.Cor));
            paramentros.Add(new NpgsqlParameter("preco", produto.Preco));
            paramentros.Add(new NpgsqlParameter("saldo_estoque", produto.SaldoEstoque));

            return paramentros;
        }

        public List<Produto> ReaderParaObejto(IDataReader reader)
        {
            var produtos = new List<Produto>();

            while (reader.Read())
            {
                var produto = new Produto();
                produto.Id = GetSafeField<int>(reader["id_produto"], 0);
                produto.Descricao = GetSafeField<string>(reader["descricao"], string.Empty);
                produto.Tamanho = GetSafeField<decimal>(reader["tamanho"], 0);
                produto.Cor = GetSafeField<string>(reader["cor"], 0);
                produto.Preco = GetSafeField<decimal>(reader["preco"], 0);
                produto.SaldoEstoque = GetSafeField<decimal>(reader["saldo_estoque"], 0);

                produtos.Add(produto);
            }

            return produtos;
        }
    }
}
