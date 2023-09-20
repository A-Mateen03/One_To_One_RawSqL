using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using One_To_One_RawSqL.Data;
using One_To_One_RawSqL.Models;

namespace One_To_One_RawSqL.Controllers
{
    public class EmployeeAddressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeAddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeAddress
        public async Task<IActionResult> Index()
        {
            var employeeAddresses = await _context.EmployeeAddress
                .FromSqlRaw("SELECT * FROM EmployeeAddress")
                .Include(ea => ea.Employee)
                .ToListAsync();

            return View(employeeAddresses);
        }


        // GET: EmployeeAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAddress = await _context.EmployeeAddress
                .FromSqlInterpolated($"SELECT * FROM EmployeeAddress WHERE EmployeeID = {id}").Include(ea => ea.Employee)
                .FirstOrDefaultAsync();

            if (employeeAddress == null)
            {
                return NotFound();
            }

            return View(employeeAddress);
        }

        // GET: EmployeeAddress/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            return View();
        }

        // POST: EmployeeAddress/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,Address")] EmployeeAddress employeeAddress)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO EmployeeAddress (EmployeeID, Address) VALUES ({employeeAddress.EmployeeID}, {employeeAddress.Address})");
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", employeeAddress.EmployeeID);
            return View(employeeAddress);
        }

        // GET: EmployeeAddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAddress = await _context.EmployeeAddress
                .FromSqlInterpolated($"SELECT * FROM EmployeeAddress WHERE EmployeeID = {id}")
                .FirstOrDefaultAsync();

            if (employeeAddress == null)
            {
                return NotFound();
            }

            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", employeeAddress.EmployeeID);
            return View(employeeAddress);
        }

        // POST: EmployeeAddress/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,Address")] EmployeeAddress employeeAddress)
        {
            if (id != employeeAddress.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE EmployeeAddress SET Address = {employeeAddress.Address} WHERE EmployeeID = {id}");
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", employeeAddress.EmployeeID);
            return View(employeeAddress);
        }

        // GET: EmployeeAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeAddress = await _context.EmployeeAddress
                .FromSqlInterpolated($"SELECT * FROM EmployeeAddress WHERE EmployeeID = {id}")
                .FirstOrDefaultAsync();

            if (employeeAddress == null)
            {
                return NotFound();
            }

            return View(employeeAddress);
        }

        // POST: EmployeeAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM EmployeeAddress WHERE EmployeeID = {id}");
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeAddressExists(int id)
        {
            var employeeAddress = _context.EmployeeAddress
                .FromSqlInterpolated($"SELECT * FROM EmployeeAddress WHERE EmployeeID = {id}")
                .FirstOrDefault();

            return employeeAddress != null;
        }
    }
}
