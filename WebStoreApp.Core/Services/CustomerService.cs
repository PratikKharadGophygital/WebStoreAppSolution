using WebStoreApp.Application.DTO;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Interfaces;

namespace WebStoreApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerService _customerService;

        public CustomerService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            try
            {
                var customer = await _customerService.GetAllAsync();

                return customer.Select(c => new CustomerDto { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName, Address = c.Address, DateOfBirth = c.DateOfBirth, Email = c.Email });

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
