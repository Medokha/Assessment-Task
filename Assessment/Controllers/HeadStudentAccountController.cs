using Assessment.Constants;
using Assessment.ViewModels.Edu;
using Assessment.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assessment.Data;
using MoreLinq;
namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadStudentAccountController : Controller
    {
        HosinOldTestingContext context = new HosinOldTestingContext();

        [HttpGet("staticaccount")]
        public async Task<IActionResult> Getstaticaccount()
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
                var categoryyear = await context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();
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
                                name = d,
                                paynumber = totalpay(deptid, categoryyear),
                                rednumber = totalred(deptid, categoryyear),
                                total = totalstd(deptid, categoryyear),

                                nopaynumber = totalnopay(deptid, categoryyear),
                                pay = await totalmonypay(deptid, categoryyear),
                                red = await totalmonyred(deptid, categoryyear),
                                nopay = await totalmonypayre(deptid, categoryyear),
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

                return Ok(statisticspre1000);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("StudentPayment")]
        public async Task<IActionResult> GetStudentPaymentPositionData()
        {
            try
            {
                var maxYearId = await context.DepartmentsYears.MaxAsync(y => y.Id);
                var studentData = await context.DepartmentsStudentinfromtions
          .Include(c => c.Stage)
          .Include(c => c.Student)
          .ThenInclude(c => c.Dep)
          .ThenInclude(c => c.AccountingAnnualInstallments)
          .Where(c=>c.YearId == maxYearId)
          .Select(c => new PaymentAccVM
          {

              id = c.Id,
              FullName = c.Student.FullName,
              DeptName = c.Student.Dep.Name,
              Stage = c.Stage.Stage,
              Edu = c.Student.Edu,
              MainStatus = c.Student.MainStatus,
              Fee = c.Fee,
              Reduction = c.Reduction,
              Paid = c.Paid,
              State = c.State,
              rest = (c.Fee - c.Reduction - c.Paid),
              accept = c.Year.Year,
              FeePrentage = c.FeePrentage,
              //Rate = c.Rate,
              Installment = c.Fee - c.Reduction,
              FirstInstallment = (c.Fee - c.Reduction) == 0 ? 1 :
              c.Paid / ((c.Fee - c.Reduction) / 5) >= 1 ? 1 : 0,
              SecondInstallment = (c.Fee - c.Reduction) == 0 ? 1 :
              c.Paid / ((c.Fee - c.Reduction) / 5) >= 2 ? 1 : 0,
              ThirdInstallment = (c.Fee - c.Reduction) == 0 ? 1 :
              c.Paid / ((c.Fee - c.Reduction) / 5) >= 3 ? 1 : 0,
              FourthInstallment = (c.Fee - c.Reduction) == 0 ? 1 :
              c.Paid / ((c.Fee - c.Reduction) / 5) >= 4 ? 1 : 0,
              FifthInstallment = (c.Fee - c.Reduction) == 0 ? 1 :
              c.Paid / ((c.Fee - c.Reduction) / 5) >= 5 ? 1 : 0,
          }).ToListAsync();

                if (studentData == null)
                    return NotFound();

                return Ok(studentData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("StudentReceipts")]
        public async Task<IActionResult> GetStudentReceiptsData()
        {
            try

            {
                var maxYearId = await context.DepartmentsYears.MaxAsync(y => y.Id);

                var studentsData = await context.DepartmentsStudentinfromtions
                       .Include(dsf => dsf.Year)
                       .Include(dsf => dsf.Stage)
                       .Include(dsf => dsf.Student).ThenInclude(s => s.Dep)
                                 .Where(c => c.YearId == maxYearId)
                       .SelectMany(sd => sd.AccountingStudentPayments.Select(ac => new ReceiptsToReturnVM
                       {
                           RecId = ac.Id,
                           DSFID = sd.Id,
                           Id = sd.StudentId,
                           StudentName = sd.Student.FullName,
                           Stage = sd.Stage.Stage,
                           Department = sd.Student.Dep.Name,
                           VoucherNumber = ac.VoucherNumber,
                           PaidDate = ac.Date,
                           UniversitTuition = ac.Payment,
                           Identity = ac.Identity,
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
                       .OrderByDescending(o => o.RecId)
                       .ToListAsync();

                if (studentsData == null)
                    return NotFound();

                return Ok(studentsData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
