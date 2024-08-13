using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Application.DTO.Product
{
    public class ProductAddDTO
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }

        [StringLength(50)]
        public string? CouponCode { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Coupon Amount must be a positive value")]
        public decimal CouponAmount { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public ProductStatus StatusID { get; set; } = ProductStatus.Active;

        public byte IsActive { get; set; } = 1;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ProductAddDTO ConvertProductAddDTO(ProductEntity product)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            return new ProductAddDTO
            {
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

        public static ProductEntity ConvertProductEntity(ProductAddDTO product)
        {

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            return new ProductEntity
            {
                
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
