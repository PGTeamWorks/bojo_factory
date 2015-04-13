using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Infraestrutura;
using Npgsql;

namespace Repositorio
{
    public class RepositorioFormula : BaseData
    {
        public int InsereAltera(Formula formula)
        {
            var operacao = formula.Id == 0 ? "I" : "U";

            try
            {
                var parametros = PreparaParamentros(formula);

                var query = string.Format("SELECT * FROM fn_formula( :id_formula," +
                                          "                          :id_produto," +
                                          "                          :id_materia_prima," +
                                          "                          :quantidade," +
                                          "                          '{0}')", operacao);

                var datareader = ExecutarNonQuery(query, parametros);
                return datareader;
            }
            catch (NpgsqlException exception)
            {

                throw new Exception(exception.BaseMessage);
            }
        }

        public List<NpgsqlParameter> PreparaParamentros(Formula formula)
        {
            var paramentros = new List<NpgsqlParameter>();

            paramentros.Add(formula.Id != 0
                ? new NpgsqlParameter("id_formula", formula.Id)
                : new NpgsqlParameter("id_formula", null));

            paramentros.Add(new NpgsqlParameter("id_formula", formula.Id));
            paramentros.Add(new NpgsqlParameter("id_produto", formula.IdProduto));
            paramentros.Add(new NpgsqlParameter("id_materia_prima", formula.IdMateriaPrima));
            paramentros.Add(new NpgsqlParameter("quantidade", formula.Quantidade));

            return paramentros;
        }

        public List<Formula> ReaderParaObejto(IDataReader reader)
        {
            var formulas = new List<Formula>();

            while (reader.Read())
            {
                var formula = new Formula();
                formula.Id = GetSafeField<int>(reader["id_formula"], 0);
                formula.IdProduto = GetSafeField<int>(reader["id_produto"], 0);
                formula.IdMateriaPrima = GetSafeField<int>(reader["id_materia_prima"], 0);
                formula.Quantidade = GetSafeField<int>(reader["quantidade"], 0);

                formulas.Add(formula);
            }
            return formulas;
        }
    }
}
