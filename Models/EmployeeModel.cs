using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeModel
    {
        public int ID { get; set; }
        [NotMapped]
        public string EncryptedId{ get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+[a-zA-Z0-9_]+$", ErrorMessage = "Invalid Email format")]
        [MaxLength(50,ErrorMessage ="Name cannot exceed 50 character")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+@gmail.com$",ErrorMessage ="Invalid Email format")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string PhotoPath { get; set; }

    }
}
