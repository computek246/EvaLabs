using System.Linq;
using System.Threading.Tasks;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Areas.Admin.Controllers
{
    public class TestStatusController : AdminBaseController
    {
        private readonly EvaContext _context;

        public TestStatusController(EvaContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.TestStatuses.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var testStatus = await _context.TestStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testStatus == null) return NotFound();

            return View(testStatus);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestStatus testStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(testStatus);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var testStatus = await _context.TestStatuses.FindAsync(id);
            if (testStatus == null) return NotFound();
            return View(testStatus);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestStatus testStatus)
        {
            if (id != testStatus.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestStatusExists(testStatus.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(testStatus);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var testStatus = await _context.TestStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testStatus == null) return NotFound();

            return View(testStatus);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testStatus = await _context.TestStatuses.FindAsync(id);
            _context.TestStatuses.Remove(testStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestStatusExists(int id)
        {
            return _context.TestStatuses.Any(e => e.Id == id);
        }
    }
}