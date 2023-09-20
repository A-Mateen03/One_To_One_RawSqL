using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using One_To_One_RawSqL.Data;
using One_To_One_RawSqL.Models;

namespace One_To_One_RawSqL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employee.FromSqlRaw("SELECT * FROM Employee").ToListAsync();
            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FromSqlInterpolated($"SELECT * FROM Employee WHERE EmployeeID = {id}").FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,Name,PhoneNumber")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                //await _context.Database.ExecuteSqlRawAsync($"INSERT INTO Employees (Name, PhoneNumber) VALUES ({employee.Name}, {employee.PhoneNumber})");

                await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Employee (Name, PhoneNumber) VALUES ({employee.Name}, {employee.PhoneNumber})");
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FromSqlInterpolated($"SELECT * FROM Employee WHERE EmployeeID = {id}").FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeID,Name,PhoneNumber")] Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Employee SET Name = {employee.Name}, PhoneNumber = {employee.PhoneNumber} WHERE EmployeeID = {id}");
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FromSqlInterpolated($"SELECT * FROM Employee WHERE EmployeeID = {id}").FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Employee WHERE EmployeeID = {id}");
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            var employee = _context.Employee.FromSqlInterpolated($"SELECT * FROM Employee WHERE EmployeeID = {id}").FirstOrDefault();
            return employee != null;
        }
    }
}
