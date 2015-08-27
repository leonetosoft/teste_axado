using Dominio;
using Helpers;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Aplicacao
{
    /// <summary>
    /// Classe que cuida dos métodos do usuário., login e segurança
    /// </summary>
    public class UsuarioAuth
    {

        private Contexto contexto;

       
        /// <summary>
        /// Checar se o usuario e senha existem na database
        /// </summary>
        /// <param name="_username">Nome de usuário</param>
        /// <param name="_password">Senha do usuário</param>
        /// <returns>True se o usuario existe e a password está correta</returns>
        public bool AutenticaUsuario(string _username, string _password)
        {
            string strQuery = @"SELECT Username FROM usuario WHERE ";
            strQuery += string.Format("Username = '{0}' AND Password = '{1}'", _username, SHA1.Encode(_password));

            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);
                if (retornoDataReader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(_username, false);
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// Coleta as permissoes do usuario logado
        /// </summary>
        /// <returns> List<Strin lista </returns>
        public List<String> getPermissoesDoUsuario()
        {
            Usuario user = this.getUsuarioLogado();

            if (user == null) return null;


            string strQuery = @"SELECT Nome FROM permissao ";
            strQuery += string.Format(" WHERE Usuario_id = '{0}'", user.Id);
    
            List<String> lista = new List<String>();
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);

                while (retornoDataReader.Read())
                {
                    lista.Add(retornoDataReader["Nome"].ToString());
                }
            }

            return lista;
        }

        /// <summary>
        /// Desloga do FormsAuth
        /// </summary>
        public void deslogar()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Avalia transportadora 
        /// </summary>
        /// <param name="t">Transportadora</param>
        /// <param name="avaliacao">Avaliacao</param>
        public void avaliarTransportadora(Transportadora t, int avaliacao)
        {
            var usuario = this.getUsuarioLogado();

            var strQuery = "";
            strQuery += "INSERT INTO usuario_avaliacao (Usuario_id, Transportadora_id, Avaliacao) ";
            strQuery += string.Format(" VALUES ('{0}','{1}','{2}') ",
                usuario.Id, t.Codigo, avaliacao
             );

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        /// <summary>
        /// Consulta avaliações deste usuario na empresa setada em TransportadoraId
        /// </summary>
        /// <param name="TransportadoraId"></param>
        /// <returns>Enum do tipo de avaliação: AVALIACAO_TIPO</returns>
        public AVALIACAO_TIPO getAvaliacao(int TransportadoraId)
        {
            AVALIACAO_TIPO avaliacao = AVALIACAO_TIPO.Nenhuma;
            var usuario = this.getUsuarioLogado();

            if (usuario == null) return avaliacao;

            string strQuery = @"SELECT Avaliacao FROM usuario_avaliacao WHERE ";
            strQuery += string.Format("Usuario_id = '{0}' AND Transportadora_id = '{1}'", usuario.Id, TransportadoraId);

            
            using (contexto = new Contexto())
            {
                  var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);

                  while (retornoDataReader.Read())
                  {
                      int av = Convert.ToInt32(retornoDataReader["Avaliacao"]);
                      avaliacao = (AVALIACAO_TIPO)av;
                  }
            }


            return avaliacao;
        }

        /// <summary>
        /// Coleta o usuário logado
        /// </summary>
        /// <returns></returns>
        public Usuario getUsuarioLogado()
        {
            string login = HttpContext.Current.User.Identity.Name;

            if (login == null)
            {
                return null;
            }

            string strQuery = @"SELECT Id, Username, Email, RegDate FROM usuario WHERE ";
            strQuery += string.Format("Username = '{0}'", login);

            Usuario user = null;
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.ExecutaComandoComRetorno(strQuery);

                while (retornoDataReader.Read())
                {
                    user = new Usuario();
                    user.Id = Convert.ToInt32(retornoDataReader["Id"]);
                    user.Email = retornoDataReader["Email"].ToString();
                    user.UserName = retornoDataReader["Username"].ToString();
                    int colIndex = retornoDataReader.GetOrdinal("RegDate");
                    user.RegDate = retornoDataReader.GetDateTime(colIndex);
                }
            }

            return user;
        }

    }

}
