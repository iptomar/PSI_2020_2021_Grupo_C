using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LuizaAndaluz.Models;
using Luiza_Andaluz.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Luiza_Andaluz.Controllers
{
    public class HistoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _caminho;
        public HistoriasController(ApplicationDbContext context, IWebHostEnvironment caminho)
        {
            _context = context;
            _caminho = caminho;
        }

        // GET: Historias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Historias.Include(h => h.Local).Include(h => h.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Historias
        public IActionResult GetHistoriasByLocation(string lat, string lng)
        {

            Local local = null;
            List<NewHistoria> historias = null;
            try
            {
                local = _context.Local.Where(l => l.Latitude == lat && l.Longitude == lng).FirstOrDefault();
                historias = _context.Historias.Where(h => h.LocalFK == local.ID).Select(x => new NewHistoria
                {

                    ID = x.ID,
                    Descricao = x.Descricao,
                    Titulo = x.Titulo,
                    Data = x.Data.ToString("dd-MM-yyyy"),
                }).ToList();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno");
            }

            return Ok(historias);
        }

        // GET: Historias/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historia = await _context.Historias
                .Include(h => h.Local)
                .Include(h => h.Utilizador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (historia == null)
            {
                return NotFound();
            }

            return View(historia);
        }

        // GET: Historias/Create
        public IActionResult Create()
        {
            ViewData["LocalFK"] = new SelectList(_context.Local, "ID", "ID");
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizador, "ID", "ID");
            return View();
        }

        // POST: Historias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,Descricao,Nome,Idade,Email")] Historia historia, List<IFormFile> fich, String lat, String lng)
        {
            if (lat.Equals("0") || lng.Equals("0"))
            {
                return View();
            }

            Local local = new Local
            {
                ID = Guid.NewGuid().ToString(),
                Latitude = lat,
                Longitude = lng
            };
            _context.Add(local);
            await _context.SaveChangesAsync();

            historia.ID = Guid.NewGuid().ToString();
            historia.Estado = true;
            historia.LocalFK = local.ID;
            historia.UtilizadorFK = "naoval";

            _context.Add(historia);
            await _context.SaveChangesAsync();

            foreach (IFormFile ficheiro in fich)
            {

                string caminhoCompleto = "";
                bool haFicheiro = false;

                if (ficheiro == null)
                {
                    return View();
                }
                string extensao = Path.GetExtension(ficheiro.FileName).ToLower();
                string nome = Guid.NewGuid().ToString() + extensao;
                caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Ficheiros", nome);
                haFicheiro = true;
                try
                {
                    if (haFicheiro)
                    {
                        Conteudo cont = new Conteudo
                        {
                            ID = Guid.NewGuid().ToString(),
                            HistoriaFK = historia.ID,
                            Tipo = extensao,
                            Ficheiro = nome

                        };
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await ficheiro.CopyToAsync(stream);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                }
            }
            return View();

        }

        // GET: Historias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historia = await _context.Historias.FindAsync(id);
            if (historia == null)
            {
                return NotFound();
            }
            ViewData["LocalFK"] = new SelectList(_context.Local, "ID", "ID", historia.LocalFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizador, "ID", "ID", historia.UtilizadorFK);
            return View(historia);
        }

        // POST: Historias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Titulo,Descricao,Estado,Nome,Idade,Email,LocalFK,UtilizadorFK")] Historia historia)
        {
            if (id != historia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriaExists(historia.ID))
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
            ViewData["LocalFK"] = new SelectList(_context.Local, "ID", "ID", historia.LocalFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizador, "ID", "ID", historia.UtilizadorFK);
            return View(historia);
        }

        // GET: Historias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historia = await _context.Historias
                .Include(h => h.Local)
                .Include(h => h.Utilizador)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (historia == null)
            {
                return NotFound();
            }

            return View(historia);
        }

        // POST: Historias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var historia = await _context.Historias.FindAsync(id);
            _context.Historias.Remove(historia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaExists(string id)
        {
            return _context.Historias.Any(e => e.ID == id);
        }
    }

    public class NewHistoria
    {
        public string ID;
        public string Descricao;
        public string Titulo;
        public string Data;
    }

}
