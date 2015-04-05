using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestrutura;
using NHibernate.Hql;
using Npgsql;

namespace Repositorio
{
    public class RepositorioProduto : BaseData, IRepositorioProduto 
    {
        public Produto ManipulaObjeto(Produto obj)
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
                                  "FROM fn_produto " +
                                  "(NULL, " +
                                  " :descricao, " +
                                  " :tamanho, " +
                                  " :cor, " +
                                  " :preco, " +
                                  " :saldo_estoque, " +
                                  "'{0}')",tipoOperacao);
                

              
                

                var dataReader = ExecutarReader(query, parametros);

                return dataReader.FillList<Produto>(ReaderParaObjeto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public Produto ObterPorId(int id)
        {
            try
            {
                
                var query = string.Format(" SELECT id_produto,   " +
                                          "        descricao,    " +
                                          "        cor,          " +
                                          "        tamanho,      " +
                                          "        saldo_estoque " +
                                          " FROM vs_produto      " +
                                          " WHERE {0}",          id);

                var dataReader = ExecutarReader(query);

                return dataReader.FillList<Produto>(ReaderParaObjeto).FirstOrDefault();
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

        public IEnumerable<Produto> Obter()
        {
            try
            {
                var query = string.Format(" SELECT id_produto,   " +
                                          "        descricao,    " +
                                          "        cor,          " +
                                          "        tamanho,      " +
                                          "        saldo_estoque " +
                                          " FROM vs_produto      ");

                var dataReader = ExecutarReader(query);

                return dataReader.FillList<Produto>(ReaderParaObjeto);
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

        public List<NpgsqlParameter> PreparParametros(Produto produto)
        {
            var parametros = new List<NpgsqlParameter>();

            parametros.Add(new NpgsqlParameter("descricao", produto.Descricao));
            parametros.Add(new NpgsqlParameter("cor", produto.Cor));
            parametros.Add(new NpgsqlParameter("tamanho", produto.Tamanho));
            parametros.Add(new NpgsqlParameter("preco", produto.Preco));
            parametros.Add(new NpgsqlParameter("saldo_estoque", produto.SaldoEstoque));

            return parametros;
        }

        public List<Produto> ReaderParaObjeto(IDataReader reader)
        {
            var listaProdudos = new List<Produto>();

            while (reader.Read())
            {
                var produto = new Produto();

                produto.Id = GetSafeField<int>(reader["id_produto"], 0);
                produto.Descricao = GetSafeField<string>(reader["descricao"], string.Empty);
                produto.Cor = GetSafeField<string>(reader["cor"], string.Empty);
                produto.Tamanho = GetSafeField<decimal>(reader["tamanho"], 0);
                produto.SaldoEstoque = GetSafeField<decimal>(reader["saldo_estoque"], 0);

                listaProdudos.Add(produto);
            }

            return listaProdudos;


        } 
    }
}
