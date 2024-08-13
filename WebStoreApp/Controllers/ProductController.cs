using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebStoreApp.Application.DTO;
using WebStoreApp.Application.DTO.Product;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Application.Services;
using WebStoreApp.Domain.Entities;

namespace WebStoreApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(PaginationParameters parameters)
        {

            try
            {
                // Fetch paginated product data
                var result = await _productService.GetPagedProductAsync(parameters);

                var model = new ProductViewModel()
                {
                    Product = result.Items,
                    TotalRecords = result.TotalRecords,
                    Pagination = parameters
                };

                return View(model);
            }
            catch (Exception)
            {
                // Handle exceptions
                return View("Error");
            }
        }

        #region Product Create 
        // Customer create 
        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {

            return View();
        }

        // Action to handle form submission for addning a new customer 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductAddDTO model)
        {
            if (ModelState.IsValid)
            {
                var success = await _productService.CreateProductAsync(model);

                if (success > 0)
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Product") });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update user." });
                }
            }

            return View(model);
        }

        #endregion










    }
}
