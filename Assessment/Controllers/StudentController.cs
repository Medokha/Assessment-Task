using Assessment.ViewModels.Edu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        HosinOldTestingContext _context = new HosinOldTestingContext();

        [HttpGet("{id}")]
        [Authorize(Roles = "std")]
        public async Task<IActionResult> GetStudentInformationAsync(int id)
        {
            try
            {
                var emp = await _context.UseresUsers
                    .Include(c => c.Dep)
                    .Include(c => c.Religion)
                    .Include(c => c.Nationality)
                    .Where(c => c.Id == id)
                    .Select(c => new studentinformation
                    {
                        Image = c.PersonalPhoto,
                        name = c.FullName,
                        states = c.MainStatus,
                        mother = c.MotherName,
                        nationality = c.Nationality.Name,
                        sex = c.Sex,
                        religion = c.Religion.Name,
                        nat = c.nationalism,
                        city = c.Gev,
                        citykda = c.MdName,
                        cityone = c.Area,
                        citytwo = c.Store,
                        citythree = c.Zqaq,
                        cityfour = c.Dar,
                        phone = c.SuperiorPhoneNumber,
                        cityborn = c.PlaceOfBrith,
                        date = c.BrithDate,

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
        [Authorize(Roles = "std")]
        public async Task<IActionResult> GetStudentMaterialAsync(int id)
        {
            try
            {
                var maxYearId = await _context.DepartmentsYears.MaxAsync(y => y.Id);

                var query = await _context.UseresUsers
                .Include(c => c.userpermations)
                .Include(o => o.Dep)
                .Include(o => o.DepartmentsMaterials)
                .ThenInclude(s => s.DepartmentsMaterialfiles)
                .Include(o => o.DepartmentsStudentinfromtions)
                .ThenInclude(o => o.DepartmentsStudentmaterials)
                .Include(o => o.DepartmentsStudentinfromtions)
                .ThenInclude(o => o.Stage)
                .Where(c => c.Id == id )
                .SelectMany(c => c.DepartmentsStudentinfromtions
                .SelectMany(s => s.DepartmentsStudentmaterials
                .Select(m => new Stumatrial
                {
                    DocName = c.userpermations.Name,
                    SubName = m.Material.Name,
                    Dep = c.Dep.Name,
                    Quest = m.Quest ?? 0,
                    Total = m.Total.HasValue ? (int)m.Total.Value : 0,
                    StageName = s.Stage.Stage,
                    Grade = CalculateGrade(m.Total.HasValue ? (int)m.Total.Value : 0),
                    FileName = m.Material.DepartmentsMaterialfiles.FirstOrDefault().File
                }))).ToListAsync();

                if (query == null)
                    return NotFound();
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private static string CalculateGrade(int total)
        {
            return total switch
            {
                >= 90 => "أمتياز",
                >= 80 => "جيد جدا",
                >= 70 => "جيد",
                >= 60 => "متوسط",
                >= 50 => "مقبول",
                _ => "رسوب"
            };
        }
    }
}
