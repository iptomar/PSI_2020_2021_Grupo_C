using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LuizaAndaluz.Models;
using Luiza_Andaluz.Data;

namespace Luiza_Andaluz.Controllers
{
    public class HistoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Historias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ficheiros.Include(h => h.Local).Include(h => h.Utilizador);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Historias/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historia = await _context.Ficheiros
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
            ViewData["LocalFK"] = new SelectList(_context.Comentarios, "ID", "ID");
            ViewData["UtilizadorFK"] = new SelectList(_context.Downloads, "ID", "ID");
            return View();
        }

        // POST: Historias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,Descricao,Estado,Nome,Idade,Email,LocalFK,UtilizadorFK")] Historia historia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalFK"] = new SelectList(_context.Comentarios, "ID", "ID", historia.LocalFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Downloads, "ID", "ID", historia.UtilizadorFK);
            return View(historia);
        }

        // GET: Historias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historia = await _context.Ficheiros.FindAsync(id);
            if (historia == null)
            {
                return NotFound();
            }
            ViewData["LocalFK"] = new SelectList(_context.Comentarios, "ID", "ID", historia.LocalFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Downloads, "ID", "ID", historia.UtilizadorFK);
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
            ViewData["LocalFK"] = new SelectList(_context.Comentarios, "ID", "ID", historia.LocalFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.Downloads, "ID", "ID", historia.UtilizadorFK);
            return View(historia);
        }

        // GET: Historias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historia = await _context.Ficheiros
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
            var historia = await _context.Ficheiros.FindAsync(id);
            _context.Ficheiros.Remove(historia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaExists(string id)
        {
            return _context.Ficheiros.Any(e => e.ID == id);
        }
    }
}
