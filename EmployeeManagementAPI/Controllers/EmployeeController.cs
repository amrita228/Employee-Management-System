using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET ALL EMPLOYEES
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _repository.GetAllAsync();

            return Ok(employees);
        }

        // ADD EMPLOYEE
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO dto)
        {
            try
            {
                var employee = new Employee
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Gender = dto.Gender,
                    DateOfBirth = dto.DateOfBirth,
                    DepartmentId = dto.DepartmentId,
                    RoleId = dto.RoleId,
                    Salary = dto.Salary,
                    JoiningDate = dto.JoiningDate,
                    Address = dto.Address,
                    IsActive = true
                };

                var result = await _repository.AddAsync(employee);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE EMPLOYEE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO dto)
        {
            try
            {
                var employees = await _repository.GetAllAsync();

                var employee = employees.FirstOrDefault(x => x.EmployeeId == id);

                if (employee == null)
                {
                    return NotFound("Employee not found");
                }

                employee.FirstName = dto.FirstName;
                employee.LastName = dto.LastName;
                employee.Email = dto.Email;
                employee.Phone = dto.Phone;
                employee.Gender = dto.Gender;
                employee.DateOfBirth = dto.DateOfBirth;
                employee.DepartmentId = dto.DepartmentId;
                employee.RoleId = dto.RoleId;
                employee.Salary = dto.Salary;
                employee.JoiningDate = dto.JoiningDate;
                employee.Address = dto.Address;

                await _repository.UpdateAsync(employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE EMPLOYEE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(id);

                if (!result)
                {
                    return NotFound("Employee not found");
                }

                return Ok("Employee Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}