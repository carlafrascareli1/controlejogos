using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleJogos.Data.Context;
using ControleJogos.Model;
using ControleJogos.Utils;

namespace ControleJogos.Controllers
{
    public class EmprestimosController : Controller
    {
        private readonly ControleJogosContext _context;

        public EmprestimosController(ControleJogosContext context)
        {
            _context = context;
        }

        // GET: Emprestimos
        public async Task<IActionResult> Index()
        {
            var controleJogosContext = _context.Emprestimo.Include(e => e.Amigo);
            return View(await controleJogosContext.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Amigo)
                .Include(e => e.EmprestimoJogo)
                .ThenInclude(le => le.Jogo)
                .SingleOrDefaultAsync(m => m.EmprestimoID == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Devolver/5
        public async Task<IActionResult> Devolver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Amigo)
                .Include(e => e.EmprestimoJogo)
                .ThenInclude(le => le.Jogo)
                .SingleOrDefaultAsync(m => m.EmprestimoID == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            emprestimo.DataDevolucao = DateTime.Now;

            return View(emprestimo);
        }

        // POST: Emprestimos/Devolver/5
        [HttpPost, ActionName("Devolver")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver([Bind("EmprestimoID,DataDevolucao")] Emprestimo emprestimo)
        {
            var emprestimo_tmp = await _context.Emprestimo
                .SingleOrDefaultAsync(m => m.EmprestimoID == emprestimo.EmprestimoID);
            emprestimo_tmp.DataDevolucao = emprestimo.DataDevolucao;

            _context.Update(emprestimo_tmp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            ViewData["Amigo"] = new SelectList(_context.Amigos, "AmigoID", "Nome");
            ViewBag.Jogos = new Listagens(_context).JogosCheckBox();
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind("EmprestimoID,DataEmprestimo,DataDevolucaoPrevista,AmigoID")] Emprestimo emprestimo
            , string[] selectedJogos)
        {
            if (selectedJogos != null)
            {
                emprestimo.EmprestimoJogo = new List<EmprestimoJogo>();

                foreach (var id in selectedJogos)
                {
                    emprestimo.EmprestimoJogo.Add(new EmprestimoJogo
                    {
                        Emprestimo = emprestimo,
                        JogoID = Convert.ToInt32(id)
                    });
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(emprestimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Amigo"] = new SelectList(_context.Amigos, "AmigoID", "Nome", emprestimo.AmigoID);
            ViewBag.Jogos = new Listagens(_context).JogosCheckBox();

            return View(emprestimo);
        }


        // GET: Emprestimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Amigo)
                .SingleOrDefaultAsync(m => m.EmprestimoID == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimo
                .Include(e => e.EmprestimoJogo)
                .SingleOrDefaultAsync(m => m.EmprestimoID == id);

            _context.EmprestimoJogo.RemoveRange(emprestimo.EmprestimoJogo);
            _context.Emprestimo.Remove(emprestimo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimo.Any(e => e.EmprestimoID == id);
        }
    }
}
