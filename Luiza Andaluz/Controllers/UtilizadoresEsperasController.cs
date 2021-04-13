using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LuizaAndaluz.Models;
using Luiza_Andaluz.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Luiza_Andaluz.Controllers
{
    public class UtilizadoresEsperasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UtilizadoresEsperasController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UtilizadoresEsperas
        public async Task<IActionResult> Index()
        {
            return View(await _context.UtilizadoresEspera.ToListAsync());
        }

        [HttpPost, ActionName("Admitir")]
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Admitir(string id, string cargo)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(cargo == "Administrador" || cargo == "Irmã"){
                await _userManager.AddToRoleAsync(user, cargo == "Administrador" ? "admin" : "irma");
            
            }
            else if(cargo == "Não Admitir"){
                await _userManager.DeleteAsync(user);
            }
            UtilizadoresEspera util = await _context.UtilizadoresEspera.FirstOrDefaultAsync(u => u.ID == id);
            _context.UtilizadoresEspera.Remove(util);
            await _context.SaveChangesAsync();
            return RedirectToAction("");
        }


    }
}
