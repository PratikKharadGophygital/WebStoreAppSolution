using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<PaginatedResult<ProductEntity>> GetPagedProductAsync(int pageNumber, int pageSize, string sortColumn, string sortOrder, string searchTerm, CancellationToken cancellationToken = default);

        Task<int> CreateProductAsync(ProductEntity productAdd);

        Task<ProductEntity> GetProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(ProductEntity model);
    }
}
