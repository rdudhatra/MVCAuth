using Microsoft.EntityFrameworkCore;
using MVCAuth.Core.Services.Interface;
using MVCAuth.Data;
using MVCAuth.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCAuth.Core.Services
{
    //Services on EmployeeController
    public class EmployeeService : IEmployeeService
    {
        private readonly MVCAuthContext _context;

        public EmployeeService(MVCAuthContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        //{
        //    return await _context.Employees.ToListAsync();
        //}

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesByNameAsync(string name)
        {
            return await _context.Employees.Where(e => e.Name.Contains(name)).ToListAsync();
        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var emp = await _context.Employees.FindAsync(employee.Id);
            if (emp == null)
            {
                return false;
            }

            emp.Salary = employee.Salary;
            emp.Department = employee.Department;
            emp.Name = employee.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Employee>> GetEmployeesPagedAsync(int pageIndex, int pageSize)
{
    return await _context.Employees
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
}

public async Task<int> GetTotalEmployeeCountAsync()
{
    return await _context.Employees.CountAsync();
}

	
	}
}
