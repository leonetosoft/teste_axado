using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    /// <summary>
    /// Métodos comums para conexão ao database
    /// </summary>
    public class Contexto : IDisposable
    {
        private readonly SqlConnection minhaConexao;

        public Contexto()
        {
            //Conectando ao banco de dados
            minhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["AXADO_Db"].ConnectionString);
            minhaConexao.Open();
        }

        /// <summary>
        /// Executa comandos sem retorno
        /// </summary>
        /// <param name="strQuery"></param>
        public void ExecutaComando(string strQuery)
        {
            var cmdComando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = minhaConexao
            };
            cmdComando.ExecuteNonQuery();
        }

        /// <summary>
        /// Executa com retorno
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public SqlDataReader ExecutaComandoComRetorno(string strQuery)
        {
            var cmdComando = new SqlCommand(strQuery, minhaConexao);
            return cmdComando.ExecuteReader();
        }

        /// <summary>
        /// Desliga conexao quando esta classe é apagada
        /// </summary>
        public void Dispose()
        {
            if (minhaConexao.State == ConnectionState.Open)
                minhaConexao.Close();
        }
    }
}
