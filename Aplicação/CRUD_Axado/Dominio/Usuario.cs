using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    /// <summary>
    /// Classe do usuario
    /// </summary>
    public class Usuario 
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Lembrar senha")]
        public bool RememberMe { get; set; }

        [Display(Name = "Email")]
        public DateTime RegDate { get; set; }
    }
}
