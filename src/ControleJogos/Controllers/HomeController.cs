using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControleJogos.Data.Context;
using ControleJogos.Models;
using System.Diagnostics;
using System.Linq;

namespace ControleJogos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ControleJogosContext _context;

        public HomeController(ControleJogosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var controleJogosContext = _context.Emprestimo
                .Where(p=>!p.DataDevolucao.HasValue)
                .Include(e => e.Amigo)
                .OrderByDescending(p=>p.DiasAtraso);

            return View(await controleJogosContext.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
