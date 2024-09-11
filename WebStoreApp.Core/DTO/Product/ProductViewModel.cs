using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStoreApp.Application.DTO.Product
{
    public class ProductViewModel
    {
        public PaginationParameters Pagination { get; set; } = new PaginationParameters();
        public int TotalRecords { get; set; }
        public IEnumerable<ProductDTO> Product { get; set; } = Enumerable.Empty<ProductDTO>();
        // Properties for creating or updating
        public ProductAddDTO AddDTO { get; set; }
        public ProductUpdateDTO UpdateDTO { get; set; }

    }

}
