using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Application.DTO
{
    public class CustomerDto
    {
        public Int64 Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^\+?\d{1,4}?[-.\s\(\)]*\d{1,15}$", ErrorMessage = "Invalid phone number")]
        [StringLength(100)]
        public string PhoneNumber { get; set; }
    }
}
