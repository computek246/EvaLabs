using System.Linq;
using System.Threading.Tasks;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Areas.Admin.Controllers
{
    public class UserTestController : AdminBaseController
    {
        private readonly EvaContext _context;

        public UserTestController(EvaContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var evaContext = _context.UserTests.Include(u => u.Area).Include(u => u.Branch).Include(u => u.City)
                .Include(u => u.Lab).Include(u => u.Test).Include(u => u.TestStatus).Include(u => u.User);
            return View(await evaContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var userTest = await _context.UserTests
                .Include(u => u.Area)
                .Include(u => u.Branch)
                .Include(u => u.City)
                .Include(u => u.Lab)
                .Include(u => u.Test)
                .Include(u => u.TestStatus)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTest == null) return NotFound();

            return View(userTest);
        }


        public IActionResult Create()
        {
            GetViewData(null);
            return View(new UserTest
            {
                TestLocation = 2
            });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserTest userTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            GetViewData(userTest);
            return View(userTest);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var userTest = await _context.UserTests.FindAsync(id);
            if (userTest == null) return NotFound();
            GetViewData(userTest);
            return View(userTest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserTest userTest)
        {
            if (id != userTest.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTestExists(userTest.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            GetViewData(userTest);
            return View(userTest);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var userTest = await _context.UserTests
                .Include(u => u.Area)
                .Include(u => u.Branch)
                .Include(u => u.City)
                .Include(u => u.Lab)
                .Include(u => u.Test)
                .Include(u => u.TestStatus)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTest == null) return NotFound();

            return View(userTest);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTest = await _context.UserTests.FindAsync(id);
            _context.UserTests.Remove(userTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTestExists(int id)
        {
            return _context.UserTests.Any(e => e.Id == id);
        }

        private void GetViewData(UserTest userTest)
        {
            if (userTest == null)
            {
                ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "AreaName");
                ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName");
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
                ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabName");
                ViewData["TestId"] = new SelectList(_context.Tests, "Id", "TestName");
                ViewData["TestStatusId"] = new SelectList(_context.TestStatuses, "Id", "StatusName");
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            }
            else
            {
                ViewData["AreaId"] = new SelectList(_context.Areas, "Id", "AreaName", userTest.AreaId);
                ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName", userTest.BranchId);
                ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName", userTest.CityId);
                ViewData["LabId"] = new SelectList(_context.Labs, "Id", "LabName", userTest.LabId);
                ViewData["TestId"] = new SelectList(_context.Tests, "Id", "TestName", userTest.TestId);
                ViewData["TestStatusId"] = new SelectList(_context.TestStatuses, "Id", "StatusName", userTest.TestStatusId);
                ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName", userTest.UserId);
            }
        }

    }
}