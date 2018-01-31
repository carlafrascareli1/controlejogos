using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleJogos.Model
{
    public class Amigo
    {
        [Key]
        public int AmigoID { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
        public string Email { get; set; }

        //public virtual ICollection<Emprestimo> Emprestimo { get; set; }

        //public ICollection<SistemaUsuario> SistemaUsuario { get; set; }
    }
}
