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

        #region Proudct Update 

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var user = await _productService.GetProductByIdAsync(id);

                return View(user);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductUpdateDTO model)
        {
            if (model.ProductID == 0)
            {
                return Json(new { success = false, message = "Invalid Product ID." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return Json(new { success = false, message = string.Join(" ", errors) });
            }

            try
            {
                var success = await _productService.UpdateProductAsync(model);
                if (success)
                {

                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Product") });


                }
                else
                {
                    return Json(new { success = false, message = "Failed to update the product. Please try again later." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use your preferred logging method)
                return Json(new { success = false, message = "An error occurred while updating the product: " + ex.Message });
            }
        }


        #endregion










    }
}
