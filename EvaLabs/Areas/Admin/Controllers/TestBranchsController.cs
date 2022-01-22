using System.Linq;
using System.Threading.Tasks;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Areas.Admin.Controllers
{
    public class TestBranchsController : AdminBaseController
    {
        private readonly EvaContext _context;

        public TestBranchsController(EvaContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var evaContext = _context.TestBranchs.Include(t => t.Branch).Include(t => t.Test);
            return View(await evaContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var testBranchs = await _context.TestBranchs
                .Include(t => t.Branch)
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testBranchs == null) return NotFound();

            return View(testBranchs);
        }


        public IActionResult Create()
        {
            GetViewData(null);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestBranchs testBranchs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testBranchs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            GetViewData(testBranchs);
            return View(testBranchs);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var testBranchs = await _context.TestBranchs.FindAsync(id);
            if (testBranchs == null) return NotFound();
            GetViewData(testBranchs);
            return View(testBranchs);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestBranchs testBranchs)
        {
            if (id != testBranchs.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testBranchs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestBranchsExists(testBranchs.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            GetViewData(testBranchs);
            return View(testBranchs);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var testBranchs = await _context.TestBranchs
                .Include(t => t.Branch)
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testBranchs == null) return NotFound();

            return View(testBranchs);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testBranchs = await _context.TestBranchs.FindAsync(id);
            _context.TestBranchs.Remove(testBranchs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestBranchsExists(int id)
        {
            return _context.TestBranchs.Any(e => e.Id == id);
        }


        private void GetViewData(TestBranchs testBranchs)
        {
            if (testBranchs == null)
            {
                ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName");
                ViewData["TestId"] = new SelectList(_context.Tests, "Id", "TestName");
            }
            else
            {
                ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName", testBranchs.BranchId);
                ViewData["TestId"] = new SelectList(_context.Tests, "Id", "TestName", testBranchs.TestId);
            }
        }
    }
}