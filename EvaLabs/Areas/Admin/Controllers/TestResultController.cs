using System.Linq;
using System.Threading.Tasks;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Areas.Admin.Controllers
{
    public class TestResultController : AdminBaseController
    {
        private readonly EvaContext _context;

        public TestResultController(EvaContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var evaContext = _context.TestResults.Include(t => t.UserTest);
            return View(await evaContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var testResult = await _context.TestResults
                .Include(t => t.UserTest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testResult == null) return NotFound();

            return View(testResult);
        }


        public IActionResult Create()
        {
            ViewData["UserTestId"] = new SelectList(_context.UserTests, "Id", "Id");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserTestId"] = new SelectList(_context.UserTests, "Id", "Id", testResult.UserTestId);
            return View(testResult);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult == null) return NotFound();
            ViewData["UserTestId"] = new SelectList(_context.UserTests, "Id", "Id", testResult.UserTestId);
            return View(testResult);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestResult testResult)
        {
            if (id != testResult.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestResultExists(testResult.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["UserTestId"] = new SelectList(_context.UserTests, "Id", "Id", testResult.UserTestId);
            return View(testResult);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var testResult = await _context.TestResults
                .Include(t => t.UserTest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testResult == null) return NotFound();

            return View(testResult);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testResult = await _context.TestResults.FindAsync(id);
            _context.TestResults.Remove(testResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestResultExists(int id)
        {
            return _context.TestResults.Any(e => e.Id == id);
        }
    }
}