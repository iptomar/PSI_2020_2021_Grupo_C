using Luiza_Andaluz.Data;
using Luiza_Andaluz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Luiza_Andaluz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true);
            ViewBag.locais = applicationDbContext.Select(x => new NewLocal { 
                                latitude = x.Local.Latitude,
                                longitude = x.Local.Longitude
                             }).ToList();
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class NewLocal
    {
        public string latitude;
        public string longitude;
    }
}
