using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca_core_att.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public int Idade { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Entrega> Entregas { get; set; }

    }
}
