using WebStoreApp.Application.DTO;


namespace WebStoreApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
    }
}
