using GerenciadorDeNotas.Entidades;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public List<Avaliacao> Avaliacoes { get; set; }

        public decimal Media
        {
            get
            {
               return  Avaliacoes != null &&
                       Avaliacoes.Any()
                       ?
                            Avaliacoes.Select(avaliacao => avaliacao.Nota).Sum() 
                       : 0;
            }
            set { }
        }

        public bool Aprovado
        {

            get
            {

                return Media > 28;
            }

            set { }
        }

        public bool Reprovado
        {
            get
            {
                return Media + (Avaliacoes != null && Avaliacoes.Any() ? (4 - Avaliacoes.Count) : 4) * 10 < 28;
            }

            set { }
        }


    }
}
