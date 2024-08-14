using Microsoft.AspNetCore.Mvc;

namespace WebStoreApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
