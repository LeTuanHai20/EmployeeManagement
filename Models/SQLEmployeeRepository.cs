using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRespository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLEmployeeRepository> logger;

        public SQLEmployeeRepository(AppDbContext context , ILogger<SQLEmployeeRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public EmployeeModel Add(EmployeeModel employee)
        {
            context.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public EmployeeModel Delete(int Id)
        {
            EmployeeModel employee = context.Employees.Find(Id);
            if (employee != null)
            {
                context.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public List<EmployeeModel> GetAllEmployee()
        {
            return context.Employees.ToList();
        }
        public EmployeeModel GetEmployee(int Id)
        {
            logger.LogTrace("Log Track");
            logger.LogDebug("Log Debug");
            logger.LogWarning("Log Waring");
            logger.LogError(" lOG ERROR");
            logger.LogCritical(" LogCritical");
            return context.Employees.Find(Id);
        }

        public EmployeeModel Update(EmployeeModel employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
