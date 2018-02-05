using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeNotas.Web.Models
{
    public class AlunoViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Nome Completo")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        [StringLength(10)]
        public string Matricula { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Foto { get; set; }


        public string Telefone { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EMail { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeId { get; set; }
        
    }
}
