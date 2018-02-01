using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ControleJogos.Model
{
    public class Emprestimo
    {
        [Key]
        public int EmprestimoID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de empréstimo")]
        public DateTime DataEmprestimo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data que pretende devolver")]
        public DateTime DataDevolucaoPrevista { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data da devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Display(Name = "Dias de atraso na devolução")]
        public int DiasAtraso
        {
            get
            {
                return DateTime.Now.Subtract(DataDevolucaoPrevista).Days;
            }
        }

        public int AmigoID { get; set; }

        public Amigo Amigo { get; set; }

        [Display(Name = "Jogos emprestados")]
        public ICollection<EmprestimoJogo> EmprestimoJogo { get; set; }
    }
}
