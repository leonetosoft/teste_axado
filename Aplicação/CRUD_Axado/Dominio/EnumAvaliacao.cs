using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Enum para armazenamento da AVALIACAO_TIPO
    /// </summary>
    [Flags]
    public enum AVALIACAO_TIPO
    {
         [Description("Boa")]
        Boa = 3,
         [Description("Média")]
        Media = 2,
         [Description("Ruim")]
        Ruim = 1,
         [Description("Péssima")]
        Pessima = 0,
         [Description("Nenhuma")]
        Nenhuma = -1
    }
}
