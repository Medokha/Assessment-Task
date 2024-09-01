using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assessment.Services;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _empService;
        private readonly IDepartmentsService  _DeptService;


        public EmployeeController(IEmployeeService employeeService, IDepartmentsService departmentsService, IMapper mapper)
        {
            _empService = employeeService;
            _DeptService =  departmentsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var emp = await _empService.GetAll();

            var data = _mapper.Map<IEnumerable<EmpDTO>>(emp);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var emp = await _empService.GetById(id);

            if(emp == null)
                return NotFound();

            var dto = _mapper.Map<EmpDTO>(emp);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string name,int salary,int dep)
        {

            var x = new Employee()
            {
                Name = name,
                Salary = salary,
                departmentId = dep

            };


            _empService.Add(x);

            return Ok(x);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EmpDTO empt)
        {
            var emp = await _empService.GetById(id);

            if (emp == null)
                return NotFound($"No employee was found with ID {id}");

            if (string.IsNullOrWhiteSpace(empt.name))
                return BadRequest("Employee name cannot be empty.");

            if (empt.salary <= 0)
                return BadRequest("Salary must be a positive value.");

            if (empt.department <= 0)
                return BadRequest("Department ID must be a positive value.");

            emp.Name = empt.name;
            emp.departmentId = empt.department;
            emp.Salary = empt.salary;

             _empService.Update(emp);

            var dto = _mapper.Map<EmpDTO>(emp);
            return Ok(dto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var emp = await _empService.GetById(id);

            if (emp == null)
                return NotFound($"No emplyee was found with ID {id}");

            _empService.Delete(emp);

            return Ok(emp);
        }
    }
}