using Microsoft.AspNetCore.Mvc;

namespace BloodDonationAPI.Controllers
{
    public class DonationOperationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
