using Assessment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assessment.Services;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentsService _DepartmentService;

        public DepartmentController(IDepartmentsService DepartmentService)
        {
            _DepartmentService = DepartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var dept = await _DepartmentService.GetAll();

            return Ok(dept);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DepartmentDto dto)
        {
            var dept = new Department { Name = dto.Name };

            await _DepartmentService.Add(dept);

            return Ok(dept);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] DepartmentDto dto)
        {
            var dept = await _DepartmentService.GetById(id);

            if (dept == null)
                return NotFound($"No dept was found with ID: {id}");

            dept.Name = dto.Name;

            _DepartmentService.Update(dept);

            return Ok(dept);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var dept = await _DepartmentService.GetById(id);

            if (dept == null)
                return NotFound($"No dept was found with ID: {id}");

            _DepartmentService.Delete(dept);

            return Ok(dept);
        }

    }
}