using Assessment.ViewModels.Edu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        HosinOldTestingContext _context = new HosinOldTestingContext();
        /// <summary>
        /// تستخدم هذه الداله فى الحصول على معلومات الاستاذ الشخصيه
        /// </summary>
        [HttpGet("doctor/{id}")]
        //[Authorize(Roles = "edu")]
        public async Task<IActionResult> GetDoctorInformationAsync(int id)
        {
            try
            {
                var emp = await _context.userpermations
                    .Include(c => c.Dep)
                    .Where(c => c.Id == id)
                    .Select(c => new doctorinformation
                    {
                        name = c.Username, // اسم المستخدم
                        department = c.Dep.Name, // القسم
                        phone = c.PhoneNumber // رقم الهاتف
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
        /// تستخدم هذه الداله فى الحصول على مواد الاستاذ و يمكن تحديد المرحله و السنه
        /// </summary>
        [HttpGet("material/{id}")]
        //[Authorize(Roles = "edu")]
        public async Task<IActionResult> GetMaterailAsync(int id, string? stage, string? year)
        {
            try
            {
                var doctorId = id;
                var departmentMatrials = await _context.DepartmentsMaterials
                    .Include(dm => dm.Department)
                    .Include(dm => dm.Stage)
                    .Include(dm => dm.Year)
                    .Where(dm => dm.UserPerId == doctorId )
                    .Select(c => new materail
                    {
                        name = c.Name, // اسم الماده 
                        department = c.Department.Name, // القسم
                        stage = c.Stage.Stage, // المرحله
                        id = c.Id,
                        year =c.Year.Year,// السنه
                    })
                    .ToListAsync();
                if (!string.IsNullOrEmpty(stage))
                {
                    departmentMatrials = departmentMatrials.Where(c => c.stage == stage).ToList();
                }
                if (!string.IsNullOrEmpty(year))
                {
                    departmentMatrials = departmentMatrials.Where(c => c.year == year).ToList();
                }
                //if (!departmentMatrials.Any())
                //    return NotFound();

                return Ok(departmentMatrials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// تستخدم هذه الداله فى الحصول على طلاب مواد الاستاذ
        /// </summary>
        [HttpGet("studentmaterial/{MaterialID}")]
        //[Authorize(Roles = "edu")]
        public async Task<IActionResult> GetStudentMaterailAsync(int MaterialID, string? stage, string? year)
        {
            try
            {
                var students = await _context.DepartmentsStudentmaterials
                    .Include(c => c.Studentinfromtion).ThenInclude(c => c.Student)
                    .Include(c => c.Studentinfromtion).ThenInclude(c => c.Stage)
                    .Include(c => c.Studentinfromtion).ThenInclude(c => c.Year)
                    .Where(c => c.MaterialId == MaterialID)
                    .Select(c => new StudentDegreeVM
                    {
                        StudentInformationID = c.Studentinfromtion.Id,
                        MaterailID = c.MaterialId,
                        StudentName = c.Studentinfromtion.Student.FullName, //اسم الطالب 
                        Quest = c.Quest, // درجه السعى 
                        stage =c.Studentinfromtion.Stage.Stage, // المرحله
                        year =c.Studentinfromtion.Year.Year, // السنه
                    }).ToListAsync();
                if (!string.IsNullOrEmpty(stage))
                {
                    students = students.Where(c => c.stage == stage).ToList();
                }
                if (!string.IsNullOrEmpty(year))
                {
                    students = students.Where(c => c.year == year).ToList();
                }
                if (students == null)
                    return NotFound();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// تستخدم هذه الداله فى لتعديل درجه السعى
        /// </summary>
        [HttpPut("Quest/{MaterialID}/{StudentInformationID}")]
        public async Task<IActionResult> PutQuestDegreeAsync(int MaterialID, int StudentInformationID, int? quest)
        {
            try
            {
                if (MaterialID == null && StudentInformationID == null)
                    return NotFound(new { message = "Materail and Student are required" });
                var studentMaterial = await _context.DepartmentsStudentmaterials
                    .FirstOrDefaultAsync(dsm => dsm.MaterialId == MaterialID && dsm.StudentinfromtionId == StudentInformationID);
                if (studentMaterial == null)
                    return NotFound(new { message = "Not Find Student Accourding To Information" });
               //if(quest == null)
               //     return NotFound(new { message = "Quest is Reqired" });
               studentMaterial.Quest =quest ?? studentMaterial.Quest;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
