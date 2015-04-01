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
        public Cliente ManipulaObjeto(Cliente cliente)
        {
            var paramretos = PreparaParamentros(cliente);

            try
            {
                var query =
                    string.Format("SELECT * FROM fn_cliente (NULL,:nome,:cpf,:email,:telefone,:data_nascimento,'I')");

                var dataReader = ExecutarReader(query, paramretos);

                return dataReader.FillList<Cliente>(ReaderParaObejto).FirstOrDefault();

            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                base.FecharConexao();
            }


        }

        public Cliente ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> Obter()
        {
            try
            {
                var query = string.Format("SELECT * FROM vs_cliente");

                var dataReader = ExecutarReader(query);

                // a parte de extention vc entendeu? pra dar um datareader.Fill?
                //não
                //tipo, vc pode fazer um extention pra qualquer tipo de objeto, ae vc da o objeto.OMetodoQueVcCriou
                //as não precisa chamar OMetodoQueVcCriou(seuObjeto);

                //aqui to passando o método que atende o delegate pro FillList la do meu extention
                //uma pergunta..  a parte de generics vc manja? List<T>
                //e essas coisas?
                // Sim... tipo um T pode ser qualquer coisa 
                // sim object sim... só q em vez de retornar o object retorna o tipo T
                //blz... passou la pro extention
                return dataReader.FillList<Cliente>(ReaderParaObejto);


            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<NpgsqlParameter> PreparaParamentros(Cliente cliente)
        {
            var paramentros = new List<NpgsqlParameter>();

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
