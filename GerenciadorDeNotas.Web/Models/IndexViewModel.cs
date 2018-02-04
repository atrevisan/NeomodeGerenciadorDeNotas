using System.Collections.Generic;

namespace GerenciadorDeNotas.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<AlunoViewModel> Items { get; set; }
        public Pager Pager { get; set; }
    }
}
