
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<(IEnumerable<Customer>, Int64 TotalCount)>GetAllAsync(Int64 pageNumber, int pageSize, string searchTerm, string sortColumn, string sortDirection);
    }
}
