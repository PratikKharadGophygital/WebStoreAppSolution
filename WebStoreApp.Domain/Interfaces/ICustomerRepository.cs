
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        //Task<(IEnumerable<Customer>, Int64 TotalCount)>GetAllAsync(Int64 pageNumber, Int64 pageSize, string searchTerm, string sortColumn, string sortDirection);

        Task<(IEnumerable<Customer> Users, int TotalRecords)> GetPagedUsersAsync(int pageNumber, int pageSize, string sortColumn, string sortOrder, string searchTerm);

        Task<int> CreateUserAsync(Customer customer);

        Task<Customer> GetUserByIdAsync(int id);

        Task<bool> UpdateUserAsync(Customer customer);
    }
}
