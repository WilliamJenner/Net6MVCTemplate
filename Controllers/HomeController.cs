using Microsoft.AspNetCore.Mvc;
using Net6MVCTemplate.API.Models;

namespace Net6MVCTemplate.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index", new IndexModel());
        }
    }
}
