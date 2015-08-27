using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Helpers
{
    /// <summary>
    /// Esta classe foi criada para facilitar a implementação 
    /// Podendo esconder elementos do MvCSTRING
    /// EX:
    /// 
    ///      @Html.ActionLink("Editar", "Editar", new { id = item.Codigo }).If(true, true)
    ///          Saída Renderizada: Editar |
    ///          
    ///      @Html.ActionLink("Editar", "Editar", new { id = item.Codigo }).If(true, false)
    ///          Saída Renderizada:  Editar 
    ///          
    ///      @Html.ActionLink("Editar", "Editar", new { id = item.Codigo }).If(false, false)
    ///          Saída Renderizada: [não aparecerá nada]  
    /// </summary>
    public static class ActionLinkHelper
    {
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation, bool separator)
        {
            return evaluation ? separator ? new MvcHtmlString(value.ToString() + " |") : value : MvcHtmlString.Empty;
        }
    }
}
