using Assessment.Constants;
using Assessment.ViewModels.Edu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadStudentController : Controller
    {
        HosinOldTestingContext _context = new HosinOldTestingContext();

        [HttpGet]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetAllStudentInformationAsync()
        {
            try
            {
                var emp = await _context.UseresUsers
                    .Include(c => c.Dep)
                    .Include(c => c.Religion)
                    .Include(c => c.Nationality)
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

                    }).ToListAsync();
                if (emp == null)
                    return NotFound();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]

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


        [HttpGet("Waiting")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetAllStudentWaitingAsync()
        {
            try
            {
                var studentData = await _context.UseresWaiting
                   .Include(u => u.Dep) // الانضمام للقسم
                   .Include(u => u.createdby) // الانضمام إلى المستخدم المنشئ
                   .Where(u => u.IsStaff == false &&
                               u.StudentStatus != "تم التسجيل" &&
                               u.Dep != null) // التحقق من القيم غير null
                   .Select(u => new StudentTableVM
                   {
                       way = u.Way.Name ?? " ", // تحديد قيمة افتراضية
                       window = u.Window.Name ?? " ", // تحديد قيمة افتراضية
                       work = u.createdby.IsWork,
                       role = u.createdby.Role ?? " ", // تحديد قيمة افتراضية
                       Id = u.Id,
                       Name = u.FullName ?? " ", // تحديد قيمة افتراضية
                       Phone_Number = u.PhoneNumber ?? " ", // تحديد قيمة افتراضية
                       Gev = u.Gev ?? " ", // تحديد قيمة افتراضية
                       Adress = u.Area ?? " ", // تحديد قيمة افتراضية
                       SchooName = u.SchooName ?? " ", // تحديد قيمة افتراضية
                       status = u.StudentStatus ?? " ", // تحديد قيمة افتراضية
                       moadel = u.UniversyAvg ?? 0, // تحديد قيمة افتراضية (0 في حالة المعدل)
                       eduu = u.Edu ?? " ", // تحديد قيمة افتراضية
                       deppp = u.Dep.Name ?? " ", // تحديد قيمة افتراضية
                       username = u.MinistryUsername ?? " ", // تحديد قيمة افتراضية
                       mandname = u.createdby.Username ?? " ", // تحديد قيمة افتراضية
                       pasname = u.MinistrySecretCode ?? " ", // تحديد قيمة افتراضية
                   })
                   .ToListAsync();
                if (studentData == null)
                    return NotFound();
                return Ok(studentData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Users")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> GetAllUserInformationAsync()
        {
            try
            {
                var emp = await _context.userpermations
                    .Include(c => c.Dep)
                    .Include(c => c.Nationality)
                    .Select(c => new doctorinformation
                    {
                        name = c.Username,
                        phone = c.PhoneNumber,
                        jobtitel = c.JobTitle,
                        address = c.Address,
                        states = c.IsActive,
                        role = c.Role,
                        fullname = c.Name
                    }).ToListAsync();
                if (emp == null)
                    return NotFound();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Statistic")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> StatisticloginAsync()
        {
            try
            {

                var maxYearid = await _context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxtear = await _context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();


                var category = maxtear;

                var query = _context.DepartmentsStudentinfromtions
                .Include(c => c.Stage)
                .Include(c => c.Student)
                .ThenInclude(c => c.Dep)
                .Select(c => new StudentStatesVM
                {
                    id = c.Id,
                    FullName = c.Student.FullName,
                    Stage = c.Stage.Stage,
                    Fee = c.Fee,
                    Edu = c.Student.Edu,
                    State = c.State,
                    FeePrentage = c.FeePrentage,
                    DeptName = c.Student.Dep.Name,
                    accept = c.Student.StartYear.Year,
                    Role = c.Student.createdby.Role,
                    IsStaff = c.Student.IsStaff
                }).Distinct();
                var querydata = query.ToList();
                var Year = await _context.DepartmentsYears.Select(c => c.Year).ToListAsync();
                ViewBag.Years = Year;
                ViewBag.year = category;

                var stdbasicCount = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basic" /*|| c.createdby.Role == "basiccommit" || c.createdby.Role == "basiccommitadd"*/ && c.GrdNumberDate == category).ToListAsync();
                var stdmandoCount = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob" && c.GrdNumberDate == category).ToListAsync();
                var stdbasicCountwait = await _context.UseresWaiting.Include(c => c.createdby).Where(c => c.createdby.Role == "basic" /*|| c.createdby.Role == "basiccommit" || c.createdby.Role == "basiccommitadd"*/ && c.GrdNumberDate == maxtear).ToListAsync();
                var stdmandoCountwait = await _context.UseresWaiting.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob" && c.GrdNumberDate == maxtear).ToListAsync();
                var viewModel = new CountsVM
                {
                    //basic
                    StudentCount = querydata./*Where(c => && c.accept == category)*/Count(s => s.IsStaff == false),
                    StudentCountOngoing = querydata.Where(c => /*c.Role == "basic"*/  c.accept == category).Count(s => s.State == "مستمر"),
                    StudentCountDeferred = querydata.Where(c => /*c .Role == "basic" &&*/ c.accept == category).Count(s => s.State == "تأجيل سنة دراسية"),
                    StudentCountFailedAbsence = querydata.Where(c => c.accept == category).Count(s => s.State == "راسب بالغياب"),

                    StudentCountMovedPrivate = querydata.Where(c => c.accept == category).Count(s => s.State == "نقل من كلية اهلية"),
                    StudentCountFailedCheating = querydata.Where(c => c.accept == category).Count(s => s.State == "راسب بالغش"),

                    StudentCountHostedPrivate = querydata.Where(c => c.accept == category).Count(s => s.State == "استضافة من كلية اهلية"),
                    StudentCountSuspended = querydata.Where(c => c.accept == category).Count(s => s.State == "ترقين قيد بسبب الوفاة"),

                    StudentCountHostedOther = querydata.Where(c => c.accept == category).Count(s => s.State == "استضافة في كلية حكومية"),
                    StudentCountTerminated = querydata.Where(c => c.accept == category).Count(s => s.State == "ترقين قيد"),

                    StudentCountMovedOther23 = querydata.Where(c => c.accept == category).Count(s => s.State == "انسحاب بعد شهر"),
                    StudentCountFailed = querydata.Where(c => c.accept == category).Count(s => s.State == "انسحاب خلال شهر"),

                    StudentCountMovedPrivateCollege = querydata.Where(c => c.accept == category).Count(s => s.State == "نقل الى كلية أهلية"),
                    StudentCountMoved = querydata.Where(c => c.accept == category).Count(s => s.State == "نقل من كلية أهلية"),

                    StudentCountGraduate = querydata.Where(c => c.accept == category).Count(s => s.State == "استضافة في كلية أهلية"),
                    StudentCountHostedGov = querydata.Where(c => c.accept == category).Count(s => s.State == "استضافة من كلية حكومية"),

                    StudentCountMovedOther = querydata.Where(c => c.accept == category).Count(s => s.State == "نقل من كلية حكومية"),
                    StudentCountNonDirect = querydata.Where(c => c.accept == category).Count(s => s.State == "نقل الى كلية حكومية أوائل"),
                    StudentCountSuspendedDeath = querydata.Where(c => c.accept == category).Count(s => s.State == "نقل الى كلية حكومية"),

                    //mandob
                    mStudentCountOngoing = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "مستمر"),
                    mStudentCountDeferred = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "تأجيل سنة دراسية"),
                    mStudentCountFailedAbsence = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "راسب بالغياب"),
                    mStudentCountMovedPrivate = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "نقل من كلية اهلية"),
                    mStudentCountFailedCheating = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "راسب بالغش"),
                    mStudentCountHostedPrivate = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "استضافة من كلية اهلية"),
                    mStudentCountSuspended = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "ترقين قيد بسبب الوفاة"),
                    mStudentCountHostedOther = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "استضافة في كلية حكومية"),
                    mStudentCountTerminated = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "ترقين قيد"),
                    mStudentCountMovedOther23 = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "انسحاب بعد شهر"),
                    mStudentCountFailed = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "انسحاب خلال شهر"),
                    mStudentCountMovedPrivateCollege = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "نقل الى كلية اهلية"),
                    mStudentCountMoved = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "نقل من كلية أهلية"),
                    mStudentCountGraduate = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "استضافة في كلية أهلية"),
                    mStudentCountHostedGov = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "استضافة من كلية حكومية"),
                    mStudentCountMovedOther = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "نقل من كلية حكومية"),
                    mStudentCountNonDirect = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "نقل الى كلية حكومية أوائل"),
                    mStudentCountSuspendedDeath = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "mandob").CountAsync(s => s.MainStatus == "نقل الى كلية حكومية"),


                    //basiccommit
                    bcmStudentCount = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit").CountAsync(s => s.IsStaff == false),
                    bcmStudentCountOngoing = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "مستمر"),
                    bcmStudentCountDeferred = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "تأجيل سنة دراسية"),
                    bcmStudentCountFailedAbsence = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "راسب بالغياب"),
                    bcmStudentCountMovedPrivate = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "نقل من كلية اهلية"),
                    bcmStudentCountFailedCheating = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "راسب بالغش"),
                    bcmStudentCountHostedPrivate = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "استضافة من كلية اهلية"),
                    bcmStudentCountSuspended = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "ترقين قيد بسبب الوفاة"),
                    bcmStudentCountHostedOther = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "استضافة في كلية حكومية"),
                    bcmStudentCountTerminated = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "ترقين قيد"),
                    bcmStudentCountMovedOther23 = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "انسحاب بعد شهر"),
                    bcmStudentCountFailed = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "انسحاب خلال شهر"),
                    bcmStudentCountMovedPrivateCollege = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "نقل الى كلية اهلية"),
                    bcmStudentCountMoved = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "نقل من كلية أهلية"),
                    bcmStudentCountGraduate = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "استضافة في كلية أهلية"),
                    bcmStudentCountHostedGov = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "استضافة من كلية حكومية"),
                    bcmStudentCountMovedOther = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "نقل من كلية حكومية"),
                    bcmStudentCountNonDirect = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "نقل الى كلية حكومية أوائل"),
                    bcmStudentCountSuspendedDeath = await _context.UseresUsers.Include(c => c.createdby).Where(c => c.createdby.Role == "basiccommit" && c.GrdNumberDate == category).CountAsync(s => s.MainStatus == "نقل الى كلية حكومية"),




                    EmployeeCount = await _context.UseresUsers.CountAsync(s => s.IsStaff == true),
                    stdwatingCount = await _context.UseresUsers.CountAsync(s => s.IsStaff == false),
                    BatchCount = await _context.DepartmentsYears.CountAsync(d => d.Year != null),
                    DeptCount = await _context.DepartmentsDepartments.CountAsync(d => d.Short != null && d.Type == "std"),
                    stdbasicCount = stdbasicCount.Count(),
                    stdmandoCount = stdmandoCount.Count(),
                    nomoadalCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "دون معدل" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    createacountCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "انشاء حساب" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    mosadacCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب مصادق عليه" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    nomosadacCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب غير مصادق" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    requwstCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم التقديم" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    enterCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "ادخال فقط" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    norequestCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب قبل التسجيل" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    norequestacceptCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب بعد التسجيل" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),
                    acceptCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم التسجيل" && c.GrdNumberDate == category).CountAsync(s => s.IsStaff == false),

                    //
                    stdwatingCountwait = await _context.UseresWaiting.CountAsync(s => s.IsStaff == false),
                    stdbasicCountwait = stdbasicCountwait.Count(),
                    stdmandoCountwait = stdmandoCountwait.Count(),
                    nomoadalCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "دون معدل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    createacountCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "انشاء حساب" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    mosadacCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب مصادق عليه" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    nomosadacCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب غير مصادق" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    requwstCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم التقديم" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    enterCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "ادخال فقط" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    norequestCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب قبل التسجيل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    norequestacceptCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب بعد التسجيل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    acceptCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم التسجيل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),


                };


                var mosadacCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب مصادق عليه" && c.IsStaff == false /*&& c.GrdNumberDate == category */).ToListAsync();
                var nomosadacCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب غير مصادق" && c.IsStaff == false /*&& c.GrdNumberDate == category */).ToListAsync();
                var requwstCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم التقديم" && c.IsStaff == false /*&& c.GrdNumberDate == category */).ToListAsync();
                var norequestCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب قبل التسجيل" && c.IsStaff == false /*&& c.GrdNumberDate == category */).ToListAsync();
                var norequestacceptCount = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب بعد التسجيل" && c.IsStaff == false /*&& c.GrdNumberDate == category */).ToListAsync();



                if (viewModel == null)
                    return NotFound();
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("StudentStates")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> AllStudentStatesAsync()
        {
            try
            {

                var maxYearid = await _context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxtear = await _context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();


                var category = maxtear;

                var query = _context.DepartmentsStudentinfromtions
                .Include(c => c.Stage)
                .Include(c => c.Student)
                .ThenInclude(c => c.Dep)
                .Select(c => new StudentStatesVM
                {
                    id = c.Id,
                    FullName = c.Student.FullName,
                    Stage = c.Stage.Stage,
                    Fee = c.Fee,
                    Edu = c.Student.Edu,
                    State = c.State,
                    FeePrentage = c.FeePrentage,
                    DeptName = c.Student.Dep.Name,
                    accept = c.Student.StartYear.Year,
                    Role = c.Student.createdby.Role,
                    IsStaff = c.Student.IsStaff
                }).Distinct();
                var querydata = query.ToList();

                if (query == null)
                    return NotFound();
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
