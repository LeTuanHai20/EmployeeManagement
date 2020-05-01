using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_]+@gmail.com$", ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public List<string> Claims { get; set; }
        public List<string> Roles { get; set; }
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
    }
}
