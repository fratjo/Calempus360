using Microsoft.AspNetCore.Mvc;

namespace Calempus360_api.Controllers
{
    public class AdministratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
