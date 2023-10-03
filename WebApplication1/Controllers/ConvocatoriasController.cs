using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ConvocatoriasController : Controller
    {
        private readonly WebApplication1Context _context;

        public ConvocatoriasController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: Convocatorias
        public async Task<IActionResult> Index()
        {
              return _context.Convocatoria != null ? 
                          View(await _context.Convocatoria.ToListAsync()) :
                          Problem("Entity set 'WebApplication1Context.Convocatoria'  is null.");
        }

        // GET: Convocatorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Convocatoria == null)
            {
                return NotFound();
            }

            var convocatoria = await _context.Convocatoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (convocatoria == null)
            {
                return NotFound();
            }

            return View(convocatoria);
        }

        // GET: Convocatorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Convocatorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Contenido,Imagen,NombreCreador")] Convocatoria convocatoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(convocatoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(convocatoria);
        }

        // GET: Convocatorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Convocatoria == null)
            {
                return NotFound();
            }

            var convocatoria = await _context.Convocatoria.FindAsync(id);
            if (convocatoria == null)
            {
                return NotFound();
            }
            return View(convocatoria);
        }

        // POST: Convocatorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Contenido,Imagen,NombreCreador")] Convocatoria convocatoria)
        {
            if (id != convocatoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(convocatoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConvocatoriaExists(convocatoria.Id))
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
            return View(convocatoria);
        }

        // GET: Convocatorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Convocatoria == null)
            {
                return NotFound();
            }

            var convocatoria = await _context.Convocatoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (convocatoria == null)
            {
                return NotFound();
            }

            return View(convocatoria);
        }

        // POST: Convocatorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Convocatoria == null)
            {
                return Problem("Entity set 'WebApplication1Context.Convocatoria'  is null.");
            }
            var convocatoria = await _context.Convocatoria.FindAsync(id);
            if (convocatoria != null)
            {
                _context.Convocatoria.Remove(convocatoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConvocatoriaExists(int id)
        {
          return (_context.Convocatoria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
