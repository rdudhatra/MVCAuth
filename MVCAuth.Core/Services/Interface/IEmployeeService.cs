using MVCAuth.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAuth.Core.Services.Interface
{
    public interface IEmployeeService
    {
        //Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> SearchEmployeesByNameAsync(string name);
        Task<bool> AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
		Task<IEnumerable<Employee>> GetEmployeesPagedAsync(int pageIndex, int pageSize);
		Task<int> GetTotalEmployeeCountAsync();
	}
}
