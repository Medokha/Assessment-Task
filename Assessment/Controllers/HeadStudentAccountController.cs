using Assessment.Constants;
using Assessment.ViewModels.Edu;
using Assessment.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assessment.Data;
using MoreLinq;
using Microsoft.AspNetCore.Authorization;
namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadStudentAccountController : Controller
    {
        HosinOldTestingContext context = new HosinOldTestingContext();

        /// <summary>
        ///   تستخدم هذه الداله احصائيات الماليه للاقسام
        /// </summary>
        [HttpGet("staticaccount")]
        //[Authorize(Roles = "head")]
        public async Task<IActionResult> Getstaticaccount(string? year)
        {
             int total(int id, string year)
            {
                var stdmedical = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج"
                    && c.Studentinfromtion.Year.Year == year)
                    .AsEnumerable().DistinctBy(c => c.StudentinfromtionId)
                    .ToList();
                return stdmedical.Count();
            }
             async Task<decimal?> totalmony(int id, int stage, string year)
            {
                var total = await context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.StageId == stage && c.Studentinfromtion.Year.Year == year)
                    .Select(c => c.Studentinfromtion.Fee)
                    .ToListAsync();
                return total.Sum(p => p.HasValue ? (long)p.Value : 0);
            }
             async Task<decimal?> totalmonyall(int id, string year)
            {
                var total = await context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year)
                    .Select(c => c.Studentinfromtion.Fee)
                    .ToListAsync();
                return total.Sum(p => p.HasValue ? (long)p.Value : 0);
            }
             async Task<decimal?> totalmonypay(int id, string year)
            {
                var total = await context.AccountingStudentPayments
                   .Include(x => x.Studentinfromtion)
                   .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                   .Where(c => c.Studentinfromtion.Student.DepId == id
                   && c.Studentinfromtion.Year.Year == year &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Payment != 0)
                   .Select(c => c.Payment)
                   .ToListAsync();
                return total.Sum(p => p.HasValue ? (long)p.Value : 0);
            }
             int totalpay(int id, string year)
            {
                var stdmedical = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
                    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year && c.Payment != 0)
                    .AsEnumerable()
                    .DistinctBy(c => c.StudentinfromtionId)
                    .ToList();
                return stdmedical.Count();
            }
             int totalred(int id, string year)
            {
                var stdmedical = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year && c.Studentinfromtion.Reduction != 0)
                      .AsEnumerable().DistinctBy(c => c.StudentinfromtionId)
                    .ToList();
                return stdmedical.Count();
            }
             int totalstd(int id, string year)
            {
                var stdmedical = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year).ToList();
                return stdmedical.Count();
            }
             int totalnopay(int id, string year)
            {
                var stdmedical = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                     .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year && c.Studentinfromtion.Paid == 0)
                    .AsEnumerable().DistinctBy(c => c.StudentinfromtionId)
                    .ToList();
                return stdmedical.Count();
            }
             async Task<decimal?> totalmonypayre(int id, string year)
            {
                var total = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id
                    && c.Studentinfromtion.Year.Year == year &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Paid == 0)
                     .AsEnumerable()
                    .DistinctBy(c => c.StudentinfromtionId)
                    .Select(c => c.Studentinfromtion.Fee)
                    .ToList();
                return total.Sum(p => p.HasValue ? (long)p.Value : 0);
            }
             async Task<decimal?> totalmonyred(int id, string year)
            {
                var total = context.AccountingStudentPayments
                    .Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                               c.Studentinfromtion.Year.Year == year &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" &&
                               c.Studentinfromtion.Reduction != 0)
                    .AsEnumerable()
                    .DistinctBy(c => c.StudentinfromtionId)
                    .Select(c => c.Studentinfromtion.Reduction)
                    .ToList();
                return total.Sum(p => p.HasValue ? (long)p.Value : 0);
            }
             int totalcomplete(int id, int precentage, string year)
            {
                var stdmedicalcomplete = context.AccountingStudentPayments
                    .Include(x => x.Studentinfromtion)
                    .ThenInclude(x => x.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.FeePrentage == precentage && c.Studentinfromtion.Year.Year == year).ToList();
                return stdmedicalcomplete.Count();
            }


            try
            {
                var depts = await context.DepartmentsDepartments.Where(c => c.Type == "std").Select(x => x.Name).ToListAsync();
                var deptsid = await context.DepartmentsDepartments.Where(c => c.Type == "std").Select(x => x.Id).ToListAsync();
                ViewBag.Years = await context.DepartmentsYears.Select(x => x.Year).ToListAsync();
                var maxYearid = await context.DepartmentsYears.MaxAsync(y => y.Id);
                var categoryyear = year;
                stagedeptVM statisticspre100 = new stagedeptVM();
                List<newstatic> statisticspre1000 = new List<newstatic>();

                if (categoryyear == null)
                {
                    categoryyear = await context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();
                    if (globalvar.category == null)
                    {
                        globalvar.category = categoryyear;

                    }
                    categoryyear = globalvar.category;


                    ViewBag.year = categoryyear;

                    foreach (var d in depts)
                    {
                        int deptid = await context.DepartmentsDepartments.Where(c => c.Name == d).Select(x => x.Id).FirstOrDefaultAsync();
                        var c = new newstatic();
                        if ((totalpay(deptid, categoryyear) + totalnopay(deptid, categoryyear)) == 0)
                        {

                            c = new newstatic()
                            {
                                name = d, //اسم القسم  
                                paynumber = totalpay(deptid, categoryyear), // عدد الطلاب المسددين
                                rednumber = totalred(deptid, categoryyear), // عدد الطلاب الحاصلين على تخفيض
                                total = totalstd(deptid, categoryyear), // عدد الطلاب الكلى

                                nopaynumber = totalnopay(deptid, categoryyear), // عدد الطلاب العير مسددين
                                pay = await totalmonypay(deptid, categoryyear), // الاموال المسدده
                                red = await totalmonyred(deptid, categoryyear), // التفيضات
                                nopay = await totalmonypayre(deptid, categoryyear), // الغير مدفوعه
                                rate = 0

                            };
                        }
                        else
                        {
                            var r = (totalpay(deptid, categoryyear) + totalnopay(deptid, categoryyear));
                            var x = ((totalpay(deptid, categoryyear) / r) * 100);
                            c = new newstatic()
                            {
                                name = d,
                                paynumber = totalpay(deptid, categoryyear),
                                nopaynumber = totalnopay(deptid, categoryyear),
                                total = totalstd(deptid, categoryyear),

                                pay = await totalmonypay(deptid, categoryyear),
                                nopay = await totalmonypayre(deptid, categoryyear),
                                rednumber = totalred(deptid, categoryyear),
                                red = await totalmonyred(deptid, categoryyear),

                                rate = x

                            };
                        }
                        statisticspre1000.Add(c);
                    }



                    var totall = context.AccountingStudentPayments
                        .Include(x => x.Studentinfromtion).ThenInclude(c => c.Student)
                        .Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
                        .Where(c => c.Studentinfromtion.Year.Year == categoryyear)
                        .Where(c => c.Studentinfromtion.Fee != 0 && deptsid.Contains((int)c.Studentinfromtion.Student.DepId) &&
                        c.Studentinfromtion.Stage.Stage != "الخريج")
                        .AsEnumerable()
                        .DistinctBy(c => c.StudentinfromtionId)
                        .Select(c => c.Studentinfromtion.Fee)
                        .ToList();
                    ViewBag.one = totall.Sum(p => p.HasValue ? (long)p.Value : 0);
                    ViewBag.onenum = totall.Count();

                    var totalre = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                        .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Year.Year == categoryyear)
                                            .Where(c => c.Studentinfromtion.Reduction != 0 &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && deptsid.Contains((int)c.Studentinfromtion.Student.DepId))
                                            .AsEnumerable()
                        .DistinctBy(c => c.StudentinfromtionId)
                        .Select(c => c.Studentinfromtion.Reduction)
                        .ToList();
                    ViewBag.onere = totalre.Sum(p => p.HasValue ? (long)p.Value : 0);
                    ViewBag.onenumre = totalre.Count();

                    var totalpaid = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                        .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Year.Year == categoryyear)
                         .Where(c => c.Studentinfromtion.Paid != 0 &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && deptsid.Contains((int)c.Studentinfromtion.Student.DepId))
                        .AsEnumerable()
                        .DistinctBy(c => c.StudentinfromtionId)
                         .Select(c => c.Studentinfromtion.Paid)
                        .ToList();
                    ViewBag.onepaid = totalpaid.Sum(p => p.HasValue ? (long)p.Value : 0);
                    ViewBag.onenumpaid = totalpaid.Count();

                }
                else
                {
                    globalvar.category = categoryyear;

                    ViewBag.year = categoryyear;

                    foreach (var d in depts)
                    {
                        int deptid = await context.DepartmentsDepartments.Where(c => c.Name == d).Select(x => x.Id).FirstOrDefaultAsync();
                        var c = new newstatic();
                        if ((totalpay(deptid, categoryyear) + totalnopay(deptid, categoryyear)) == 0)
                        {
                            c = new newstatic()
                            {
                                red = await totalmonyred(deptid, categoryyear),
                                rednumber = totalred(deptid, categoryyear),
                                total = totalstd(deptid, categoryyear),
                                name = d,
                                paynumber = totalpay(deptid, categoryyear),
                                nopaynumber = totalnopay(deptid, categoryyear),
                                pay = await totalmonypay(deptid, categoryyear),
                                nopay = await totalmonypayre(deptid, categoryyear),
                                rate = 0

                            };
                        }
                        else
                        {
                            double Dividee(double numerator, double denominator)
                            {

                                return numerator / denominator;
                            }
                            var pay = totalpay(deptid, categoryyear);
                            var nopay = totalnopay(deptid, categoryyear);
                            var totalq = pay + nopay;
                            var x = totalq != 0 ? Dividee(pay, totalq) : 0;  // Avoid division by zero

                            var payMoney = await totalmonypay(deptid, categoryyear);
                            var nopayMoney = await totalmonypayre(deptid, categoryyear);

                            c = new newstatic()
                            {
                                red = await totalmonyred(deptid, categoryyear),
                                rednumber = totalred(deptid, categoryyear),
                                total = totalstd(deptid, categoryyear),

                                name = d,
                                paynumber = pay,
                                nopaynumber = nopay,
                                pay = payMoney,
                                nopay = nopayMoney,
                                rate = Math.Round(x * 100, 1)
                            };
                        }
                        statisticspre1000.Add(c);
                    }

                    var totalll = context.AccountingStudentPayments
                        .Include(x => x.Studentinfromtion).ThenInclude(c => c.Student)
                                            .Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Year.Year == categoryyear)
                        .Where(c => c.Studentinfromtion.Fee != 0 &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && deptsid.Contains((int)c.Studentinfromtion.Student.DepId))
                        .AsEnumerable()
                        .DistinctBy(c => c.StudentinfromtionId)
                        .Select(c => c.Studentinfromtion.Fee)
                        .ToList();
                    ViewBag.one = totalll.Sum(p => p.HasValue ? (long)p.Value : 0);
                    ViewBag.onenum = totalll.Count();

                    var totalre = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                        .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Year.Year == categoryyear)
                         .Where(c => c.Studentinfromtion.Reduction != 0 &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && deptsid.Contains((int)c.Studentinfromtion.Student.DepId))
                        .AsEnumerable()
                        .DistinctBy(c => c.StudentinfromtionId).Select(c => c.Studentinfromtion.Reduction)
                        .ToList();
                    ViewBag.onere = totalre.Sum(p => p.HasValue ? (long)p.Value : 0);
                    ViewBag.onenumre = totalre.Count();

                    var totalpaid = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                        .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Year.Year == categoryyear)
                         .Where(c => c.Studentinfromtion.Paid != 0 &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && deptsid.Contains((int)c.Studentinfromtion.Student.DepId))
                        .AsEnumerable()
                        .DistinctBy(c => c.StudentinfromtionId).Select(c => c.Studentinfromtion.Paid)
                        .ToList();
                    ViewBag.onepaid = totalpaid.Sum(p => (long)p.GetValueOrDefault());

                    ViewBag.onenumpaid = totalpaid.Count();

                    double Divide(double numerator, double denominator)
                    {

                        return numerator / denominator;
                    }
                    var payy = totalpaid.Sum(p => (long)p.GetValueOrDefault());
                    var totall = totalll.Sum(p => (long)p.GetValueOrDefault());
                    var xx = totall != 0 ? Divide(payy, totall) : 0;  // Avoid division by zero

                    var rate = xx * 100;

                    ViewBag.rate = Math.Round(rate, 1);

                }
                ViewBag.statistic100 = statisticspre100;


                if (statisticspre1000 == null)
                    return NotFound();

                //return Ok(statisticspre1000);
                return Ok(new
                {
                    year = categoryyear,
                    departmentsStatistics = statisticspre1000,
                    totalFees = ViewBag.one,
                    totalFeesCount = ViewBag.onenum,
                    totalReductions = ViewBag.onere,
                    totalReductionsCount = ViewBag.onenumre,
                    totalPaid = ViewBag.onepaid,
                    totalPaidCount = ViewBag.onenumpaid,
                    overallRate = ViewBag.rate
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// تستخدم هذه الداله فى اظهار الدفعات الماليه لجميع الطلاب
        /// </summary>
        [HttpGet("StudentPayment")]
        //[Authorize(Roles = "head")]
        public async Task<IActionResult> GetStudentPaymentPositionData(int? page, string? year, string? stage)
        {
            try
            {
                var pageSize = 20; // Number of items per page

                // Get the maximum YearId from DepartmentsYears table
                var maxYearid = await context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxtear = await context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();

                var category = maxtear;

                if (year == null)
                {
                    category = maxtear;
                }
                else
                {
                    category = year;
                }
                // Query to get the student payment data with the necessary relationships
                var query = context.DepartmentsStudentinfromtions
                    .Include(c => c.Stage)
                    .Include(c => c.Student)
                        .ThenInclude(c => c.Dep)
                        .ThenInclude(c => c.AccountingAnnualInstallments)
                    .Where(c => c.Year.Year == category)
                    .Select(c => new PaymentAccVM
                    {
                        id = c.Id,
                        FullName = c.Student.FullName, // اسم الطالب
                        DeptName = c.Student.Dep.Name, // القسم
                        Stage = c.Stage.Stage, // المرحله
                        Edu = c.Student.Edu, // الفتره
                        MainStatus = c.Student.MainStatus,// الحاله
                        Fee = c.Fee, // المبلغ الكلى
                        Reduction = c.Reduction, // التخفيض
                        Paid = c.Paid, // المدفوع
                        State = c.State, //الحاله حسب المرحله
                        rest = (c.Fee - c.Reduction - c.Paid), // المتبقى
                        accept = c.Year.Year, // سنه القبول
                        FeePrentage = c.FeePrentage, // نسبه الدفع
                        Installment = c.Fee - c.Reduction, // الاقساط 
                        FirstInstallment = (c.Fee - c.Reduction) == 0 ? 1 : c.Paid / ((c.Fee - c.Reduction) / 5) >= 1 ? 1 : 0, // القسط الاول
                        SecondInstallment = (c.Fee - c.Reduction) == 0 ? 1 : c.Paid / ((c.Fee - c.Reduction) / 5) >= 2 ? 1 : 0, // القسط الثانى
                        ThirdInstallment = (c.Fee - c.Reduction) == 0 ? 1 : c.Paid / ((c.Fee - c.Reduction) / 5) >= 3 ? 1 : 0, // القسط الثالث
                        FourthInstallment = (c.Fee - c.Reduction) == 0 ? 1 : c.Paid / ((c.Fee - c.Reduction) / 5) >= 4 ? 1 : 0, // القسط الرابع
                        FifthInstallment = (c.Fee - c.Reduction) == 0 ? 1 : c.Paid / ((c.Fee - c.Reduction) / 5) >= 5 ? 1 : 0, // القسط الخامس
                    })
                    .OrderByDescending(o => o.id); // Sorting by Id in descending order (can adjust to any field)
                if (!string.IsNullOrEmpty(stage))
                {
                    query = (IOrderedQueryable<PaymentAccVM>)query.Where(c => c.Stage == stage);
                }
                // Calculate the total number of records for pagination
                var totalCount = await query.CountAsync();

                // Calculate total pages
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure the page number is within a valid range
                page = Math.Max(page ?? 1, 1); // Default to 1 if null or less than 1
                page = Math.Min(page ?? 1, totalPages); // Ensure page does not exceed total pages

                // Retrieve the data for the current page
                var studentPaymentData = await query
                    .Skip((page.Value - 1) * pageSize) // Skip to the correct page
                    .Take(pageSize) // Take only the page size number of records
                    .ToListAsync();

                // Return the result with pagination metadata
                var result = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    StudentPaymentData = studentPaymentData
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// تستخدم هذه الداله فى اظهار جميع الوصلات
        /// </summary>
        [HttpGet("StudentReceipts")]
        //[Authorize(Roles = "head")]
        public async Task<IActionResult> GetStudentReceiptsData(int? page, string? year,string? stage)
        {
            try
            {
                var pageSize = 20; // Number of items per page

                // Get the maximum YearId from DepartmentsYears table
                var maxYearid = await context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxtear = await context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();

                var category = maxtear;

                if (year == null)
                {
                    category = maxtear;
                }
                else
                {
                    category = year;
                }
                // Query to get the student receipts data with the necessary relationships
                var query = context.DepartmentsStudentinfromtions
                    .Include(dsf => dsf.Year)
                    .Include(dsf => dsf.Stage)
                    .Include(dsf => dsf.Student)
                        .ThenInclude(s => s.Dep)
                    .Where(c => c.Year.Year == category)
                    .SelectMany(sd => sd.AccountingStudentPayments.Select(ac => new ReceiptsToReturnVM
                    {
                        RecId = ac.Id,
                        DSFID = sd.Id,
                        Id = sd.StudentId,
                        StudentName = sd.Student.FullName, // اسم الطالب
                        Stage = sd.Stage.Stage, // المرحله
                        Department = sd.Student.Dep.Name, // القسم 
                        VoucherNumber = ac.VoucherNumber, // رقم الوصل 
                        PaidDate = ac.Date, // تاريخ الدفع
                        UniversitTuition = ac.Payment, // فسط
                        Identity = ac.Identity, // هويه
                        Protest = ac.Protest,
                        SupportBook = ac.Support,
                        Other = ac.Other,
                        Insurances = ac.Insurances,
                        accept = sd.Year.Year,
                        Penalty = ac.Penalty,
                        Training = ac.Training,
                        GraduationDoc = ac.GraduationDoc,
                        Clearance = ac.NonCurrentActivity,
                        penality = ac.Penalty
                    }))
                    .OrderByDescending(o => o.RecId); // Sorting by RecId in descending order
                if (!string.IsNullOrEmpty(stage))
                {
                    query = (IOrderedQueryable<ReceiptsToReturnVM>)query.Where(c => c.Stage == stage);
                }
                // Calculate the total number of records for pagination
                var totalCount = await query.CountAsync();

                // Calculate total pages
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                // Ensure the page number is within a valid range
                page = Math.Max(page ?? 1, 1); // Default to 1 if null or less than 1
                page = Math.Min(page ?? 1, totalPages); // Ensure page does not exceed total pages

                // Retrieve the data for the current page
                var studentReceipts = await query
                    .Skip((page.Value - 1) * pageSize) // Skip to the correct page
                    .Take(pageSize) // Take only the page size number of records
                    .ToListAsync();

                // Return the result with pagination metadata
                var result = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    TotalCount = totalCount,
                    StudentReceipts = studentReceipts
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
