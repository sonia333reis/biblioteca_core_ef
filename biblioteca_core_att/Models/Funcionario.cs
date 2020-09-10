using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace biblioteca_core_att.Models
{
    public class Funcionario
    {
        [Key]
        public int FuncionarioID { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public int Idade { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
