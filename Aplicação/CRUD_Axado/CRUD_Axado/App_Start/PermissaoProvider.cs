using Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_Axado.App_Start
{
    /// <summary>
    /// Sobrescrevendo método GetRolesForUser apra coletar apenas as Permissoes do usuario logado
    /// </summary>
    public class PermissaoProvider : System.Web.Security.RoleProvider
    {

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            UsuarioAuth auth = new UsuarioAuth();

            List<String> listaPermissoes = auth.getPermissoesDoUsuario();

            if (listaPermissoes == null)
            {
                return new string[] { };
            }

            return listaPermissoes.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}