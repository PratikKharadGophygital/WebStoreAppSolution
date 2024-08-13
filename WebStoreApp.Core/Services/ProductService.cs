using WebStoreApp.Application.DTO;
using WebStoreApp.Application.DTO.Product;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Entities;
using WebStoreApp.Domain.Interfaces;

namespace WebStoreApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

       
        public async Task<int> CreateProductAsync(ProductAddDTO productAdd)
        {

            var data = ProductAddDTO.ConvertProductEntity(productAdd);

            return await _repository.CreateProductAsync(data);
        }

        public async Task<PaginatedResult<ProductDTO>> GetPagedProductAsync(PaginationParameters parameters)
        {

            // Default sort column to a valid column if not provided
            string sortColumn = string.IsNullOrWhiteSpace(parameters.SortColumn) ? "FirstName" : parameters.SortColumn;

            // Validate and sanitize sort order
            string sortOrder = parameters.SortOrder == SortOrder.Ascending ? "ASC" : "DESC";

            try
            {         
                
                var result =  await _repository.GetPagedProductAsync(
                parameters.PageNumber,
                parameters.PageSize,
                parameters.SortColumn,
                sortOrder,
                parameters.SearchTerm
                );

                var productDTO = result.Items.Select(ProductDTO.ConvertProductDTO).ToList();

                return new PaginatedResult<ProductDTO>
                {
                    Items = productDTO,
                    TotalRecords = result.TotalRecords,

                };
            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
