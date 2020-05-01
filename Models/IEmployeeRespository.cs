
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public interface IEmployeeRespository
    {
        EmployeeModel GetEmployee(int Id);
        List<EmployeeModel> GetAllEmployee();
        EmployeeModel Add(EmployeeModel employee);
        EmployeeModel Update(EmployeeModel employeeChanges);
        EmployeeModel Delete(int Id);
    }
}
