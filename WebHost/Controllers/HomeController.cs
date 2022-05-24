using Microsoft.AspNetCore.Mvc;
using Polaris.NetCoreMVCTemplate.WebHost.Models;

namespace Polaris.NetCoreMVCTemplate.WebHost.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index", new IndexModel());
        }
    }
}
