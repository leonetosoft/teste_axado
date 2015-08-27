using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Axado.App_Start
{
    public class PermissaoFiltro : AuthorizeAttribute
    {
        /// <summary>
        /// Caso nao tiver autorizado pelas permissoes redireciona para: Home/Negado (Mensagem de acesso negado)
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);


            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Negado");
            }

        }
    }
}