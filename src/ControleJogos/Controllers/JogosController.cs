using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleJogos.Data.Context;
using ControleJogos.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ControleJogos.Controllers
{
    public class JogosController : Controller
    {
        private readonly ControleJogosContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public JogosController(ControleJogosContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jogos.ToListAsync());
        }

        // GET: Jogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .SingleOrDefaultAsync(m => m.JogoID == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }

        // GET: Jogos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JogoID,Titulo,Quantidade,Foto")] Jogo jogo, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogo);
                await _context.SaveChangesAsync();

                jogo.Foto = await RealizarUploadImagens(files, jogo.JogoID);
                _context.Update(jogo);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(jogo);
        }

        // GET: Jogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos.SingleOrDefaultAsync(m => m.JogoID == id);
            if (jogo == null)
            {
                return NotFound();
            }
            return View(jogo);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JogoID,Titulo,Quantidade,Foto")] Jogo jogo, List<IFormFile> files)
        {
            if (id != jogo.JogoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (files.Count > 0)
                    {
                        jogo.Foto = await RealizarUploadImagens(files, jogo.JogoID);
                    }

                    _context.Update(jogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogoExists(jogo.JogoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jogo);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .SingleOrDefaultAsync(m => m.JogoID == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogo = await _context.Jogos.SingleOrDefaultAsync(m => m.JogoID == id);
            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogoExists(int id)
        {
            return _context.Jogos.Any(e => e.JogoID == id);
        }

        private async Task<string> RealizarUploadImagens(List<IFormFile> files, int idLivro)
        {
            // Verifica se existem arquivos selecionados
            if (files.Count > 0)
            {
                // Variável para armazenar o caminho de upload das imagens
                var pathUpload = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                // Se o caminho não existe então cria
                if (!Directory.Exists(pathUpload))
                    Directory.CreateDirectory(pathUpload);
                // Para cada arquivo faça
                foreach (var file in files)
                {
                    // Verifica se o arquivo possui informação
                    if (file.Length > 0)
                    {
                        // Concatena o nome do arquivo
                        var nomeArquivo = "livro_" + idLivro +
                        Path.GetExtension(file.FileName);
                        // Concatena o caminho do arquivo
                        var pathFile = Path.Combine(pathUpload, nomeArquivo);
                        // Realiza a cópia
                        using (var fileStream = new FileStream(pathFile,
                        FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        // Retorna o caminho do arquivo que será salvo
                        // no banco de dados
                        return "uploads//" + Path.GetFileName(pathFile);
                    }
                }
            }
            return null;
        }
    }
}
