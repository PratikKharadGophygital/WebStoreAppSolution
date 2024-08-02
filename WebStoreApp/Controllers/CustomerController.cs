using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebStoreApp.Application.DTO;
using WebStoreApp.Application.Interfaces;
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


        public async Task<IActionResult> Index(BaseEntityDto baseEntityDto)
        {
            if (baseEntityDto.PageNumber < 1) baseEntityDto.PageNumber = 1;

            if (!new[] { "ASC","DESC"}.Contains(baseEntityDto.SortDirection.ToUpper()) )
            {
                baseEntityDto.SortDirection = "ASC";
            }

            CustomerDto model = new CustomerDto();            

            try
            {
                var (customers, totalCount) = await _customerService.GetAllAsync(baseEntityDto);

                model.listCustomers = customers;
                model.TotalCount = totalCount;
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while fetching data.");
            }         

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(BaseEntityDto baseEntityDto)
        {

            var customer = await _customerService.GetAllAsync(baseEntityDto);
            return Ok(customer);
        }

    }
}
