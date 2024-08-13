using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Application.DTO.Product
{
    public class ProductDTO
    {
        public int ProductID { get; set; }


        public string Name { get; set; } = string.Empty;


        public string Code { get; set; }

        public decimal Price { get; set; }


        public int Quantity { get; set; }


        public string? CouponCode { get; set; }


        public decimal CouponAmount { get; set; }


        public string? Description { get; set; }


        public ProductStatus StatusID { get; set; } = ProductStatus.Active;

        public byte IsActive { get; set; } = 1;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public static ProductDTO ConvertProductDTO(ProductEntity product)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            return new ProductDTO
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Code = product.Code,
                Price = product.Price,
                Quantity = product.Quantity,
                CouponCode = product.CouponCode,
                CouponAmount = product.CouponAmount,
                Description = product.Description,
                StatusID = product.StatusID,
                IsActive = product.IsActive,
                CreatedDate = product.CreatedDate
            };
        }
    }
}
