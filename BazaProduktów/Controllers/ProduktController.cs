using BazaProduktow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BazaProduktow.Controllers
{
    public class ProduktController : Controller
    {
        private readonly AppDbContext _context;

        public ProduktController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string szukaj, string kategoria)
        {
            var produkty = _context.Produkty.AsQueryable();

            if (!string.IsNullOrEmpty(szukaj))
                produkty = produkty.Where(p => p.Nazwa.Contains(szukaj) || p.Opis.Contains(szukaj));

            if (!string.IsNullOrEmpty(kategoria))
                produkty = produkty.Where(p => p.Kategoria == kategoria);

            ViewBag.Szukaj = szukaj;
            ViewBag.Kategoria = kategoria;
            ViewBag.Kategorie = await _context.Produkty.Select(p => p.Kategoria).Distinct().ToListAsync();

            return View(await produkty.ToListAsync());
        }

        public IActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Dodaj(Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                produkt.DataDodania = DateTime.Now;
                _context.Produkty.Add(produkt);
                await _context.SaveChangesAsync();
                TempData["Sukces"] = $"Produkt '{produkt.Nazwa}' został dodany.";
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        public async Task<IActionResult> Edytuj(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null) return NotFound();
            return View(produkt);
        }

        [HttpPost]
        public async Task<IActionResult> Edytuj(int id, Produkt produkt)
        {
            if (id != produkt.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Produkty.Update(produkt);
                await _context.SaveChangesAsync();
                TempData["Sukces"] = $"Produkt '{produkt.Nazwa}' został zaktualizowany.";
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        public async Task<IActionResult> Usun(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null) return NotFound();
            return View(produkt);
        }

        [HttpPost, ActionName("Usun")]
        public async Task<IActionResult> UsunPotwierdzony(int id)
        {
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt != null)
            {
                _context.Produkty.Remove(produkt);
                await _context.SaveChangesAsync();
                TempData["Sukces"] = $"Produkt '{produkt.Nazwa}' został usunięty.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}