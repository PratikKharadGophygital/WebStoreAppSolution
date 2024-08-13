using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Application.DTO;
using WebStoreApp.Application.DTO.Product;
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDTO>> GetPagedProductAsync(PaginationParameters parameters);
        Task<int> CreateProductAsync(ProductAddDTO productAdd);
    }
}
