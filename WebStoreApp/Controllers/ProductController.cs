using Microsoft.AspNetCore.Mvc;
using WebStoreApp.Application.DTO;

namespace WebStoreApp.Controllers
{
    public class ProductController : Controller
    {

        public ProductController()
        {
                
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10, string sortColumn = "FirstName", string sortOrder = "ASC", string searchTerm = "")
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

            return View(model);
        }
    }
}
