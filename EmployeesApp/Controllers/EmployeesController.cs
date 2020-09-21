using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesApp.Models;

namespace EmployeesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EmployeesController(AppDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<int> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee.Id;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(int companyId)
        {
            var employees = await _context.Employees
                .Where(e => e.CompanyId == companyId)
                .Include(e => e.Passport)
                .ToListAsync();

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }
                
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            var currentEmployee = _context.Employees.Include(e => e.Passport).Single(e => e.Id == id);

            currentEmployee.Id = employee.Id == 0 ? currentEmployee.Id : employee.Id;
            currentEmployee.Name = employee.Name ?? currentEmployee.Name;
            currentEmployee.Surname = employee.Surname ?? currentEmployee.Surname;
            currentEmployee.Phone = employee.Phone ?? currentEmployee.Phone;
            currentEmployee.CompanyId = employee.CompanyId == 0 ? currentEmployee.CompanyId : employee.CompanyId;
            currentEmployee.Passport.Type = employee.Passport.Type ?? currentEmployee.Passport.Type;
            currentEmployee.Passport.Number = employee.Passport.Number ?? currentEmployee.Passport.Number;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }        
        
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
