using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStoreApp.Domain.Interfaces;



namespace WebStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        { 

            var customer = await _customerRepository.GetAllAsync();
            return Ok(customer);
        }

    }
}
