using WebStoreApp.Application.DTO;
using WebStoreApp.Domain.Entities;


namespace WebStoreApp.Application.Interfaces
{
    public interface ICustomerService
    {
        //Task<(IEnumerable<CustomerDto>, Int64 TotalCount)> GetAllAsync(BaseEntityDto baseEntityDto);
        Task<(IEnumerable<Customer> Users, int TotalRecords)> GetPagedUsersAsync(int pageNumber, int pageSize, string sortColumn, string sortOrder, string searchTerm);

        Task<int> CreateUserAsync(Customer customer);

        Task<CustomerUpdateDto> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(CustomerUpdateDto customerUpdateDto);


    }
}
