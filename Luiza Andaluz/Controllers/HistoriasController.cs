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
/// <summary>
/// Classe referente as historias de Luiza Andaluz
/// Contem: ligação a DB, Anbiente web e controlador de administrador de Utilizadores
/// </summary>
    public class HistoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _caminho;
        private readonly UserManager<IdentityUser> _userManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">contexto da Base de Dados</param>
        /// <param name="caminho">Ambiente onde a aplicação está a correr</param>
        /// <param name="userManager">Administração de Utilizadores</param>
        public HistoriasController(ApplicationDbContext context, IWebHostEnvironment caminho, UserManager<IdentityUser> userManager){
            _context = context;
            _caminho = caminho;
            _userManager = userManager;
        }
        /// <summary>
        /// se o utilizador tem permições de Admin ou de "irmã"
        /// Acede à base de dados e retira todas as historias de luiza andaluz
        /// </summary>
        /// <returns>View com as historias de Luiza Andaluz</returns>
        // GET: Historias
        [Authorize]
        [Authorize(Roles = "admin, irma")]
        public async Task<IActionResult> Index(){
            var applicationDbContext = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// se o utilizador tem permições de admin
        /// retorna da DB as historias por validar de Luiza Andaluz
        /// </summary>
        /// <returns>view de validação</returns>
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> PorValidar(){
            var applicationDbContext = _context.Historias.Where(e => e.Estado == false);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// acede á BD e retira as historias de luiza andaluz com as coordenadas passadas
        /// </summary>
        /// <param name="lat">Latitude do local da historia</param>
        /// <param name="lng">longitude do local da historia</param>
        /// <returns></returns>
        // GET: Historias
        public IActionResult GetHistoriasByLocation(string lat, string lng){
            Local local = null;
            List<NewHistoria> historias = null;
            try{
                local = _context.Local.Where(l => l.Latitude == lat && l.Longitude == lng).FirstOrDefault();
                var ahistorias = _context.Historias.Include(h => h.Conteudo).Where(h => h.LocalFK == local.ID);

                historias = ahistorias.Select(x => new NewHistoria
                {
                    ID = x.ID,
                    Descricao = x.Descricao,
                    Titulo = x.Titulo,
                    Data = x.Data.ToString("dd-MM-yyyy"),
                    Conteudo = x.Conteudo.Count != 0 ? x.Conteudo.ToList()[0].Ficheiro : "nada"
                }).ToList();
            }
            catch (Exception){
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno");
            }

            return Ok(historias);
        }

        /// <summary>
        /// Acede aos detalhes de uma historia de Luiza Andaluz
        /// </summary>
        /// <param name="id">Parametro ID da Historia</param>
        /// <returns>view de Detalhes da historia</returns>
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

        /// <summary>
        /// metodo que retorna a pagina de criaçao de uma historia
        /// </summary>
        /// <returns>View de Criação de uma historia</returns>
        // GET: Historias/Create
        public IActionResult Create()
        {
            ViewBag.locais = _context.Historias.Include(h => h.Local).Where(h => h.Estado == true).Select(l => l.Local).ToList();
            return View();
        }

        /// <summary>
        /// se o utilizador tem permissões de  admin
        /// pesquisa na DB por a historia de luiza andaluz e marca a historia como validada
        /// permitindo que seja vista por todos os utilizadores
        /// </summary>
        /// <param name="id">ID da Historia </param>
        /// <returns>view de detalhes</returns>
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

        /// <summary>
        /// metodo que guarda uma historia de luiza andaluz
        /// </summary>
        /// <param name="historia">Objeto com os parametros da Historia</param>
        /// <param name="fich">Ficheiros relevantes à historia</param>
        /// <param name="lat">Latitude do local da historia</param>
        /// <param name="lng">Longitude do local da historia</param>
        /// <returns>view da pagina home</returns>
        // POST: Historias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titulo,Descricao,Nome,Idade,Email")] Historia historia, List<IFormFile> fich, String lat, String lng){
            if (lat.Equals("0") || lng.Equals("0")){
                ViewBag.Erro = "Insira um Local";
                return View();
            }
            bool locali = false;
            Local local = null;
            if (_context.Local.Any(l => l.Latitude == lat && l.Longitude == lng)){
                local = await _context.Local.FirstOrDefaultAsync(l => l.Latitude == lat && l.Longitude == lng);
            }
            else{
                local = new Local{
                    ID = Guid.NewGuid().ToString(),
                    Latitude = lat,
                    Longitude = lng
                };
                locali = true;
            }
            var user = await _userManager.GetUserAsync(User);
            historia.Estado = false;
            if (user != null) historia.Estado = true;
            historia.ID = Guid.NewGuid().ToString();
            historia.LocalFK = local.ID;
            historia.Local = local;
            historia.Data = DateTime.Now;
            historia.Validador = null;

            try{
                if (locali){
                    _context.Local.Add(local);
                    await _context.SaveChangesAsync();
                }
                _context.Historias.Add(historia);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex){
                return View();
            }

            foreach (IFormFile ficheiro in fich){
                string caminhoCompleto = "";
                bool haFicheiro = false;

                if (ficheiro == null){
                    return View();
                }
                string extensao = Path.GetExtension(ficheiro.FileName).ToLower();
                string nome = Guid.NewGuid().ToString() + extensao;
                caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Ficheiros", nome);
                haFicheiro = true;
                try{
                    if (haFicheiro){
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
                catch (Exception){
                    return View();
                }
            }
            return View("HistoryCreated");
        }

        /// <summary>
        /// Metodo que retorna a pagina de edição de uma historia
        /// </summary>
        /// <param name="id">ID da Historia de Luiza Andaluz</param>
        /// <returns>view da pagina Edit</returns>
        // GET: Historias/Edit/5
        [Authorize]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id){
            if (id == null){
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

        /// <summary>
        /// Guarda as Alterações feitas na pagina edit
        /// </summary>
        /// <param name="id">ID da Historia</param>
        /// <param name="historia">Objeto com os parametros da Historia</param>
        /// <param name="lat">Latitude do local da historia</param>
        /// <param name="lng">longitude do local da historia</param>
        /// <returns>View dos Detalhes da Historia</returns>
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

        /// <summary>
        /// Se O Utilizador tem permissões de admin
        /// retorna a pagina de delete
        /// </summary>
        /// <param name="id">ID da historia</param>
        /// <returns>View da pagina de delete</returns>
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

        /// <summary>
        /// se o utilizdor tem permissões de admin
        /// apaga uma historia da Base de dados
        /// </summary>
        /// <param name="id">ID da historia</param>
        /// <returns>view das historias</returns>
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
            if (!(_context.Historias.Any(h => h.LocalFK == local.ID))){
                _context.Local.Remove(local);
            }
            for(var i = 0; i < conteudo.Count; i++){
                System.IO.File.Delete(Path.Combine(_caminho.WebRootPath, "Ficheiros", conteudo[i].Ficheiro));
                _context.Conteudo.Remove(conteudo[i]);
            }
            await _context.SaveChangesAsync();
            return Redirect("~/home"); ;
        }

        private bool HistoriaExists(string id)
        {
            return _context.Historias.Any(e => e.ID == id);
        }
    }

    /// <summary>
    /// classe interna de uma nova histora
    /// </summary>
    public class NewHistoria
    {
        public string ID;
        public string Descricao;
        public string Titulo;
        public string Data;
        public string Conteudo;
    }

}
