using Assessment.Constants;
using Assessment.ViewModels.Edu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadStasticAccountController : Controller
    {
        HosinOldTestingContext context = new HosinOldTestingContext();


        [HttpGet("staticbystage")]
        public async Task<IActionResult> GetstaticbystageAsync()
        {
             async Task<long> total(int id, int stage, string year)
            {
                var stdmedical = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                     .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id
                     && c.Studentinfromtion.StageId == stage &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year)
                      .AsEnumerable()
                     .DistinctBy(c => c.StudentinfromtionId)
                     .ToList();
                return stdmedical.Count();
            }
             async Task<decimal?> totalmony(int id, int stage, string year)
            {
                var total = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id &&
                    c.Studentinfromtion.StageId == stage &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year)
                    .AsEnumerable()
                    .DistinctBy(c => c.StudentinfromtionId).Select(c => c.Studentinfromtion.Fee)
                    .ToList();
                return total.Sum(p => (long)p.GetValueOrDefault());
            }
             async Task<decimal?> totalmonypaid(int id, int stage, string year)
            {
                var total = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                   .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id
                   && c.Studentinfromtion.StageId == stage &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year)
                    .AsEnumerable()
                   .DistinctBy(c => c.StudentinfromtionId).Select(c => c.Studentinfromtion.Paid)
                    .ToList();
                return total.Sum(p => (long)p.GetValueOrDefault());
            }
             async Task<decimal?> totalmonyred(int id, int stage, string year)
            {
                var total = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(c => c.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)
    .Where(c => c.Studentinfromtion.Student.DepId == id
                    && c.Studentinfromtion.StageId == stage && c.Studentinfromtion.Year.Year == year &&
                        c.Studentinfromtion.Stage.Stage != "الخريج")
                     .AsEnumerable()
                    .DistinctBy(c => c.StudentinfromtionId).Select(c => c.Studentinfromtion.Reduction)
                    .ToList();
                return total.Sum(p => (long)p.GetValueOrDefault());
            }
             async Task<long> totalcomplete(int id, int precentage, int stage, string year)
            {
                var stdmedicalcomplete = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(x => x.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                    .Where(c => c.Studentinfromtion.Student.DepId == id && c.Studentinfromtion.FeePrentage == precentage
                    && c.Studentinfromtion.StageId == stage &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.Year.Year == year)
                    .AsEnumerable()
                    .DistinctBy(c => c.StudentinfromtionId)
                    .ToList();
                return stdmedicalcomplete.Count();
            }
             async Task<long> totalallcomplete(int id, int stage, string year)
            {
                var stdmedicalcomplete = context.AccountingStudentPayments.Include(x => x.Studentinfromtion)
                    .ThenInclude(x => x.Student).Include(x => x.Studentinfromtion).ThenInclude(c => c.Stage)

                    .Where(c => c.Studentinfromtion.Student.DepId == id
                    && c.Studentinfromtion.FeePrentage != 0 &&
                        c.Studentinfromtion.Stage.Stage != "الخريج" && c.Studentinfromtion.StageId == stage && c.Studentinfromtion.Year.Year == year)
                    .AsEnumerable()
                    .DistinctBy(c => c.StudentinfromtionId)
                    .ToList();
                return stdmedicalcomplete.Count();
            }
             async Task<stagedeptVM> Stagedept(int stage, int dept, string year)
            {
                decimal t = await totalmony(dept, stage, year) ?? 0;
                decimal d = await totalmonyred(dept, stage, year) ?? 0;
                decimal p = await totalmonypaid(dept, stage, year) ?? 0;
                stagedeptVM depstage = new stagedeptVM()
                {
                    numberstu = await total(dept, stage, year),
                    numberstupaid = await totalallcomplete(dept, stage, year),
                    numberstupaidcomplete = await totalcomplete(dept, 100, stage, year),
                    numberstunopaidcomplete = await totalcomplete(dept, 0, stage, year),
                    totalmony = await totalmony(dept, stage, year) ?? 0,
                    monyreduction = await totalmonyred(dept, stage, year) ?? 0,
                    monypaid = await totalmonypaid(dept, stage, year) ?? 0,
                    monynopaid = t - (d + p)
                };
                return depstage;
            }
             stagedeptVM stagedepttotal(stagedeptVM? one = null, stagedeptVM? two = null,
                stagedeptVM? three = null, stagedeptVM? four = null, stagedeptVM? five = null, stagedeptVM? grd = null)
            {
                stagedeptVM x = new stagedeptVM()
                {
                    numberstu = one.numberstu ?? 0 + two.numberstu ?? 0 + three.numberstu ?? 0 + four.numberstu ?? 0 + five.numberstu ?? 0 + grd.numberstu ?? 0,
                    numberstupaid = one.numberstupaid ?? 0 + two.numberstupaid ?? 0 + three.numberstupaid ?? 0 + four.numberstupaid ?? 0 + five.numberstupaid ?? 0 + grd.numberstupaid ?? 0,
                    numberstupaidcomplete = one.numberstupaidcomplete ?? 0 + two.numberstupaidcomplete ?? 0 + three.numberstupaidcomplete ?? 0 + four.numberstupaidcomplete ?? 0 + five.numberstupaidcomplete ?? 0 + grd.numberstupaidcomplete ?? 0,
                    numberstunopaidcomplete = one.numberstunopaidcomplete ?? 0 + two.numberstunopaidcomplete ?? 0 + three.numberstunopaidcomplete ?? 0 + four.numberstunopaidcomplete ?? 0 + five.numberstunopaidcomplete ?? 0 + grd.numberstunopaidcomplete ?? 0,
                    totalmony = one.totalmony ?? 0 + two.totalmony ?? 0 + three.totalmony ?? 0 + four.totalmony ?? 0 + five.totalmony ?? 0 + grd.totalmony ?? 0,
                    monyreduction = one.monyreduction ?? 0 + two.monyreduction ?? 0 + three.monyreduction ?? 0 + four.monyreduction ?? 0 + five.monyreduction + grd.monyreduction ?? 0,
                    monypaid = one.monypaid ?? 0 + two.monypaid ?? 0 + three.monypaid ?? 0 + four.monypaid ?? 0 + five.monypaid ?? 0 + grd.monypaid ?? 0,
                    monynopaid = one.monynopaid ?? 0 + two.monynopaid ?? 0 + three.monynopaid ?? 0 + four.monynopaid ?? 0 + five.monynopaid ?? 0 + grd.monynopaid ?? 0
                };
                return x;
            }


            try
            {
                ViewBag.Years = await context.DepartmentsYears.Select(x => x.Year).ToListAsync();
                var maxYearid = await context.DepartmentsYears.MaxAsync(y => y.Id);
                var maxtear = await context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();
                var category = maxtear;
                if (category == null)
                {
                    //category = maxtear;
                    if (globalvar.category == null)
                    {
                        globalvar.category = maxtear;

                    }
                    category = globalvar.category;
                }
                else
                {
                    globalvar.category = category;
                }
                ViewBag.year = category;
                int friststageid = await context.DepartmentsStages.Where(c => c.Stage == "الاولى").Select(c => c.Id).FirstOrDefaultAsync();
                int secondstageid = await context.DepartmentsStages.Where(c => c.Stage == "الثانية").Select(c => c.Id).FirstOrDefaultAsync();
                int thiredstageid = await context.DepartmentsStages.Where(c => c.Stage == "الثالثة").Select(c => c.Id).FirstOrDefaultAsync();
                int fourtageid = await context.DepartmentsStages.Where(c => c.Stage == "الرابعة").Select(c => c.Id).FirstOrDefaultAsync();
                int fivestageid = await context.DepartmentsStages.Where(c => c.Stage == "الخامسة").Select(c => c.Id).FirstOrDefaultAsync();
                int sixstageid = await context.DepartmentsStages.Where(c => c.Stage == "السادسه").Select(c => c.Id).FirstOrDefaultAsync();
                int gradestageid = await context.DepartmentsStages.Where(c => c.Stage == "الخريج").Select(c => c.Id).FirstOrDefaultAsync();
                var depts = await context.DepartmentsDepartments.Where(c => c.Type == "std").Select(x => x.Id).ToListAsync();
                List<statictotal> s = new List<statictotal>();
                long x11 = 0;
                long x12 = 0;
                long x13 = 0;
                long x14 = 0;
                decimal? x15 = 0;
                decimal? x16 = 0;
                decimal? x17 = 0;
                decimal? x18 = 0;
                foreach (var dept in depts)
                {
                    statictotal v = new statictotal();
                    var level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync();
                    if (level == 4)
                    {

                        v = new statictotal()
                        {

                            Name = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Name).FirstOrDefaultAsync(),
                            level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync() ?? 5,
                            x1 = await Stagedept(friststageid, dept, category),
                            x2 = await Stagedept(secondstageid, dept, category),
                            x3 = await Stagedept(thiredstageid, dept, category),
                            x4 = await Stagedept(fourtageid, dept, category),
                            x6 = new stagedeptVM
                            {
                                stage = " ",
                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x5 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },

                            x7 = stagedepttotal(await Stagedept(friststageid, dept, category),
                               await Stagedept(secondstageid, dept, category), await Stagedept(thiredstageid, dept, category),
                               await Stagedept(fourtageid, dept, category))
                        };
                        s.Add(v);
                    }
                    else if (level == 5)
                    {
                        v = new statictotal()
                        {
                            level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync() ?? 5,

                            Name = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Name).FirstOrDefaultAsync(),
                            x1 = await Stagedept(friststageid, dept, category),
                            x2 = await Stagedept(secondstageid, dept, category),
                            x3 = await Stagedept(thiredstageid, dept, category),
                            x4 = await Stagedept(fourtageid, dept, category),
                            x5 = await Stagedept(fivestageid, dept, category),
                            x6 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x7 = stagedepttotal(await Stagedept(friststageid, dept, category),
                               await Stagedept(secondstageid, dept, category), await Stagedept(thiredstageid, dept, category),
                               await Stagedept(fourtageid, dept, category), await Stagedept(fivestageid, dept, category))
                        };
                        s.Add(v);
                    }
                    else if (level == 6)
                    {
                        v = new statictotal()
                        {
                            level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync() ?? 5,

                            Name = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Name).FirstOrDefaultAsync(),
                            x1 = await Stagedept(friststageid, dept, category),
                            x2 = await Stagedept(secondstageid, dept, category),
                            x3 = await Stagedept(thiredstageid, dept, category),
                            x4 = await Stagedept(fourtageid, dept, category),
                            x5 = await Stagedept(fivestageid, dept, category),
                            x6 = await Stagedept(sixstageid, dept, category),
                            x7 = stagedepttotal(await Stagedept(friststageid, dept, category),
                               await Stagedept(secondstageid, dept, category), await Stagedept(thiredstageid, dept, category),
                               await Stagedept(fourtageid, dept, category), await Stagedept(fivestageid, dept, category), await Stagedept(sixstageid, dept, category))
                        };
                        s.Add(v);
                    }
                    else if (level == 3)
                    {
                        v = new statictotal()
                        {
                            level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync() ?? 5,

                            Name = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Name).FirstOrDefaultAsync(),
                            x1 = await Stagedept(friststageid, dept, category),
                            x2 = await Stagedept(secondstageid, dept, category),
                            x3 = await Stagedept(thiredstageid, dept, category),
                            x6 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x5 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x4 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x7 = stagedepttotal(await Stagedept(friststageid, dept, category),
                                   await Stagedept(secondstageid, dept, category), await Stagedept(thiredstageid, dept, category)
                                   )
                        };
                        s.Add(v);
                    }
                    else if (level == 2)
                    {
                        v = new statictotal()
                        {
                            level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync() ?? 5,

                            Name = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Name).FirstOrDefaultAsync(),
                            x1 = await Stagedept(friststageid, dept, category),
                            x2 = await Stagedept(secondstageid, dept, category),
                            x6 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x5 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x4 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x3 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x7 = stagedepttotal(await Stagedept(friststageid, dept, category),
                           await Stagedept(secondstageid, dept, category))

                        };
                        s.Add(v);
                    }
                    else if (level == 1)
                    {
                        v = new statictotal()
                        {
                            level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync() ?? 5,

                            Name = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Name).FirstOrDefaultAsync(),
                            x1 = await Stagedept(friststageid, dept, category),
                            x6 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x5 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x4 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x3 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x2 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x7 = stagedepttotal(await Stagedept(friststageid, dept, category))
                        };
                        s.Add(v);
                    }
                    else
                    {
                        v = new statictotal()
                        {
                            level = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Level).FirstOrDefaultAsync() ?? 5,

                            Name = await context.DepartmentsDepartments.Where(c => c.Type == "std" && c.Id == dept).Select(x => x.Name).FirstOrDefaultAsync(),
                            x1 = await Stagedept(friststageid, dept, category),
                            x2 = await Stagedept(secondstageid, dept, category),
                            x3 = await Stagedept(thiredstageid, dept, category),
                            x4 = await Stagedept(fourtageid, dept, category),
                            x5 = await Stagedept(fivestageid, dept, category),
                            x6 = new stagedeptVM
                            {
                                stage = " ",

                                numberstu = 0,
                                numberstupaid = 0,
                                numberstupaidcomplete = 0,
                                numberstunopaidcomplete = 0,
                                totalmony = 0,
                                monypaid = 0,
                                monyreduction = 0,
                                monynopaid = 0
                            },
                            x7 = stagedepttotal(await Stagedept(friststageid, dept, category),
                           await Stagedept(secondstageid, dept, category), await Stagedept(thiredstageid, dept, category),
                           await Stagedept(fourtageid, dept, category), await Stagedept(fivestageid, dept, category), await Stagedept(gradestageid, dept, category))
                        };
                        s.Add(v);
                    }


                    x11 += v.x1.numberstu ?? 0;
                    x11 += v.x2.numberstu ?? 0;
                    x11 += v.x3.numberstu ?? 0;
                    x11 += v.x4.numberstu ?? 0;
                    x11 += v.x5.numberstu ?? 0;
                    x11 += v.x6.numberstu ?? 0;
                    x11 += v.x7.numberstu ?? 0;
                    //************
                    x12 += v.x1.numberstupaid ?? 0;
                    x12 += v.x2.numberstupaid ?? 0;
                    x12 += v.x3.numberstupaid ?? 0;
                    x12 += v.x4.numberstupaid ?? 0;
                    x12 += v.x5.numberstupaid ?? 0;
                    x12 += v.x6.numberstupaid ?? 0;
                    x12 += v.x7.numberstupaid ?? 0;
                    //************
                    x13 += v.x1.numberstupaidcomplete ?? 0;
                    x13 += v.x2.numberstupaidcomplete ?? 0;
                    x13 += v.x3.numberstupaidcomplete ?? 0;
                    x13 += v.x4.numberstupaidcomplete ?? 0;
                    x13 += v.x5.numberstupaidcomplete ?? 0;
                    x13 += v.x6.numberstupaidcomplete ?? 0;
                    x13 += v.x7.numberstupaidcomplete ?? 0;
                    //************                   ?0
                    x14 += v.x1.numberstunopaidcomplete ?? 0;
                    x14 += v.x2.numberstunopaidcomplete ?? 0;
                    x14 += v.x3.numberstunopaidcomplete ?? 0;
                    x14 += v.x4.numberstunopaidcomplete ?? 0;
                    x14 += v.x5.numberstunopaidcomplete ?? 0;
                    x14 += v.x6.numberstunopaidcomplete ?? 0;
                    x14 += v.x7.numberstunopaidcomplete ?? 0;
                    //************
                    x15 += v.x1.totalmony ?? 0;
                    x15 += v.x2.totalmony ?? 0;
                    x15 += v.x3.totalmony ?? 0;
                    x15 += v.x4.totalmony ?? 0;
                    x15 += v.x5.totalmony ?? 0;
                    x15 += v.x6.totalmony ?? 0;
                    x15 += v.x7.totalmony ?? 0;
                    //************
                    x16 += v.x1.monypaid ?? 0;
                    x16 += v.x2.monypaid ?? 0;
                    x16 += v.x3.monypaid ?? 0;
                    x16 += v.x4.monypaid ?? 0;
                    x16 += v.x5.monypaid ?? 0;
                    x16 += v.x6.monypaid ?? 0;
                    x16 += v.x7.monypaid ?? 0;
                    //************
                    x17 += v.x1.monyreduction ?? 0;
                    x17 += v.x2.monyreduction ?? 0;
                    x17 += v.x3.monyreduction ?? 0;
                    x17 += v.x4.monyreduction ?? 0;
                    x17 += v.x5.monyreduction ?? 0;
                    x17 += v.x6.monyreduction ?? 0;
                    x17 += v.x7.monyreduction ?? 0;
                    //************
                    x18 += v.x1.monynopaid ?? 0;
                    x18 += v.x2.monynopaid ?? 0;
                    x18 += v.x3.monynopaid ?? 0;
                    x18 += v.x4.monynopaid ?? 0;
                    x18 += v.x5.monynopaid ?? 0;
                    x18 += v.x6.monynopaid ?? 0;
                    x18 += v.x7.monynopaid ?? 0;

                }



                if (s == null)
                    return NotFound();

                return Ok(s);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
