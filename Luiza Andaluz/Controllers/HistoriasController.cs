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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Luiza_Andaluz.Controllers
{
    public class HistoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _caminho;
        private readonly UserManager<IdentityUser> _userManager;
        public HistoriasController(ApplicationDbContext context, IWebHostEnvironment caminho, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: Historias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Historias.Include(h => h.Local);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> PorValidar()
        {
            var applicationDbContext = _context.Historias.Where(e => e.Estado == false);
            return View(await applicationDbContext.ToListAsync());
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
                .Include(h => h.Conteudo)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (historia == null)
            {
                return NotFound();
            }

            Local loc = await _context.Local.FirstOrDefaultAsync(l => l.ID == historia.LocalFK);
            ViewBag.latitude = loc.Latitude;
            ViewBag.longitude = loc.Longitude;
            return View(historia);
        }

        // GET: Historias/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Validar")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Validar(string id)
        {
            Historia hist = _context.Historias.FirstOrDefault(d => d.ID == id);
            hist.Validador = _userManager.GetUserId(User);
            hist.Estado = true;
            _context.Update(hist);
            await _context.SaveChangesAsync();
            return Redirect("Details/" + id);
        }

        // POST: Historias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,Descricao,Nome,Idade,Email")] Historia historia, List<IFormFile> fich, String lat, String lng)
        {
            if(lat.Equals("0") || lng.Equals("0")){
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
            historia.Estado = false;
            historia.LocalFK = local.ID;
            historia.Local = local;
            historia.Data = DateTime.Now;
            historia.Validador = null;

            _context.Historias.Add(historia);
            await _context.SaveChangesAsync();
            
            foreach(IFormFile ficheiro in fich){

                string caminhoCompleto = "";
                bool haFicheiro = false;

                if (ficheiro == null) {
                    return View();
                }
                string extensao = Path.GetExtension(ficheiro.FileName).ToLower();
                string nome = Guid.NewGuid().ToString() + extensao;
                caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Ficheiros", nome);
                haFicheiro = true;
                try
                {
                    if (haFicheiro) {
                        Conteudo cont = new Conteudo
                        {
                            ID = Guid.NewGuid().ToString(),
                            HistoriaFK = historia.ID,
                            Historia = historia,
                            Tipo = extensao,
                            Ficheiro = nome,
                        };
                        using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                        await ficheiro.CopyToAsync(stream);

                        _context.Conteudo.Add(cont);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception) {
                    return View();
                }
            }
            return RedirectToAction("home"); ;

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
}
