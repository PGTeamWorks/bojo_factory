using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces;
using FluentNHibernate.Utils;
using Infraestrutura;
using Npgsql;

namespace Repositorio
{
    public class RepositorioMateriaPrima : BaseData, IRepositorioMateriaPrima
    {
        public MateriaPrima InsereAltera(MateriaPrima materia)
        {
            var operacao = materia.Id == 0 ? "I" : "U";

            try
            {
                var parametros = PreparaParamentros(materia);

                var query = string.Format("SELECT fn_materia_prima( :id_materia_prima, " +
                                          "                         :descricao, " +
                                          "                         :saldo_estoque, " +
                                          "                         :preco_custo, " +
                                          "                         :'{0}')",operacao);

                var datareader = ExecutarReader(query, parametros);
                return datareader.FillList<MateriaPrima>(ReaderParaObejto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {
                
                throw new Exception(exception.BaseMessage);
            }
        }

        public MateriaPrima ObterPorId(int id)
        {
            try
            {
                var query = string.Format("SELECT * " +
                                          "FROM vs_materia_prima " +
                                          "WHERE id_materia_prima = '{0}'", id);

                var dataReader = ExecutarReader(query);
                return dataReader.FillList<MateriaPrima>(ReaderParaObejto).FirstOrDefault();
            }
            catch (NpgsqlException exception)
            {
                throw new Exception(exception.BaseMessage);
            }
        }

        public IEnumerable<MateriaPrima> Obter()
        {
            try
            {
                var query = string.Format("SELECT * " +
                                      "FROM vs_materia_prima");
                var dataReader = ExecutarReader(query);
                return dataReader.FillList<MateriaPrima>(ReaderParaObejto);
            }
            catch (NpgsqlException exception)
            {
                
                throw new Exception(exception.BaseMessage);
            }
            
        }

        public List<NpgsqlParameter> PreparaParamentros(MateriaPrima materia)
        {
            var paramentros = new List<NpgsqlParameter>();

            paramentros.Add(materia.Id != 0
                ? new NpgsqlParameter("id_materia_prima", materia.Id)
                : new NpgsqlParameter("id_materia_prima", null));

            paramentros.Add(new NpgsqlParameter("descricao", materia.Descricao));
            paramentros.Add(new NpgsqlParameter("preco_custo", materia.PrecoCusto));
            paramentros.Add(new NpgsqlParameter("saldo_estoque", materia.SaldoEstoque));
            
            return paramentros;
        }

        public List<MateriaPrima> ReaderParaObejto(IDataReader reader)
        {
            var materias = new List<MateriaPrima>();

            while (reader.Read())
            {
                var materia = new MateriaPrima();
                materia.Id = GetSafeField<int>(reader["id_materia_prima"], 0);
                materia.Descricao = GetSafeField<string>(reader["descricao"], string.Empty);
                materia.PrecoCusto = GetSafeField<decimal>(reader["preco_custo"], 0);
                materia.SaldoEstoque = GetSafeField<decimal>(reader["saldo_estoque"], 0);

                materias.Add(materia);
            }

            return materias;
        }
    }

   
}
