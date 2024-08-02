using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>>GetAllAsync();
    }
}
