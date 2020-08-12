using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca_core_att.Models
{
    public class Livro
    {
        [Key]
        public int LivroID { get; set; }

        public string Nome { get; set; }

        public string Autor { get; set; }

        public DateTime Lancamento { get; set; }

        public virtual ICollection<Entrega> Entregas { get; set; }
    }
}
