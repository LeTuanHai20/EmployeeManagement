using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuiderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder) {
            modelBuilder.Entity<EmployeeModel>().HasData(
               new EmployeeModel()
               {
                   ID = 1,
                   Name = "Mary",
                   Email = "Mary@gmail.com",
                   Department = Dept.HR
               },
                new EmployeeModel()
                {
                    ID = 2,
                    Name = "John",
                    Email = "John@gmail.com",
                    Department = Dept.IT
                }
               );
        }
    }
}
