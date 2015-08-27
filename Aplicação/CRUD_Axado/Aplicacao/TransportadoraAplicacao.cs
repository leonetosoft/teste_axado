using Dominio;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao
{
    /// <summary>
    /// Metodos necessarios para SALVAR | EDITAR | VISUALIZAR transportadoras
    /// </summary>
    public class TransportadoraAplicacao
    {
        private Contexto contexto;

        /// <summary>
        /// Insere uma tranportadora
        /// </summary>
        /// <param name="t">Transportadora a ser inserida</param>
        private void Inserir(Transportadora t)
        {
            var strQuery = "";
            strQuery += "INSERT INTO transportadora (Nome, Telefone, Endereco, Email, InscricaoEstadual, Cnpj) ";
            strQuery += string.Format(" VALUES ('{0}','{1}','{2}', '{3}','{4}' ,'{5}') ",
                t.Nome, t.Telefone, t.Endereco, t.Email, t.InscricaoEstadual,t.Cnpj
             );

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        /// <summary>
        /// Alterar uma tranportadora
        /// </summary>
        /// <param name="t">Transportadora para edicao</param>
        private void Alterar(Transportadora t)
        {
            var strQuery = "";
            strQuery += " UPDATE transportadora SET ";
            strQuery += string.Format(" Nome = '{0}', ", t.Nome);
            strQuery += string.Format(" Telefone = '{0}', ", t.Telefone);
            strQuery += string.Format(" Endereco = '{0}', ", t.Endereco);
            strQuery += string.Format(" Email = '{0}', ", t.Email);
            strQuery += string.Format(" InscricaoEstadual = '{0}', ", t.InscricaoEstadual);
            strQuery += string.Format(" Cnpj = '{0}'", t.Cnpj);
            strQuery += string.Format(" WHERE Codigo = {0} ", t.Codigo);

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        /// <summary>
        /// Salvar transportadora
        /// </summary>
        /// <param name="t">transportadora</param>
        public void Salvar(Transportadora t)
        {
            if (t.Codigo > 0)
                Alterar(t);
            else
                Inserir(t);
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" DELETE FROM transportadora WHERE Codigo = {0}", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        /// <summary>
        /// Lista todas as transportadora
        /// </summary>
        /// <returns></returns>
        public List<Transportadora> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM transportadora ";
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader);
            }
        }

        public Transportadora ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" SELECT * FROM transportadora WHERE Codigo = {0} ", id);
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retornoDataReader).FirstOrDefault();
            }
        }

        /// <summary>
        /// Transporma em object
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<Transportadora> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var transportadoras = new List<Transportadora>();
            while (reader.Read())
            {
                var temObjeto = new Transportadora()
                {

                    Codigo = int.Parse(reader["Codigo"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Telefone = reader["Telefone"].ToString(),
                    Endereco = reader["Endereco"].ToString(),
                    Email = reader["Email"].ToString(),
                    InscricaoEstadual = Convert.ToInt32(reader["InscricaoEstadual"]),
                    Cnpj = reader["Cnpj"].ToString(),
                };

                UsuarioAuth user = new UsuarioAuth();
                temObjeto.Avaliacao = user.getAvaliacao(temObjeto.Codigo);
                transportadoras.Add(temObjeto);
            }
            reader.Close();
            return transportadoras;
        }

    }
}
