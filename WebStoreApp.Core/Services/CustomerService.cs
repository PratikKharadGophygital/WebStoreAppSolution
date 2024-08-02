using WebStoreApp.Application.DTO;
using WebStoreApp.Application.Interfaces;
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

        public async Task<(IEnumerable<CustomerDto>, Int64 TotalCount)> GetAllAsync(BaseEntityDto baseEntityDto)
        {
            try
            {
               

                var customer = await _customerRepository.GetAllAsync(baseEntityDto);

                return customer.Select(c => new CustomerDto { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName, Address = c.Address, DateOfBirth = c.DateOfBirth, Email = c.Email });

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
