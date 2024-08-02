using WebStoreApp.Application.DTO;


namespace WebStoreApp.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<(IEnumerable<CustomerDto>, Int64 TotalCount)> GetAllAsync(BaseEntityDto baseEntityDto);
    }
}
