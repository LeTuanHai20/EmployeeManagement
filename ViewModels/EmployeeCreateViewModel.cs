using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [RegularExpression(@"(^\w{1,19}$)|(^\w{1,19}\s{1}\w{1,19}$)|(^\w{1,19}\s{1}\w{1,19}\s{1}\w{1,19}$)", ErrorMessage = "Invalid Name format")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 character")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+@gmail.com$", ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public IFormFile Photo { get; set; }
    }
}
