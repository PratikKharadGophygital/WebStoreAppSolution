using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStoreApp.Application.DTO;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Domain.Entities;
using WebStoreApp.Domain.Interfaces;

namespace WebStoreApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string sortColumn = "FirstName", string sortOrder = "ASC", string searchTerm = "")
        {
            if (searchTerm == null)
            {
                searchTerm = "";
            }

            if (string.IsNullOrWhiteSpace(sortColumn)) sortColumn = "FirstName";
            if (string.IsNullOrWhiteSpace(sortOrder)) sortOrder = "ASC";

            var result = await _customerService.GetPagedUsersAsync(pageNumber, pageSize, sortColumn, sortOrder, searchTerm);
            var users = result.Users;
            var totalRecords = result.TotalRecords;

            var model = new CustomerDto
            {
                Users = users,
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                SearchTerm = searchTerm,
                TotalRecords = totalRecords
            };

            return View(model);
        }

        #region Customer create 
        // Customer create 
        [HttpGet]
        public IActionResult AddUser()
        {
            
            return View();
        }

        // Action to handle form submission for addning a new customer 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(Customer model)
        {
            if (ModelState.IsValid) 
            { 
                await _customerService.CreateUserAsync(model);
                return RedirectToAction("Index");            
            }

            return View(model);
        }

        #endregion

        #region Customer edit 

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            try
            {
                var user = await _customerService.GetUserByIdAsync(id);

                return View(user);
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(CustomerUpdateDto model)
        {
            if (model.Id == 0)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage).ToList();
                return Json(new { sucess = false, message = string.Join(" ", error) });
            }

            var success = await _customerService.UpdateUserAsync(model);

            if (success)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Customer") });
            }
            else
            {
                return Json(new { success = false, message = "Failed to update user." });
            }
           
        }
        #endregion
    }
}
