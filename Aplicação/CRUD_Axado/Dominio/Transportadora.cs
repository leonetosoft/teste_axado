using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dominio
{
    /// <summary>
    /// Model transportadora
    /// </summary>
    public class Transportadora
    {
        public AVALIACAO_TIPO Avaliacao { get; set; }

        public Transportadora()
        {
            Avaliacao = AVALIACAO_TIPO.Nenhuma;
        }

        public int Codigo { get; set; }
        //declaracao dos atributos da classe

        [Required]
        [DisplayName("Nome")]
        public string Nome {get; set;}

        [Required]
        [DisplayName("Cnpj")]
        public string Cnpj {get; set;}

        [Required]
        [DisplayName("Telefone")]
        public string Telefone {get; set;}

        [Required]
        [DisplayName("Endereço")]
        public string Endereco {get; set;}

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Inscricao Estadual")]
        public int InscricaoEstadual {get; set;}
    }
}
