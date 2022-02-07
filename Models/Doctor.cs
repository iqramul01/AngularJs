using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1263087.Models
{
    public class Doctor
    {
        
        public int? DoctorID { get; set; }
        [Required(ErrorMessage = "Doctor Name is Required!")]
        [StringLength(25, ErrorMessage = "Name must be less than 25 character")]
        public string DoctorName { get; set; }
        [Required(ErrorMessage = "Contact Address is Required!")]
        public string ContactAddress { get; set; }
        [Required(ErrorMessage = "Email is Required!")]
        public string EmailAddress { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }
}