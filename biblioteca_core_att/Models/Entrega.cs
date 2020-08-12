using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca_core_att.Models
{
    public class Entrega
    {
        [Key]
        public int EntregaID { get; set; }

        public int UsuarioID { get; set; }
        public virtual Usuario Usuarios { get; set; }

        public int LivroID { get; set; }
        public virtual Livro Livros { get; set; }
    }
}
