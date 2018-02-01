using System;
using System.Collections.Generic;
using System.Text;

namespace ControleJogos.Model
{
    public class EmprestimoJogo
    {
        public int JogoID { get; set; }
        public Jogo Jogo { get; set; }

        public int EmprestimoID { get; set; }
        public Emprestimo Emprestimo { get; set; }   
    }
}
