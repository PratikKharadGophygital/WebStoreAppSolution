using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStoreApp.Application.Common.Extension;
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

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_CustomerListPartial", model);

            //}

            return View(model);
        }


        //public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string sortColumn = "FirstName", string sortOrder = "ASC", string searchTerm = "")
        //{
        //    if (string.IsNullOrWhiteSpace(sortColumn)) sortColumn = "FirstName";
        //    if (string.IsNullOrWhiteSpace(sortOrder)) sortOrder = "ASC";

        //    var result = await _customerService.GetPagedUsersAsync(pageNumber, pageSize, sortColumn, sortOrder, searchTerm);
        //    var users = result.Users;
        //    var totalRecords = result.TotalRecords;

        //    var model = new CustomerDto
        //    {
        //        Users = users,
        //        PageNumber = pageNumber,
        //        PageSize = pageSize,
        //        SortColumn = sortColumn,
        //        SortOrder = sortOrder,
        //        SearchTerm = searchTerm,
        //        TotalRecords = totalRecords
        //    };

        //    // Request is in-build method use
        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_CustomerListPartial", model);
        //    }

        //    return View(model);
        //}


        #region Customer create 
        // Customer create 
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {

            return View();
        }

        // Action to handle form submission for addning a new customer 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserAdd(Customer model)
        {
            if (ModelState.IsValid)
            {
                var success = await _customerService.CreateUserAsync(model);

                if (success > 0)
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Customer") });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to update user." });
                }
            }

            return View(model);
        }

        #endregion

        #region Customer edit 

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            if (id <= 0)
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
