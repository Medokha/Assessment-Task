using Assessment.Constants;
using Assessment.ViewModels.Edu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadStudentController : Controller
    {
        HosinOldTestingContext _context = new HosinOldTestingContext();

        /// <summary>
        /// تستخدم هذه الداله فى اظهار جميع الطلاب
        /// </summary>
        [HttpGet("Student")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllStudentInformationAsync(int? page)
        {
            try
            {
                var pageSize = 20; // Number of items per page

                // Get the students with related data and select the required fields
                var studentsQuery = _context.UseresUsers
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
                    });

                // Calculate the total number of students and the total number of pages
                var totalCount = await studentsQuery.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure page is valid
                page = Math.Max(page ?? 1, 1); // Default to 1 if null or less than 1
                page = Math.Min(page ?? 1, totalPages); // Ensure page does not exceed total pages

                // Skip to the correct page and take the required number of items
                var studentsOnPage = await studentsQuery
                    .Skip((page.Value - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Prepare the metadata
                var result = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    Students = studentsOnPage
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// تستخدم هذه الداله فى اظهار معلومات الطالب
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize(Roles = "admin")]
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

        /// <summary>
        /// تستخدم هذه الداله فى اظهار جميع الطلاب الانتظار
        /// </summary>
        [HttpGet("Waiting")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllStudentWaitingAsync(int? page)
        {
            try
            {
                var pageSize = 20; // Number of items per page

                // Query to get the student data with necessary filters and joins
                var studentQuery = _context.UseresWaiting
                    .Include(u => u.Dep) // Join with department
                    .Include(u => u.createdby) // Join with createdby user
                    .Where(u => u.IsStaff == false &&
                                u.StudentStatus != "تم التسجيل" &&
                                u.Dep != null) // Check for non-null department and student status
                    .Select(u => new StudentTableVM
                    {
                        way = u.Way.Name ?? " ", // Default value if null
                        window = u.Window.Name ?? " ", // Default value if null
                        work = u.createdby.IsWork,
                        role = u.createdby.Role ?? " ", // Default value if null
                        Id = u.Id,
                        Name = u.FullName ?? " ", // Default value if null
                        Phone_Number = u.PhoneNumber ?? " ", // Default value if null
                        Gev = u.Gev ?? " ", // Default value if null
                        Adress = u.Area ?? " ", // Default value if null
                        SchooName = u.SchooName ?? " ", // Default value if null
                        status = u.StudentStatus ?? " ", // Default value if null
                        moadel = u.UniversyAvg ?? 0, // Default value (0 if null)
                        eduu = u.Edu ?? " ", // Default value if null
                        deppp = u.Dep.Name ?? " ", // Default value if null
                        username = u.MinistryUsername ?? " ", // Default value if null
                        mandname = u.createdby.Username ?? " ", // Default value if null
                        pasname = u.MinistrySecretCode ?? " ", // Default value if null
                    });

                // Calculate the total number of students
                var totalCount = await studentQuery.CountAsync();

                // Calculate total pages based on pageSize
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure the page number is within a valid range
                page = Math.Max(page ?? 1, 1); // Default to 1 if null or less than 1
                page = Math.Min(page ?? 1, totalPages); // Ensure page does not exceed total pages

                // Retrieve the students for the current page
                var studentsOnPage = await studentQuery
                    .Skip((page.Value - 1) * pageSize) // Skip to the correct page
                    .Take(pageSize) // Take only the page size amount
                    .ToListAsync();

                // Return the result with pagination metadata
                var result = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    Students = studentsOnPage
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// تستخدم هذه الداله فى اظهار جميع الموظفين
        /// </summary>
        [HttpGet("Users")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUserInformationAsync(int? page)
        {
            try
            {
                var pageSize = 20; // Number of items per page

                // Query to get the user data with necessary joins and filters
                var userQuery = _context.userpermations
                    .Include(c => c.Dep) // Join with department
                    .Include(c => c.Nationality) // Join with nationality
                    .Select(c => new doctorinformation
                    {
                        name = c.Username,
                        phone = c.PhoneNumber,
                        jobtitel = c.JobTitle,
                        address = c.Address,
                        states = c.IsActive,
                        role = c.Role,
                        fullname = c.Name
                    });

                // Calculate the total number of users
                var totalCount = await userQuery.CountAsync();

                // Calculate total pages based on pageSize
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure the page number is within a valid range
                page = Math.Max(page ?? 1, 1); // Default to 1 if null or less than 1
                page = Math.Min(page ?? 1, totalPages); // Ensure page does not exceed total pages

                // Retrieve the users for the current page
                var usersOnPage = await userQuery
                    .Skip((page.Value - 1) * pageSize) // Skip to the correct page
                    .Take(pageSize) // Take only the page size amount
                    .ToListAsync();

                // Return the result with pagination metadata
                var result = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    Users = usersOnPage
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// تستخدم هذه الداله فى اظهار احصائيات جميع الطلاب
        /// </summary>
        [HttpGet("Statistic")]
        //[Authorize(Roles = "admin")]

        public async Task<IActionResult> StatisticloginAsync(string? year)
        {
            try
            {

                var maxYearid = await _context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxtear = await _context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();


                var category = maxtear;
                if (year == null)
                {
                    category = maxtear;
                }
                else
                {
                    category = year;
                }

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
                    //الاحصائيات الخاصه بموظفين التسجيل 
                    // يعرض عدد الطلاب طبقا للحالات الاتيه
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
                    //الاحصائيات الخاصه بالمندوبين 
                    // يعرض عدد الطلاب طبقا للحالات الاتيه
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
                    //الاحصائيات الخاصه بموظفين لجنه التسجيل 
                    // يعرض عدد الطلاب طبقا للحالات الاتيه
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




                    EmployeeCount = await _context.UseresUsers.CountAsync(s => s.IsStaff == true),//الموظفين 
                    stdwatingCount = await _context.UseresUsers.CountAsync(s => s.IsStaff == false),// عدد الطلاب الكلي
                    BatchCount = await _context.DepartmentsYears.CountAsync(d => d.Year != null), // عدد الدفعات
                    DeptCount = await _context.DepartmentsDepartments.CountAsync(d => d.Short != null && d.Type == "std"), // عدد الاقسام العلميه
                    stdbasicCount = stdbasicCount.Count(), //(عدد طلبة (موظف التسجيل
                    stdmandoCount = stdmandoCount.Count(), //عدد طلبة المندوبين
                    // عدد الطلاب فى قائمه الانتظار طبقا للحلات الاتيه
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
                    stdwatingCountwait = await _context.UseresWaiting.CountAsync(s => s.IsStaff == false), // عدد طلاب قائمه الانتظار
                    stdbasicCountwait = stdbasicCountwait.Count(), // عدد طلاب قائمه الانتظا التى تم ادخالهم من خلال موظف التسجيل
                    stdmandoCountwait = stdmandoCountwait.Count(), // عدد طلاب قائمه الانتظار التى تم تسجيلهم من خلال المندوبين
                    //nomoadalCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "دون معدل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //createacountCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "انشاء حساب" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //mosadacCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب مصادق عليه" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //nomosadacCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "حساب غير مصادق" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //requwstCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم التقديم" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //enterCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "ادخال فقط" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //norequestCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب قبل التسجيل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //norequestacceptCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم الانسحاب بعد التسجيل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),
                    //acceptCountwait = await _context.UseresWaiting.Where(c => c.StudentStatus == "تم التسجيل" && c.GrdNumberDate == maxtear).CountAsync(s => s.IsStaff == false),


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

        /// <summary>
        /// تستخدم هذه الداله فى اظهار حاله جميع الطلاب
        /// </summary>
        [HttpGet("StudentStates")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> AllStudentStatesAsync(int? page, string? year)
        {
            try
            {
                var pageSize = 20; // Number of items per page

                // Retrieve the latest year from the DepartmentsYears table
                var maxYearid = await _context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxtear = await _context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();

                var category = maxtear;

                if (year == null)
                {
                    category = maxtear;
                }
                else
                {
                    category = year;
                }

                // Query to get the student states along with necessary related data
                var query = _context.DepartmentsStudentinfromtions
                    .Include(c => c.Stage)
                    .Include(c => c.Year)
                    .Include(c => c.Student)
                    .ThenInclude(c => c.Dep)
                    .Where(c => c.Year.Year == category) // Filter by the latest year
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
                    })
                    .Distinct(); // Ensure unique records
                if (query == null)
                    return NotFound();
                // Calculate the total number of records for pagination
                var totalCount = await query.CountAsync();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure the page number is valid
                page = Math.Max(page ?? 1, 1); // Default to 1 if null or less than 1
                page = Math.Min(page ?? 1, totalPages); // Ensure page does not exceed total pages

                // Skip and take based on the current page
                var queryData = await query
                    .Skip((page.Value - 1) * pageSize) // Skip to the correct page
                    .Take(pageSize) // Take only the number of items per page
                    .ToListAsync();

                // Return the result with pagination metadata
                var result = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    StudentStates = queryData
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
