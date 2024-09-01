using Microsoft.EntityFrameworkCore;
using Assessment.Model;

namespace Assessment.Services
{
    public class DepartmentService : IDepartmentsService
    {
        private readonly EmployeeContext _context;

        public DepartmentService(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<Department> Add(Department Department)
        {
            await _context.AddAsync(Department);
            _context.SaveChanges();

            return Department;
        }

        public Department Delete(Department Department)
        {
            _context.Remove(Department);
            _context.SaveChanges();

            return Department;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Department.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
            return await _context.Department.SingleOrDefaultAsync(g => g.Id == id);
        }


        public Department Update(Department Department)
        {
            _context.Update(Department);
            _context.SaveChanges();

            return Department;
        }
    }
}
