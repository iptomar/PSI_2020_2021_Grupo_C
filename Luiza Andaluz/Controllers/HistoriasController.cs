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
        public HistoriasController(ApplicationDbContext context, IWebHostEnvironment caminho, UserManager<IdentityUser> userManager){
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }

        // GET: Historias
        [Authorize]
        public async Task<IActionResult> Index(){
            var applicationDbContext = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PorValidar(){
            var applicationDbContext = _context.Historias.Where(e => e.Estado == false);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Historias
        public IActionResult GetHistoriasByLocation(string lat, string lng){
            Local local = null;
            List<NewHistoria> historias = null;
            try{
                local = _context.Local.Where(l => l.Latitude == lat && l.Longitude == lng).FirstOrDefault();
                historias = _context.Historias.Where(h => h.LocalFK == local.ID).Select(x => new NewHistoria
                {
                    ID = x.ID,
                    Descricao = x.Descricao,
                    Titulo = x.Titulo,
                    Data = x.Data.ToString("dd-MM-yyyy"),
                }).ToList();
            }
            catch (Exception){
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno");
            }

            return Ok(historias);
        }

        // GET: Historias/Details/5
        public async Task<IActionResult> Details(string id){
            if (id == null){
                return NotFound();
            }

            var historia = await _context.Historias
                .Include(h => h.Local)
                .Include(h => h.Conteudo)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (historia == null){
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
            ViewBag.locais = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true).Select(l => l.Local).ToList();
            return View();
        }

        [HttpPost, ActionName("Validar")]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "admin")]
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
            if (lat.Equals("0") || lng.Equals("0"))
            {
                return View();
            }

            Local local = null;
            if (_context.Local.Any(l => l.Latitude == lat && l.Longitude == lng))
            {
                local = await _context.Local.FirstOrDefaultAsync(l => l.Latitude == lat && l.Longitude == lng);

            }
            else
            {
                local = new Local
                {
                    ID = Guid.NewGuid().ToString(),
                    Latitude = lat,
                    Longitude = lng
                };
                _context.Add(local);
                await _context.SaveChangesAsync();
            }
            var user = await _userManager.GetUserAsync(User);
            historia.Estado = false;
            if (user != null) historia.Estado = true;
            historia.ID = Guid.NewGuid().ToString();
            historia.LocalFK = local.ID;
            historia.Local = local;
            historia.Data = DateTime.Now;
            historia.Validador = null;

            _context.Historias.Add(historia);
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
                catch (Exception)
                {
                    return View();
                }
            }
            return Redirect("~/home"); ;

        }

        // GET: Historias/Edit/5
        [Authorize]
        [Authorize(Roles = "admin")]
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
            ViewBag.locais = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true).Select(x => x.Local).ToList();
            Local loc = await _context.Local.FirstOrDefaultAsync(l => l.ID == historia.LocalFK);
            ViewBag.Latitude = loc.Latitude;
            ViewBag.Longitude = loc.Longitude;
            return View(historia);
        }

        // POST: Historias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Titulo,Descricao,Estado,Nome,Idade,Email,UtilizadorFK")] Historia historia, String lat, String lng)
        {
            if (id != historia.ID)
            {
                return NotFound();
            }

            Local local = null;

            if (_context.Local.Any(l => l.Latitude == lat && l.Longitude == lng))
            {
                local = await _context.Local.FirstOrDefaultAsync(l => l.Latitude == lat && l.Longitude == lng);
                _context.Update(local);
            }
            else
            {
                local = new Local
                {
                    ID = Guid.NewGuid().ToString(),
                    Latitude = lat,
                    Longitude = lng
                };
                _context.Add(local);
            }

            try
            {
                historia.LocalFK = local.ID;
                historia.Local = local;
                _context.Update(historia);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoriaExists(historia.ID)){
                    return NotFound();
                }
            }

            ViewBag.locais = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true).Select(x => x.Local).ToList();
            Local loc = await _context.Local.FirstOrDefaultAsync(l => l.ID == historia.LocalFK);
            ViewBag.Latitude = loc.Latitude;
            ViewBag.Longitude = loc.Longitude;
            return View(historia);

        }

        // GET: Historias/Delete/5
        [Authorize]
        [Authorize(Roles = "admin")]
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
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var historia = await _context.Historias.FirstOrDefaultAsync(h => h.ID == id);
            var local = await _context.Local.FirstOrDefaultAsync(l => l.ID == historia.LocalFK);
            var conteudo =  _context.Conteudo.Where(l => l.HistoriaFK == historia.ID).ToList();
            _context.Historias.Remove(historia);
            if(local.Historia.Count == 1) _context.Local.Remove(local);
            for(var i = 0; i < conteudo.Count; i++){
                System.IO.File.Delete(Path.Combine(_caminho.WebRootPath, "Ficheiros", conteudo[i].Ficheiro));
                _context.Conteudo.Remove(conteudo[i]);
            }
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
