using System.Data.SqlTypes;
using WebStoreApp.Application.DTO;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Entities;
using WebStoreApp.Domain.Interfaces;



namespace WebStoreApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> CreateUserAsync(Customer customer)
        {
            try
            {

                return await _customerRepository.CreateUserAsync(customer);
            }
            catch (Exception)
            {

                throw;
            }
        }   

        public async Task<(IEnumerable<Customer> Users, int TotalRecords)> GetPagedUsersAsync(int pageNumber, int pageSize, string sortColumn, string sortOrder, string searchTerm)
        {
            try
            {
                return await _customerRepository.GetPagedUsersAsync(pageNumber, pageSize, sortColumn, sortOrder, searchTerm);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CustomerUpdateDto> GetUserByIdAsync(int id)
        {
            CustomerUpdateDto customerUpdateDto = new CustomerUpdateDto();

            var result = await _customerRepository.GetUserByIdAsync(id);

            if (result is not null)
            {
                customerUpdateDto = customerUpdateDto.ConvertToCustomerUpdateDto(result);
            }


            return customerUpdateDto;
        }

        public async Task<bool> UpdateUserAsync(CustomerUpdateDto customerUpdateDto)
        {
            var data = customerUpdateDto.ConvertToCustomer(customerUpdateDto);

            var result = await _customerRepository.UpdateUserAsync(data);

            return result;


        }
    }
}
