using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class HomeDetailViewModel
    {
        public EmployeeModel Employee { get; set; }
        public String PageTitle { get; set; }
    }
}
