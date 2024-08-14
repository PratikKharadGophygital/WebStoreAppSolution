using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Application.DTO
{
    public class CustomerUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(20, ErrorMessage = "Phone Number cannot be longer than 20 characters")]
        public string PhoneNumber { get; set; }

        public static CustomerUpdateDto ConvertToCustomerUpdateDto(Customer customer)
        {

            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            var dto = new CustomerUpdateDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber

            };

            return dto;
        }

        public static Customer ConvertToCustomer(CustomerUpdateDto customerUpdatedto)
        {

            if (customerUpdatedto == null)
            {
                throw new ArgumentNullException(nameof(customerUpdatedto));
            }

            var dto = new Customer
            {
                Id = customerUpdatedto.Id,
                FirstName = customerUpdatedto.FirstName,
                LastName = customerUpdatedto.LastName,
                Email = customerUpdatedto.Email,
                Address = customerUpdatedto.Address,
                PhoneNumber = customerUpdatedto.PhoneNumber

            };

            return dto;
        }


    }


}
