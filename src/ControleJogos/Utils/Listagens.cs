using ControleJogos.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ControleJogos.Utils
{
    public class Listagens
    {
        private readonly ControleJogosContext _context;

        public Listagens(ControleJogosContext context)
        {
            this._context = context;
        }
        

        public List<CheckBoxItemList> JogosCheckBox()
        {
            var qry = from a in _context.Jogos.AsNoTracking()
                      orderby a.Titulo
                      select new CheckBoxItemList
                      {
                          Value = a.JogoID,
                          Text = a.Titulo
                      };

            return qry.ToList();
        }
    }
}
