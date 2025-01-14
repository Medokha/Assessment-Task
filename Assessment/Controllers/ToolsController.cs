using Assessment.ViewModels.Edu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : Controller
    {
        HosinOldTestingContext _context = new HosinOldTestingContext();

        [HttpGet("Stages")]
        public async Task<IActionResult> GetStagesAsync()
        {
            try
            {
                var stages = await _context.DepartmentsStages.Select(x => x.Stage).ToListAsync();
                    
                if (stages == null)
                    return NotFound();
                return Ok(stages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Years")]
        public async Task<IActionResult> GetYearsAsync()
        {
            try
            {
                var years = await _context.DepartmentsYears.Select(x => x.Year).ToListAsync();

                if (years == null)
                    return NotFound();
                return Ok(years);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
