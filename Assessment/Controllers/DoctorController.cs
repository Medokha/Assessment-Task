using Assessment.ViewModels.Edu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        HosinOldTestingContext _context = new HosinOldTestingContext();
        [HttpGet("doctor/{id}")]
        public async Task<IActionResult> GetDoctorInformationAsync(int id)
        {
            try
            {
                var emp = await _context.userpermations
                    .Include(c => c.Dep)
                    .Where(c => c.Id == id)
                    .Select(c => new doctorinformation
                    {
                        name = c.Username,
                        department = c.Dep.Name,
                        phone = c.PhoneNumber
                    }).FirstOrDefaultAsync();

                if (emp == null)
                    return NotFound();

                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("material/{id}")]
        public async Task<IActionResult> GetMaterailAsync(int id)
        {
            try
            {
                var doctorId = id;
                var maxYearId = await _context.DepartmentsYears.MaxAsync(y => y.Id);

                var departmentMatrials = await _context.DepartmentsMaterials
                    .Include(dm => dm.Department)
                    .Include(dm => dm.Stage)
                    .Where(dm => dm.UserPerId == doctorId && dm.YearId == maxYearId)
                    .Select(c => new materail
                    {
                        name = c.Name,
                        department = c.Department.Name,
                        stage = c.Stage.Stage,
                        id = c.Id,
                    })
                    .ToListAsync();

                //if (!departmentMatrials.Any())
                //    return NotFound();

                return Ok(departmentMatrials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("studentmaterial/{id}")]
        public async Task<IActionResult> GetStudentMaterailAsync(int id)
        {
            try
            {
                var maxYearId = await _context.DepartmentsYears.MaxAsync(y => y.Id);
                var students = await _context.DepartmentsStudentmaterials
                    .Include(c => c.Studentinfromtion).ThenInclude(c => c.Student)
                    .Where(c => c.MaterialId == id && c.Studentinfromtion.YearId == maxYearId)
                    .Select(c => new StudentDegreeVM
                    {
                        StudentName = c.Studentinfromtion.Student.FullName,
                        Quest = c.Quest,
                    })
                                   .ToListAsync();
                if (students == null)
                    return NotFound();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
