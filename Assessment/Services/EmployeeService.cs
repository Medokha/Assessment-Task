using Assessment.Services;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeContext _context;

        public EmployeeService(EmployeeContext context)
        {
            _context = context;
        }


        public async Task<Employee> Add(Employee emp)
        {
            await _context.AddAsync(emp);
            _context.SaveChanges();

            return emp;
        }

        public Employee Delete(Employee emp)
        {
            _context.Remove(emp);
            _context.SaveChanges();

            return emp;
        }

        public async Task<IEnumerable<Employee>> GetAll(int id = 0)
        {
            return await _context.Employees
                .Where(m => m.EmployeeID == id || id == 0)
                .Include(m => m.department)
                .ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context.Employees.Include(m => m.department).SingleOrDefaultAsync(m => m.EmployeeID == id);
        }

        public Employee Update(Employee emp)
        {
            _context.Update(emp);
            _context.SaveChanges();

            return emp;
        }
    }
}