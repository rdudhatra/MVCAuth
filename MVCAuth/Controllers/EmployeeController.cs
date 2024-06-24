using Microsoft.AspNetCore.Mvc;
using MVCAuth.Core.Services.Interface;
using MVCAuth.Data.Models;

namespace MVCAuth.Controllers
{
    //Crud Operation Using Ajex and Javascript
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName(string name)
        {
            var employees = await _employeeService.SearchEmployeesByNameAsync(name);
            return Json(new { data = employees });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(int pageIndex = 1, int pageSize = 5)
        {
            var employees = await _employeeService.GetEmployeesPagedAsync(pageIndex, pageSize);
            var totalRecords = await _employeeService.GetTotalEmployeeCountAsync();
            //var employees = await _employeeService.GetAllEmployeesAsync();
            return Json(new { data = employees, totalRecords});
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                return Json(new { data = employee });
            }
            return Json(new { success = false });
        }


        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var success = await _employeeService.AddEmployeeAsync(employee);
            return Json(new { success });
        }


        [HttpPost]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var success = await _employeeService.UpdateEmployeeAsync(employee);
            return Json(new { success });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var success = await _employeeService.DeleteEmployeeAsync(id);
            return Json(new { success });
        }


    }
}
