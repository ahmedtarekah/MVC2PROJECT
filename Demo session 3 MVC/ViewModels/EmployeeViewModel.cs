using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo_session_3_MVC.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
        [MinLength(4, ErrorMessage = "Min Length is 4 Chars")]
        public string Name { get; set; }

        [Range(22, 45, ErrorMessage = "Age Must be betweeen 22 and 45")]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
        , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile  Image { get; set; }
        public string ImageName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; } 
    }
}
