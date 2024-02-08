using Microsoft.AspNetCore.Mvc;

namespace BloodDonationAPI.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
