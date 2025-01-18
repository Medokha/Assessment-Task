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
        /// <summary>
        /// تستخدم هذه الداله فى الحصول على معلومات الطالب الشخصيه
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize(Roles = "std")]
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
                        Image = c.PersonalPhoto, // صوره شخصيه 
                        name = c.FullName, // الاسم كامل
                        states = c.MainStatus, // حاله الطالب
                        mother = c.MotherName, // اسم الام
                        nationality = c.Nationality.Name, // الجنسيه
                        sex = c.Sex, // النوع
                        religion = c.Religion.Name, // الديانه
                        nat = c.nationalism,  // الهويه الوطنيه
                        city = c.Gev, // المحافظه
                        citykda = c.MdName,
                        cityone = c.Area,  //
                        citytwo = c.Store, //
                        citythree = c.Zqaq, // الزقاق
                        cityfour = c.Dar, // الدار
                        phone = c.SuperiorPhoneNumber, // رقم الهاتف
                        cityborn = c.PlaceOfBrith, // محل الولاده
                        date = c.BrithDate, // تاريخ الميلاد

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

        /// <summary>
        /// تستخدم هذه الداله فى الحصول على مواد الطالب خلال جميع المراحل و يمكن اختيار مرحله واحده
        /// </summary>
        [HttpGet("material/{id}")]
        //[Authorize(Roles = "std")]
        public async Task<IActionResult> GetStudentMaterialAsync(int id,string? stage)
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
                    DocName = c.userpermations.Name, // اسم استاذ الماده
                    SubName = m.Material.Name, // اسم الماده 
                    Dep = c.Dep.Name, // القسم 
                    Quest = m.Quest ?? 0, // درجه السعى 
                    Total = m.Total.HasValue ? (int)m.Total.Value : 0, // الدرجه الكليه
                    StageName = s.Stage.Stage, // المرحله
                    Grade = CalculateGrade(m.Total.HasValue ? (int)m.Total.Value : 0), // التقدير
                    FileName = m.Material.DepartmentsMaterialfiles.FirstOrDefault().File // كتاب الماده
                }))).ToListAsync();
                if (!string.IsNullOrEmpty(stage))
                {
                    query = query.Where(c => c.StageName == stage).ToList();
                }
                if (query == null || !query.Any())
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


        /// <summary>
        /// و يمكن اختيار مرحله واحده
        /// </summary>
        [HttpGet("materialLast/{id}")]
        //[Authorize(Roles = "std")]
        public async Task<IActionResult> GetStudentMaterialLastYearAsync(int id)
        {
            try
            {
                var stdid = _context.DepartmentsStudentinfromtions.Where(c=>c.StudentId==id).Select(c => c.YearId).ToList();
                //var maxYearId = await _context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxYearId =  stdid.Max();

                var query = await _context.UseresUsers
                .Include(c => c.userpermations)
                .Include(o => o.Dep)
                .Include(o => o.DepartmentsMaterials)
                .ThenInclude(s => s.DepartmentsMaterialfiles)
                .Include(o => o.DepartmentsStudentinfromtions)
                .ThenInclude(o => o.DepartmentsStudentmaterials)
                .Include(o => o.DepartmentsStudentinfromtions)
                .ThenInclude(o => o.Stage)
                .Where(c => c.Id == id)
                .SelectMany(c => c.DepartmentsStudentinfromtions
                .SelectMany(s => s.DepartmentsStudentmaterials
                .Select(m => new Stumatrial
                {
                    DocName = c.userpermations.Name, // اسم استاذ الماده
                    SubName = m.Material.Name, // اسم الماده 
                    Dep = c.Dep.Name, // القسم 
                    Quest = m.Quest ?? 0, // درجه السعى 
                    Total = m.Total.HasValue ? (int)m.Total.Value : 0, // الدرجه الكليه
                    StageName = s.Stage.Stage, // المرحله
                    Grade = CalculateGrade(m.Total.HasValue ? (int)m.Total.Value : 0), // التقدير
                    FileName = m.Material.DepartmentsMaterialfiles.FirstOrDefault().File, // كتاب الماده
                    year =m.Studentinfromtion.YearId,
                }))).ToListAsync();
                if (maxYearId != null)
                {
                    query = query.Where(c => c.year == maxYearId).ToList();
                }
                //if (query == null || !query.Any())
                //    return NotFound();
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
