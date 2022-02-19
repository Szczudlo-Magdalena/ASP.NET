using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.Controllers
{
    [Authorize(Roles = "StoreManager,Owner")]
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}
