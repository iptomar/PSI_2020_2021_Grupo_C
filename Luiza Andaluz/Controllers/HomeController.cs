using Luiza_Andaluz.Data;
using Luiza_Andaluz.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
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
        private readonly IStringLocalizer<HomeController> _stringLocalizer;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, IStringLocalizer<HomeController> stringLocalizer, IHtmlLocalizer<HomeController> localizer)
        {
            _context = context;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index()
        {
            /*string test = _stringLocalizer["Boas"].Value;
            ViewData["Title"] = test;*/
            string message = _stringLocalizer["GreetingMessage"].Value;
            ViewData["Title"] = message;
            var applicationDbContext = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true);
            ViewBag.locais = applicationDbContext.Select(x => x.Local).ToList();
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult GereLinguagens(string linguagem)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(linguagem)),
                new CookieOptions { Expires = DateTime.Now.AddHours(12) });
                
            return RedirectToAction(nameof(Index));
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
}
